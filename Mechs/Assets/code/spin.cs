using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spin : MonoBehaviour
{
    [SerializeField] public Rigidbody rb;
    [SerializeField] Transform star;

    void Update()
    {
        Vector3 obj_velocity = rb.velocity;
        Vector3 noll = Vector3.zero;
        if (obj_velocity != noll){
            star.Rotate(0, 10, 0 * Time.deltaTime);
        }
    }
}
