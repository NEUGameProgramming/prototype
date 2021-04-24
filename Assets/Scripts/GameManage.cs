using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManage : MonoBehaviour
{
    public Text gameText;
    public string nextLevel;
    public AudioClip GameOverSFX;
    public AudioClip GameWinSFX;

    public int levelZoom;

    public static bool isGameOver = false;
    GameObject[] players;
    Vector3 safeZone;

    public static Transform curCow;

    void Start()
    {
        Camera.main.orthographicSize = levelZoom;
        isGameOver = false;
        safeZone = GameObject.FindGameObjectWithTag("Safety").transform.position;
        players = GameObject.FindGameObjectsWithTag("Player");
        gameText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver)
        {
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
        gameText.text = "GAME  OVER!";
        gameText.gameObject.SetActive(true);

        Invoke("LoadCurrentLevel", 5.224f);
    }

    public void LevelWon()
    {
        Camera.main.GetComponent<AudioSource>().Stop();
        isGameOver = true;
        AudioSource.PlayClipAtPoint(GameWinSFX, Camera.main.transform.position);
        gameText.text = "YOU  WIN!";
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

    bool AllCowsSafe()
    {
        foreach (GameObject cow in players)
        {
            if (Vector3.Distance(cow.transform.position, safeZone) > 12)
            {
                return false;
            }
        }
        return true;
    }

}
