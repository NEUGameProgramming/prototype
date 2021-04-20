﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToClickNavMesh : MonoBehaviour
{
    private NavMeshAgent mNav;
    public static int curCowIndex = 1;
    public int cowIndex = 0;
    public static GameObject cowObj;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        if (curCowIndex == cowIndex)
        {
            cowObj = gameObject;
        }
        mNav = GetComponent<NavMeshAgent>();

        anim = GetComponent<Animator>();
        anim.SetInteger("animState", 0);
    }

    // Update is called once per frame
    void Update()
    {

        if (!GameManager.isGameOver)
        {

            if (mNav.velocity != Vector3.zero)
            {
                anim.SetInteger("animState", 1);

            } else
            {
                anim.SetInteger("animState", 0);
            }




            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                curCowIndex = 1;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                curCowIndex = 2;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (curCowIndex == 2)
                {

                    curCowIndex = 1;

                }
                else
                {
                    curCowIndex = 2;
                }
            }

            //print(curCowIndex);


            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Input.GetMouseButtonDown(0) && (curCowIndex == cowIndex))
            {
                if (Physics.Raycast(ray, out hit))
                {
                    mNav.destination = hit.point;
                }
                //if (mNav.remainingDistance <= mNav.stoppingDistance)
                //{
                //    moving = false;
                //}
                //else
                //{
                //    moving = true;
                //}
            }

            if (curCowIndex == cowIndex)
            {
                cowObj = gameObject;
            }

        } else
        {
            anim.SetInteger("animState", 2);
        }
    }
}