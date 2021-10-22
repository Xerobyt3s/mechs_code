using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grapple_rope : MonoBehaviour
{
    private spring spring;
    private LineRenderer lr;
    private Vector3 currentGrapplePosition;
    public grapple grapple;
    public int quality;
    public float damper;
    public float strength;
    public float velocity;
    public float waveCount;
    public float waveHight;
    public AnimationCurve affectCurve;


    void Awake() {
        lr = GetComponent<LineRenderer>();
        spring = new spring();
        spring.SetTarget(0);
    }

    //Called after Update
    void LateUpdate() {
        DrawRope();
    } 
    void DrawRope() {
        //If not grappling, don't draw rope
        if (!grapple.IsGrappling()) 
        {
            currentGrapplePosition = grapple.gunTip.position;
            spring.Reset();
            if (lr.positionCount > 0)
                lr.positionCount = 0;
            return;
        }

        if (lr.positionCount == 0)
        {
            spring.SetVelocity(velocity);
            lr.positionCount = quality + 1;
        }

        spring.SetDamper(damper);
        spring.SetStrength(strength);
        spring.Update();

        var grapplePoint = grapple.GetGrapplePoint();
        var guntipposition = grapple.gunTip.position;
        var up = Quaternion.LookRotation((grapplePoint - guntipposition).normalized) * Vector3.up;
         


        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 12f);

        for (int i = 0; i < quality + 1; i++) {
            var delta = i / (float) quality;
            var offset = up * waveHight * Mathf.Sin(delta * waveCount * Mathf.PI) * spring.Value * affectCurve.Evaluate(delta);
            lr.SetPosition(i, Vector3.Lerp(guntipposition, currentGrapplePosition, delta) + offset);
        
        }
    }
}
