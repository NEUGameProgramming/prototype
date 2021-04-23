using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MoveToClickNavMesh : MonoBehaviour
{
    private NavMeshAgent mNav;
    public static int curCowIndex = 1;
    public int cowIndex = 0;
    public static GameObject cowObj;

    public GameObject cowPanel;
    Button[] buttons;

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

        buttons = cowPanel.GetComponentsInChildren<Button>();
        UpdateCowUI();
    }

    // Update is called once per frame
    void Update()
    {

        if (!GameManager.isGameOver)
        {

            int previousCowUI = curCowIndex;
   

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

            if (previousCowUI != curCowIndex)
            {
                UpdateCowUI();
            }

        } else
        {
            anim.SetInteger("animState", 2);
        }
    }

    void UpdateCowUI()
    {
        int i = 1;

        foreach(Button cowIcon in buttons)
        {
            if (i == curCowIndex)
            {
                cowIcon.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
            }
            else
            {
                cowIcon.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
            }

            i++;
        }
    }
}
