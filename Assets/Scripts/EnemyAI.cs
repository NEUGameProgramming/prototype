using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public float enemySpeed = 5;

    GameObject[] wanderPoints;
    Vector3 tempWander = new Vector3(-10000, 0, 0);
    Vector3 nextDestination;

    int currentDestinationIndex = 0;
    bool destinationForward = false;

    public bool isStatic;
    NavMeshAgent mNav;

    void Start()
    {
        mNav = GetComponent<NavMeshAgent>();
        mNav.updateRotation = false;
        if (!isStatic)
        {
            wanderPoints = GameObject.FindGameObjectsWithTag("Wander Point" + gameObject.tag);
            FindNextPoint();
        }
    }

    void Update()
    {
        if (!isStatic && !GameManager.isGameOver)
        {
            if (Vector3.Distance(gameObject.transform.position, nextDestination) < .5f)
            {
                if (tempWander.x != -10000)
                {
                    tempWander.x = -10000;
                    FindNextPoint();
                }
            }
            if (nextDestination.x != 0 && nextDestination.y != 0 && nextDestination.z != 0)
            {
                FaceTarget(nextDestination);
                transform.position = Vector3.MoveTowards(transform.position, nextDestination, enemySpeed * Time.deltaTime);
            }
            FaceTarget(nextDestination);
            //transform.position = Vector3.MoveTowards(transform.position, nextDestination, enemySpeed * Time.deltaTime);
            mNav.SetDestination(nextDestination);

        }
    }

    void FindNextPoint()
    {
        if (tempWander.x != -10000)
        {
            nextDestination = tempWander;
        }
        else if (wanderPoints.Length > 0)
        {
            nextDestination = wanderPoints[currentDestinationIndex].transform.position;

            if (currentDestinationIndex == wanderPoints.Length - 1 || currentDestinationIndex == 0)
            {
                destinationForward = !destinationForward;
            }

            currentDestinationIndex += destinationForward ? 1 : -1;
        }
    }

    void FaceTarget(Vector3 target)
    {
        Vector3 directionToTarget = (target - transform.position).normalized;
        directionToTarget.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        // transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10 * Time.deltaTime);
        transform.rotation = Quaternion.LookRotation(mNav.velocity.normalized);
    }

    public void MooWander(Vector3 point)
    {
        tempWander = point;
        FindNextPoint();
    }
}
