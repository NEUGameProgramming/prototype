using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
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
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver)
        {
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
