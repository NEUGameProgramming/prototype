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
    // Start is called before the first frame update
    void Start()
    {
        mNav = GetComponent<NavMeshAgent>();

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
            }
            else
            {
                moving = true;
            }
        }
    }
}
