using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dimstar : MonoBehaviour
{
    [SerializeField] float throwforce = 10f;
    [SerializeField] public Rigidbody rb;
    [SerializeField] KeyCode abilitykey = KeyCode.Mouse2;
    [SerializeField] GameObject star;
    [SerializeField] Transform startransform;
    [SerializeField] Transform player;

    [SerializeField] Transform playertransform;
    [SerializeField] public Rigidbody playerrb;
    [SerializeField] LayerMask stopat;
    [SerializeField] float maxraydis = 0.1f; 
    public bool starisout;

    void Start()
    {
        star.SetActive(false);
        rb.useGravity = false;
    }

    void Update()
    {

        if (Physics.Raycast(startransform.position, startransform.forward, maxraydis))
        {
            rb.velocity = Vector3.zero;
        }
        

        if (Input.GetKeyDown(abilitykey))
        {
            if (!starisout)
            {
                starisout = true;
                star.SetActive(true);
                startransform.position = player.position + player.forward;
                startransform.rotation = player.rotation;
                rb.velocity = Vector3.zero;
                rb.AddForce(player.forward * throwforce, ForceMode.Force);
            } else if (starisout) {
                playertransform.position = startransform.position;
                starisout = false;
                star.SetActive(false);
                
            }
        }
    }
}
