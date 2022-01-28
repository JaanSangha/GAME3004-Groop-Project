using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator playerAnimator;
    private Rigidbody rigidBody;

    float xInput;
    float zInput;
    bool isJumping;
    Vector3 playerScale;

    [Header("Movement Properties")]
    public float moveSpeed = 5.0f;
    public float gravity = -30.0f;
    public float jumpHeight = 3.0f;
    public Vector3 velocity;

    [Header("Ground Detection Properties")]
    public Transform groundCheck;
    public float groundRadius = 0.4f;
    public LayerMask groundMask;
    public bool isGrounded;

    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
        playerScale = transform.localScale;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector3(xInput, 0.0f, zInput);

        isGrounded = Physics.CheckSphere(groundCheck.position, groundRadius, groundMask);

        if (isGrounded)
        {
            isJumping = false;
        }

        xInput = Input.GetAxis("Horizontal");
        zInput = Input.GetAxis("Vertical");

        playerAnimator.SetBool("IsJumping", isJumping);


        transform.rotation = Quaternion.LookRotation(movement);

        float horizontalInput = Input.GetAxis("Vertical");
        float verticalInput = Input.GetAxis("Horizontal");


        //face the way player moves
        Vector3 moveDirection = new Vector3(verticalInput, 0, horizontalInput);

        if (moveDirection.sqrMagnitude > 0.001f)
        {
            var desiredRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * 10);
        }

    }

    private void FixedUpdate()
    {
        float xVelocity = xInput * moveSpeed;
        float zVelocity = zInput * moveSpeed;

        //walk
        rigidBody.velocity = new Vector3(xVelocity, rigidBody.velocity.y, zVelocity);

        //jump
        if (Input.GetButton("Jump") && isGrounded)
        {
            rigidBody.velocity = new Vector3(xVelocity, Mathf.Sqrt(jumpHeight * -2.0f * gravity), zVelocity);
            isJumping = true;
        }

        playerAnimator.SetFloat("XVelocity", xVelocity);
        playerAnimator.SetFloat("ZVelocity", zVelocity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }
}
