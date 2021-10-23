using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovementV2 : MonoBehaviour
{
    [SerializeField] Transform oriantation;

    [Header("Movement")]
    public float moveSpeed = 6f;
    float MovementMultiplier = 10f;
    [SerializeField] float airmultiplier = 0.4f;

    [Header("Crouch")]
    [SerializeField] private Vector3 crouchscale = new Vector3(1.25f, 0.5f, 1.25f);
    [SerializeField] private Vector3 playerscale;
    [SerializeField] float slideforce = 400f;
    //[SerializeField] float slidedrag = 0.2f;


    [Header("sprinting")]
    [SerializeField] bool autosprint = false;
    [SerializeField] float walkSpeed = 4f;
    [SerializeField] float sprintSpeed = 4f;

    [SerializeField] float grapplespeed = 20f;
    [SerializeField] float acceleration = 10f;

    [SerializeField] grapple grapple;

    [Header("jumping")]
    public float jumpforce = 5f;

    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] KeyCode sprintkey = KeyCode.LeftShift;
    [SerializeField] KeyCode Crouchkey = KeyCode.LeftControl;

    [Header("Drag")]
    float GroundDrag = 6f;
    float AirDrag = 1f;

    float playerhight = 3f;

    float horizontalMovement;
    float verticalMovement;

    [Header("Ground Detection")]
    [SerializeField] Transform groundcheck;
    [SerializeField] LayerMask groundmask;
    public bool isGrounded;
    float grounddistance = 0.2f;

    bool crouching = false;
    

    Vector3 moveDirection;
    Vector3 SlopeMoveDirection;

    Rigidbody rb;

    RaycastHit slopehit;

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopehit, playerhight / 2 + 0.5f))
        {
            if (slopehit.normal != Vector3.up)
            {
                return true;
            } else{
                return false;
            }
        }
        return false;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundcheck.position, grounddistance, groundmask);

        

        MyInput();
        ControlDrag();
        controlspeed();
        controlcrouchspeed();

        if (Input.GetKeyDown(Crouchkey) && isGrounded)
        {
            crouch();
            crouching = true;
        } else if (Input.GetKeyUp(Crouchkey)){
            stopcrouch();
            crouching = false;
        }

        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            Jump();
        }

        SlopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopehit.normal);


    }

    void MyInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = oriantation.forward * verticalMovement + oriantation.right * horizontalMovement;  
    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(transform.up * jumpforce, ForceMode.Impulse);
    }

    void crouch()
    {
        transform.localScale = crouchscale;
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        if (rb.velocity.magnitude > 0.5f) {
            if (isGrounded) {
                rb.AddForce(oriantation.transform.forward * slideforce);
            }
        }
    }
    void stopcrouch()
    {
        transform.localScale = playerscale;
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
    }

    void controlcrouchspeed()
    {
        if (crouching && isGrounded && OnSlope())
        {
            
            rb.AddForce(Vector3.down * Time.deltaTime * 6000);
            return;  
        
            
        }
    }

    void controlspeed()
    {
        if (autosprint && isGrounded && !crouching || Input.GetKey(sprintkey) && isGrounded && !crouching)
        {
            moveSpeed = Mathf.Lerp(moveSpeed, sprintSpeed, acceleration * Time.deltaTime);
        } else if (grapple.isgappeld){
            moveSpeed = Mathf.Lerp(moveSpeed, grapplespeed, acceleration * Time.deltaTime);
        } else{
            moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, acceleration * Time.deltaTime);
        }
    }

    void ControlDrag()
    {
        if (isGrounded)
        {
            rb.drag = GroundDrag;
        } else {
            rb.drag = AirDrag;
        }
    }


    private void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        if (isGrounded && !OnSlope())
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * MovementMultiplier, ForceMode.Acceleration);
        } 
        else if (isGrounded && OnSlope()){
            rb.AddForce(SlopeMoveDirection.normalized * moveSpeed * MovementMultiplier, ForceMode.Acceleration);
        }
        else if (!isGrounded){
            rb.AddForce(moveDirection.normalized * moveSpeed * MovementMultiplier * airmultiplier, ForceMode.Acceleration);
        }
        
    }
}
