using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /* if (Input.GetMouseButtonDown(1))
          {
              Debug.Log("mouse down");
              RaycastHit hitInfo = new RaycastHit();
              bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
              if (hit)
              {
                  if (hitInfo.transform.gameObject.tag == "Player")
                  {
                      
                      GameObject cow = hitInfo.transform.gameObject;
                      MoveToClickNavMesh playerScript = cow.GetComponent<MoveToClickNavMesh>();
                      Debug.Log(playerScript.cowIndex);
                      MoveToClickNavMesh.curCowIndex = playerScript.cowIndex;
                  }
              }
          }*/
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("seen");
            if (MoveToClickNavMesh.curCowIndex == 0)
            {
                MoveToClickNavMesh.curCowIndex = 1;
            }
            else if (MoveToClickNavMesh.curCowIndex == 1)
            {
                MoveToClickNavMesh.curCowIndex = 0;
            }
            Debug.Log(MoveToClickNavMesh.curCowIndex);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("seen 0");
            MoveToClickNavMesh.curCowIndex = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            Debug.Log("seen 0");
            MoveToClickNavMesh.curCowIndex = 0;
        }
    }
}
