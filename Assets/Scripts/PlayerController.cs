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
    float xVelocity;
    float zVelocity;
    bool isJumping;
    Vector3 playerScale;
    private float powerupTime = 0;
    private float maxPowerupTime = 5;
    public bool isInvincible;
    public bool isBoosted;
    private bool checkpointOneReached;
    private bool checkpointTwoReached;
    public float moveSpeed = 5.0f;

    public GameObject RightRunningShoe;
    public GameObject LeftRunningShoe;
    public Material playerMaterial;
    public ParticleSystem runningParticles;
    public ParticleSystem CheckpointOneParticles;
    public ParticleSystem CheckpointTwoParticles;

    public float force = 1;

    public int Health = 100;
    public int Lives = 3;
    public int Score = 0;
    public float timeLeft = 300;

    [Header("Movement Properties")]
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
    public GameObject GameOverBG;
    public TextMeshProUGUI GameOverText;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI timerText;
    public Image fadePlane;

    [Header("OnScreen Controls")]
    //public GameObject onScreenControls;
    public Joystick leftJoystick;
    public GameObject miniMap;

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
        RightRunningShoe.SetActive(false);
        LeftRunningShoe.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 movement = new Vector3(xInput, 0.0f, zInput);

        isGrounded = Physics.CheckSphere(groundCheck.position, groundRadius, groundMask);

        if (isGrounded)
        {
            isJumping = false;
        }

        xInput = Input.GetAxis("Horizontal") + leftJoystick.Horizontal;
        zInput = Input.GetAxis("Vertical") + leftJoystick.Vertical;

        playerAnimator.SetBool("IsJumping", isJumping);


        //transform.rotation = Quaternion.LookRotation(movement);

        float horizontalInput = Input.GetAxis("Vertical") + leftJoystick.Vertical;
        float verticalInput = Input.GetAxis("Horizontal") + leftJoystick.Horizontal;


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

        if(isBoosted)
        {
            powerupTime += Time.deltaTime;
            if (powerupTime > maxPowerupTime)
            {
                isBoosted = false;
                moveSpeed = 5;
                LeftRunningShoe.SetActive(false);
                RightRunningShoe.SetActive(false);
                runningParticles.Stop();
            }
        }
        if (isInvincible)
        {
            powerupTime += Time.deltaTime;
            if (powerupTime > maxPowerupTime)
            {
                isInvincible = false;
                playerMaterial.color = new Color(.7f, .8f, .8f);
            }
        }
    }

    private void FixedUpdate()
    {
        xVelocity = xInput * moveSpeed;
        zVelocity = zInput * moveSpeed;

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
        if (!isInvincible)
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
        if (other.gameObject.tag == "item3")
        {
            SoundManager.instance.PlaySound(SFX.PlayerSFX.PICKUP, this.gameObject);
            GainLife();
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.tag == "CheckPointOne")
        {
            if (!checkpointOneReached)
            {
                SpawnPoint = other.transform;
                CheckpointOneParticles.Play();
                CheckpointTwoParticles.Play();
                checkpointOneReached = true;
            }
        }
        if (other.gameObject.tag == "CheckPointTwo")
        {
            if (!checkpointTwoReached)
            {
                SpawnPoint = other.transform;
                CheckpointOneParticles.Play();
                CheckpointTwoParticles.Play();
                checkpointTwoReached = true;
            }
        }
        if (!isInvincible)
        {
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

    void GainLife()
    {
        if(Lives <3)
        {
            Lives++;
        }

        if (Lives == 3)
        {
            UIHeartOne.SetActive(true);
            UIHeartTwo.SetActive(true);
            UIHeartThree.SetActive(true);
        }
        else if (Lives == 2)
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

    void Respawn()
    {
        transform.position = SpawnPoint.position;
        rigidBody.velocity = Vector3.zero;
    }

    void OnGameOver()
    {
        StartCoroutine(Fade(Color.clear, Color.white, 1));
        GameOverScreen.SetActive(true);
        GameOverBG.SetActive(true);
        GameUIScreen.SetActive(false);
        inventorySystem.gameObject.SetActive(false);
    }

    public void BoostPowerUp()
    {
        moveSpeed = moveSpeed * 2;
        isBoosted = true;
        powerupTime = 0;
        LeftRunningShoe.SetActive(true);
        RightRunningShoe.SetActive(true);
        runningParticles.Play();
    }
    public void InvinciblePowerUp()
    {
        isInvincible = true;
        powerupTime = 0;
        playerMaterial.color = new Color(0, 1, 1);
    }

    public void OnJumpPressed()
    {
        if (isGrounded)
        {
            rigidBody.velocity = new Vector3(xVelocity, Mathf.Sqrt(jumpHeight * -2.0f * gravity), zVelocity);
            isJumping = true;
            SoundManager.instance.PlaySound(SFX.PlayerSFX.JUMP, this.gameObject);
        }
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
