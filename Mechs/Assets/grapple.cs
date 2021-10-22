using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grapple : MonoBehaviour
{
    private Vector3 grapplePoint;
    public LayerMask whatIsGrappleable;
    public Transform gunTip, cam, player;
    [SerializeField] private float maxDistance = 10f;

    [SerializeField] private float drawforce = 10f;

    [SerializeField] private float drawdistance = 1f;
    private SpringJoint joint;

    public bool isgappeld;

    [SerializeField] public Rigidbody rb;
        

    void Update() {
        if (Input.GetKeyDown(KeyCode.Mouse2)) {
            StartGrapple();
        }
        else if (Input.GetKeyUp(KeyCode.Mouse2)) {
            StopGrapple();
        }
    }

    

    /// <summary>
    /// Call whenever we want to start a grapple
    /// </summary>
    void StartGrapple() {
        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, maxDistance, whatIsGrappleable)) {
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            //The distance grapple will try to keep from grapple point. 
            joint.maxDistance = distanceFromPoint * 0.5f;
            joint.minDistance = distanceFromPoint * 0.2f;

            if (distanceFromPoint > drawdistance)
            {
                rb.AddForce((hit.point - transform.position).normalized * drawforce , ForceMode.Acceleration);
            }

            //Adjust these values to fit your game.
            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            isgappeld = true;
        }
    }


    /// <summary>
    /// Call whenever we want to stop a grapple
    /// </summary>
    void StopGrapple() {
        Destroy(joint);
        isgappeld = false;
    }

    

    

    public bool IsGrappling() {
        return joint != null;
    }

    public Vector3 GetGrapplePoint() {
        return grapplePoint;
    }
}
