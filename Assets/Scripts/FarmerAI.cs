using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FarmerAI : MonoBehaviour
{
    public int test = 0;
    public GameObject test2;
    public float enemySpeed = 5;

    GameObject[] wanderPoints;
    Vector3 nextDestination;

    int currentDestinationIndex = 0;
    bool destinationForward = false;

    public bool isStatic;

    void Start()
    {
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
                FindNextPoint();
            }

            FaceTarget(nextDestination);
            transform.position = Vector3.MoveTowards(transform.position, nextDestination, enemySpeed * Time.deltaTime);
        }
    }

    void FindNextPoint()
    {
        nextDestination = wanderPoints[currentDestinationIndex].transform.position;

        if (currentDestinationIndex == wanderPoints.Length - 1 || currentDestinationIndex == 0)
        {
            destinationForward = !destinationForward;
        }

        currentDestinationIndex += (destinationForward ? 1 : -1);
    }

    void FaceTarget(Vector3 target)
    {
        Vector3 directionToTarget = (target - transform.position).normalized;
        directionToTarget.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10 * Time.deltaTime);
    }
}