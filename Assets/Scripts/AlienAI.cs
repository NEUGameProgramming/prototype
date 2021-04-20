using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AlienAI : MonoBehaviour
{
    

    public enum FSMStates
    {
        PATROL, CHASE, ATTACK, DEAD
    }

    public FSMStates currentState;

    public float enemySpeed = 5;

    public int chaseDistance = 10;

    public float attackDistance = 5;

    public GameObject player;

    //public GameObject[] spellProjectiles;

    //public GameObject wandTip;

    //public float shootRate = 2.0f;

    public GameObject deadVFX;

    public Transform enemyEyes;

    public float fieldOfView = 45;

    public int wanderOffset = 0;

    public int alienNumber = 0;

    //public int damageAmount = 20;

    GameObject[] wanderPoints;

    Animator anim;

    Vector3 nextDestination;

    //bool isDead;

    //keeps track of what wander point this is heading towards
    int currentDestinationIndex = 0;

    float distanceToPlayer;

    float elapsedTime = 0;

    //EnemyHealth enemyHealth;

    int health;

    Transform deadTransform;

    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        //health = enemyHealth.currentHealth;

        /*
        if (health <= 0)
        {
            currentState = FSMStates.DEAD;
        }
        */

        switch (currentState)
        {
            case FSMStates.PATROL:
                UpdatePatrolState();
                break;
            case FSMStates.CHASE:
                UpdateChaseState();
                break;
            case FSMStates.ATTACK:
                UpdateAttackState();
                break;

        }

        elapsedTime += Time.deltaTime;


    }

    void Initialize()
    {
        wanderPoints = GameObject.FindGameObjectsWithTag("wanderpointAlien" + alienNumber);
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        //wandTip = GameObject.FindGameObjectWithTag("EnemyWandTip");
        currentState = FSMStates.PATROL;
        //enemyHealth = GetComponent<EnemyHealth>();
        //isDead = false;
        FindNextPoint();
    }

    void UpdatePatrolState()
    {
        //print("Patrolling!");

        //Sets animState int equal to 1 (which is set as the patrolling state
        anim.SetInteger("animStateAlien", 1);

        //resets how far this object should stop.
        //agent.stoppingDistance = 0;

        agent.speed = 3.5f;

        if (Vector3.Distance(transform.position, nextDestination) < 2)
        {
            FindNextPoint();
        }
        else if (distanceToPlayer <= chaseDistance && isPlayerInClearFOV())
        {
            currentState = FSMStates.CHASE;
        }

        FaceTarget(nextDestination);

        //moves from wander point to wander point
        //transform.position = Vector3.MoveTowards(transform.position, nextDestination, enemySpeed * Time.deltaTime);

        //SetDestination is apart of the AI library
        agent.SetDestination(nextDestination);
    }

    void UpdateChaseState()
    {
        /*
        //print("Chasing!");

        //Sets animState int equal to 1 (which is set as the patrolling state
        anim.SetInteger("animStateAlien", 2);

        agent.speed = 8;

        nextDestination = player.transform.position;

        //resets how far this object should stop.
        //agent.stoppingDistance = attackDistance;

        //agent.speed = 5;

        if (distanceToPlayer <= attackDistance)
        {
            currentState = FSMStates.ATTACK;
        }
        else if (distanceToPlayer > chaseDistance)
        {
            FindNextPoint();
            currentState = FSMStates.PATROL;
        }

        FaceTarget(nextDestination);

        //this line isn't actually needed because the animation has movement embedded to it 
        //transform.position = Vector3.MoveTowards(transform.position, nextDestination, enemySpeed * Time.deltaTime);
        agent.SetDestination(nextDestination);
        */
    }

    void UpdateAttackState()
    {
        /*
        //print("Attacking");
        anim.SetInteger("animStateAlien", 3);
        //EnemySpellCast();


        nextDestination = player.transform.position;

        if (distanceToPlayer <= attackDistance)
        {
            currentState = FSMStates.ATTACK;

        }
        else if (distanceToPlayer > attackDistance && distanceToPlayer <= chaseDistance)
        {
            currentState = FSMStates.CHASE;

        }
        else
        {
            currentState = FSMStates.PATROL;
        }

        FaceTarget(nextDestination);
        */
    }

    void UpdateDeadState()
    {
        //anim.SetInteger("animState", 4);
        deadTransform = gameObject.transform;
        //isDead = true;
        Destroy(gameObject, 0.5f);
    }

    void FindNextPoint()
    {
        //Allows us to continually iterate through all three points
        //(0+1) % 3 -> 1
        //(1+1) % 3 -> 2
        //(2+1) % 3 -> 0
        currentDestinationIndex = (currentDestinationIndex + 1 + wanderOffset) % wanderPoints.Length;

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
