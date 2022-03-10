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

    [SerializeField]
    private InventorySystem inventorySystem;

    float xInput;
    float zInput;
    bool isJumping;
    Vector3 playerScale;

    public float force = 1;

    public int Health = 100;
    public int Lives = 3;
    public int Score = 0;
    public float timeLeft = 300;

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
    public GameObject GameUIScreen;
    public TextMeshProUGUI GameOverText;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI timerText;
    public Image fadePlane;

    public GameObject UIHeartOne;
    public GameObject UIHeartTwo;
    public GameObject UIHeartThree;

    public Transform SpawnPoint;

    private void Awake()
    {
        Time.timeScale = 1;
        playerAnimator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
        playerScale = transform.localScale;
        transform.position = SpawnPoint.position;
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


        //transform.rotation = Quaternion.LookRotation(movement);

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

        // timer countdown
        timeLeft -= Time.deltaTime;
        timerText.text = timeLeft.ToString("F0");

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

        playerAnimator.SetFloat("Velocity", rigidBody.velocity.magnitude);
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
            SoundManager.instance.PlaySound(SFX.PlayerSFX.PLAYER_DAMAGE, this.gameObject);

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
        if (other.gameObject.tag == "item1")
        {
            SoundManager.instance.PlaySound(SFX.PlayerSFX.PICKUP, this.gameObject);
            inventorySystem.num1++;
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.tag == "item2")
        {
            SoundManager.instance.PlaySound(SFX.PlayerSFX.PICKUP, this.gameObject);
            inventorySystem.num2++;
            other.gameObject.SetActive(false);
        }
        if(other.gameObject.tag == "Respawn")
        {
            SpawnPoint = other.transform;
        }
        if (other.gameObject.tag == "Enemy") //same behaviour as obsticle
        {
            SoundManager.instance.PlaySound(SFX.PlayerSFX.PLAYER_DAMAGE, this.gameObject);

            Debug.Log("Ouch");//for checking collision with hazard

            //gives some knockback to player when colliding with hazard
            Vector3 pushDirection = other.transform.position - transform.position;

            pushDirection = -pushDirection.normalized;

            GetComponent<Rigidbody>().AddForce(pushDirection * force * 10);

            Health -= 10;

            LoseLife();
        }
        if (other.gameObject.tag == "BusSoundCol")
        {
            SoundManager.instance.PlaySound(SFX.PlayerSFX.BUS, this.gameObject);
        }
    }
    //gameover 
    public void GameOver(bool isDead)
    {
        if (isDead)
        {
            OnGameOver();
            GameOverText.text = "Game Over";
            ScoreText.text = Score.ToString();
        }
    }
    //win
    public void GameWin(bool isWin)
    {
        if(isWin)
        {
            OnGameOver();
            SoundManager.instance.PlaySound(SFX.PlayerSFX.PICKUP, this.gameObject);
            GameOverText.text = "You Win";
            ScoreText.text = Score.ToString();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Water" || collision.gameObject.tag == "Enemy")
        {
            SoundManager.instance.PlaySound(SFX.PlayerSFX.PLAYER_DAMAGE, this.gameObject);
            
            LoseLife();
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

        Respawn();
    }

    void Respawn()
    {
        transform.position = SpawnPoint.position;
        rigidBody.velocity = Vector3.zero;
    }

    void OnGameOver()
    {
        StartCoroutine(Fade(Color.clear, Color.cyan, 1));
        GameOverScreen.SetActive(true);
        GameUIScreen.SetActive(false);
        inventorySystem.gameObject.SetActive(false);
    }

    IEnumerator Fade(Color from, Color to, float time)
    {
        float speed = 3 / time;
        float percent = 0;

        while (percent < 1)
        {
            percent += Time.deltaTime * speed;
            fadePlane.color = Color.Lerp(from, to, percent);
            yield return null;
        }
        yield return new WaitForSeconds(0.06f);
        Time.timeScale = 0;
    }
}
