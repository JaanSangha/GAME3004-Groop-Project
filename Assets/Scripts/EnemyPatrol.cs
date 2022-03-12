// Patrol.cs
using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public class EnemyPatrol : MonoBehaviour
{

    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;
    //public Animator anim;

    void Start()
    {
        //anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = true;
        
        GotoNextPoint();
    }


    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }


    void Update()
    {
        // Choose the next destination point when the agent gets
        // close to the current one.
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            //StartCoroutine("Idle");
            GotoNextPoint();

        }
    
       
    }
    //IEnumerator Idle()
    //{
    //    anim.Play("IdleBattle");
    //    yield return new WaitForSeconds(2.0f); 
    //}
}