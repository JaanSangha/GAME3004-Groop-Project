using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    private float EnemyRange = 10;
    [SerializeField]
    private EnemyBehaviour EnemyState = EnemyBehaviour.PATROL;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        SwitchStates();
        float dist = Vector3.Distance(player.position, agent.transform.position);
        print("Distance to Player: " + dist);
        if (dist < EnemyRange)
        {
            EnemyState = EnemyBehaviour.CHASE;
        

        }
        else if(dist >= EnemyRange)
        {
          
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
            //    break;

        }

    }
}
