using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float levelDuration = 30.0f;
    public Text timerText;
    float countdown;
    public Text gameText;
    public string nextLevel;
    public AudioClip GameOverSFX;
    public AudioClip GameWinSFX;

    public static bool isGameOver = false;
    GameObject[] players;
    Vector3 safeZone;

    public static Transform curCow;

    void Start()
    {
        isGameOver = false;
        safeZone = GameObject.FindGameObjectWithTag("Safety").transform.position;
        players = GameObject.FindGameObjectsWithTag("Player");
        countdown = levelDuration;
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

        if (!isGameOver)
        {
            if (countdown > 0)
            {
                countdown -= Time.deltaTime;
            }
            else
            {
                countdown = 0.0f;
                LevelLost();
            }

            SetTimerText();

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

            if (AllCowsSafe())
            {
                LevelWon();
            }
        }
    }

    void SetTimerText()
    {
        timerText.text = countdown.ToString("f2");
    }

    bool AllCowsSafe()
    {
        foreach (GameObject cow in players)
        {
            if (Vector3.Distance(cow.transform.position, safeZone) > 8)
            {
                return false;
            }
        }
        return true;
    }

    public void LevelLost()
    {
        Camera.main.GetComponent<AudioSource>().Stop();
        AudioSource.PlayClipAtPoint(GameOverSFX, Camera.main.transform.position);
        isGameOver = true;
        gameText.text = "GAME OVER!";
        gameText.gameObject.SetActive(true);

        Invoke("LoadCurrentLevel", 5.224f);
    }

    public void LevelWon()
    {
        Camera.main.GetComponent<AudioSource>().Stop();
        isGameOver = true;
        AudioSource.PlayClipAtPoint(GameWinSFX, Camera.main.transform.position);
        gameText.text = "YOU WIN!";
        gameText.gameObject.SetActive(true);

        if (!string.IsNullOrEmpty(nextLevel))
        {
            Invoke("LoadNextLevel", 2.325f);
        }
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }

    void LoadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
