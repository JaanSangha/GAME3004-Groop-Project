using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    public Transform player;
    public PlayerController playerController;
    private NavMeshAgent agent;
    [SerializeField]
    private float EnemySightRange = 10;
    [SerializeField]
    private EnemyBehaviour EnemyState = EnemyBehaviour.PATROL;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        SwitchStates();
        float dist = Vector3.Distance(player.position, agent.transform.position);
        //Debug.Log("Distance to Player: " + dist);
        if (dist < EnemySightRange)
        {
            EnemyState = EnemyBehaviour.CHASE;
        

        }
        else if(dist >= EnemySightRange)
        {
          
           //StartCoroutine("Idle");
           // print("Idle finished, Switching to Patrol");
            EnemyState = EnemyBehaviour.PATROL;
        }
        //More will go here later
    }

    void SwitchStates()
    {
        //Messy, but this should work for now
        switch (EnemyState) 
        {
            case EnemyBehaviour.PATROL:
                agent.GetComponent<EnemyController>().enabled = false;
                agent.GetComponent<EnemyPatrol>().enabled = true;
                break;
            case EnemyBehaviour.CHASE:
                agent.GetComponent<EnemyPatrol>().enabled = false;
                agent.GetComponent<EnemyController>().enabled = true;
                break;
            //case EnemyBehaviour.ATTACK:
            //    break;
            //case EnemyBehaviour.IDLE:
            //    agent.GetComponent<EnemyController>().enabled = false;
            //    agent.GetComponent<EnemyPatrol>().enabled = false;
            //    break;

        }

    }
    //IEnumerator Idle()
    //{
    //    EnemyState = EnemyBehaviour.IDLE;
    //    anim.Play("IdleBattle");
    //    yield return new WaitForSeconds(2.0f);
    //}

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.gameObject.CompareTag("FootCollider") && playerController.isJumping)
    //    {
    //        playerController.isDamaging = true;
    //        this.enabled = false;
    //    }
    //}
    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("FootCollider"))
    //    {
    //        playerController.isDamaging = false;
    //    }
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("FootCollider"))
    //    {
    //        //this.enabled = false;
    //        Destroy(this);
    //    }
    //}
}
