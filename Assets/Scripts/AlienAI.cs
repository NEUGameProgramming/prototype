using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AlienAI : MonoBehaviour
{
    
    public enum FSMStates
    {
        PATROL//, CHASE, ATTACK, DEAD
    }

    public FSMStates currentState;

    public int alienNumber = 0;
    GameObject[] wanderPoints;

    Animator anim;

    Vector3 nextDestination;

    //keeps track of what wander point this is heading towards
    int currentDestinationIndex = 0;

    NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case FSMStates.PATROL:
                UpdatePatrolState();
                break;

        }

    }

    void Initialize()
    {
        wanderPoints = GameObject.FindGameObjectsWithTag("wanderpointAlien" + alienNumber);
        anim = GetComponent<Animator>();
        currentState = FSMStates.PATROL;
        FindNextPoint();
    }

    void UpdatePatrolState()
    {
        anim.SetInteger("animStateAlien", 1);
        
        if (Vector3.Distance(transform.position, nextDestination) < 2)
        {
            FindNextPoint();
        }

        FaceTarget(nextDestination);
        agent.SetDestination(nextDestination);
    }

    void FindNextPoint()
    {
        currentDestinationIndex = (currentDestinationIndex + 1) % wanderPoints.Length;

        //stores first wander point in array and returns position of it
        nextDestination = wanderPoints[currentDestinationIndex].transform.position;

        agent.SetDestination(nextDestination);
    }

    void FaceTarget(Vector3 target)
    {
        //gives direction of target vector from this vector
        Vector3 directionTarget = (target - transform.position).normalized;
        //Vector3 directionTarget = transform.position;

        //directionTarget.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(directionTarget);
        //Quaternion lookRotation = Quaternion.LookRotation(directionTarget);

        //Slerp pretty much works like interpolation
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10 * Time.deltaTime);
    }


    /*
    //allows us to visualize what this is seeing/looking-for
    private void OnDrawGizmos()
    {
        //attack visual sphere
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);

        //Chase visual sphere 
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);

        
        //represents the objects vision
        Vector3 frontRayPoint = enemyEyes.position + (enemyEyes.forward * chaseDistance);
        Vector3 leftRayPoint = Quaternion.Euler(0, fieldOfView * 0.5f, 0) * frontRayPoint;
        Vector3 rightRayPoint = Quaternion.Euler(0, -fieldOfView * 0.5f, 0) * frontRayPoint;
        

        
        //draws line that shows enemies FOV
        Debug.DrawLine(enemyEyes.position, frontRayPoint, Color.cyan);
        Debug.DrawLine(enemyEyes.position, leftRayPoint, Color.red);
        Debug.DrawLine(enemyEyes.position, rightRayPoint, Color.red);
        
    }
    */

    /*
    bool isPlayerInClearFOV()
    {

        RaycastHit hit;
        Vector3 directionToPlayer = player.transform.position - enemyEyes.position;

        if (Vector3.Angle(directionToPlayer, enemyEyes.forward) <= fieldOfView)
        {
            if (Physics.Raycast(enemyEyes.position, directionToPlayer, out hit, chaseDistance))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    //print("Player in sight!");
                    return true;
                }
            }
        }

        return false;
    }
    */

    /*
    private void OnCollisionEnter(Collision collision)
    {

        print("applying damage");
        if (collision.gameObject.CompareTag("Player"))// && PlayerHealthFPS.currentHealth > 0)
        {

            //apply damage
            var playerHealth = collision.gameObject.GetComponent<PlayerHealthFPS>();

            playerHealth.TakeDamage(UnityEngine.Random.Range(5, 20));
        }
    }
    */
    
}
