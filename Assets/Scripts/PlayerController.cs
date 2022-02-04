using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    private Animator playerAnimator;
    private Rigidbody rigidBody;

    float xInput;
    float zInput;
    bool isJumping;
    Vector3 playerScale;

    public float force = 1;

    public int Health = 100;
    public int Lives = 3;
    public int Score = 0;

    [Header("Movement Properties")]
    public float moveSpeed = 5.0f;
    public float gravity = -30.0f;
    public float jumpHeight = 5.0f;
    public Vector3 velocity;

    [Header("Ground Detection Properties")]
    public Transform groundCheck;
    public float groundRadius = 0.4f;
    public LayerMask groundMask;
    public bool isGrounded;

    [Header("Text UI")]
    public GameObject GameOverScreen;
    public TextMeshProUGUI GameOverText;
    public TextMeshProUGUI ScoreText;

    public GameObject UIHeartOne;
    public GameObject UIHeartTwo;
    public GameObject UIHeartThree;

    private void Awake()
    {
        Time.timeScale = 1;
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

            if(isGrounded) SoundManager.instance.PlaySound(SFX.PlayerSFX.WALK, this.gameObject);
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
            SoundManager.instance.PlaySound(SFX.PlayerSFX.JUMP, this.gameObject);
        }

        playerAnimator.SetFloat("XVelocity", xVelocity);
        playerAnimator.SetFloat("ZVelocity", zVelocity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "hazard")
        {
            Debug.Log("Ouch");//for checking collision with hazard

            //gives some knockback to player when colliding with hazard
            Vector3 pushDirection = other.transform.position - transform.position;

            pushDirection = -pushDirection.normalized;

            GetComponent<Rigidbody>().AddForce(pushDirection * force * 100);

            Health -= 10;

            LoseLife();
        }
        if (other.gameObject.tag == "goal")
        {
            Debug.Log("Win");

            GameWin(true);    
  
        }
    }
    //gameover 
    public void GameOver(bool isDead)
    {
        if (isDead)
        {
            GameOverScreen.SetActive(true);
            GameOverText.text = "Game Over";
            ScoreText.text = Score.ToString();
            Time.timeScale = 0;
        }
    }
    //win
    public void GameWin(bool isWin)
    {
        if(isWin)
        {
            GameOverScreen.SetActive(true);
            GameOverText.text = "You Win";
            ScoreText.text = Score.ToString();
            Time.timeScale = 0;
        }
    }

    void LoseLife()
    {
        Lives--;

        if (Lives == 3)
        {
            UIHeartOne.SetActive(true);
            UIHeartTwo.SetActive(true);
            UIHeartThree.SetActive(true);
        }
        else if(Lives == 2)
        {
            UIHeartOne.SetActive(true);
            UIHeartTwo.SetActive(true);
            UIHeartThree.SetActive(false);
        }
        else if (Lives == 1)
        {
            UIHeartOne.SetActive(true);
            UIHeartTwo.SetActive(false);
            UIHeartThree.SetActive(false);
        }
        if (Lives < 1)
        {
            UIHeartOne.SetActive(false);
            UIHeartTwo.SetActive(false);
            UIHeartThree.SetActive(false);
            GameOver(true);
        }
    }
}
