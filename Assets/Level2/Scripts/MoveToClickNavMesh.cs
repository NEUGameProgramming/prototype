using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToClickNavMesh : MonoBehaviour
{
    private NavMeshAgent mNav;
    private bool moving;
    public static int curCowIndex = 0;
    public int cowIndex = 0;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        mNav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0) && (curCowIndex == cowIndex))
        {
            if (Physics.Raycast(ray, out hit))
            {
                mNav.destination = hit.point;
            }
            if (mNav.remainingDistance <= mNav.stoppingDistance)
            {
                moving = false;
                anim.SetInteger("animState", 1);
            }
            else
            {
                moving = true;
                anim.SetInteger("animeState", 0);
            }
        }
    }
}
