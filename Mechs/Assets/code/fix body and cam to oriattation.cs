using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    
    [SerializeField] Transform oriantion;

    // Update is called once per frame
    void Update()
    {
        transform.rotation = oriantion.rotation;
    }
}
