using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class d_jump : MonoBehaviour
{
    [SerializeField] playerMovementV2 pv;
    [SerializeField] Rigidbody rb;

    bool hasdjump = true;
    void Update()
    {
        if (pv.isGrounded){
            hasdjump = true;
        }

        if (Input.GetKey(KeyCode.Space) && hasdjump && !pv.isGrounded) {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(transform.up * pv.jumpforce, ForceMode.Impulse);
            hasdjump = false;
        }
    }
}
