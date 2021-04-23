using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    // Start is called before the first frame update
    static public bool gamePause;
    public GameObject menu;


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(gamePause)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
    void PauseGame()
    {
        gamePause = true;
        Time.timeScale = 0f;
        menu.SetActive(true);
    }
    void ResumeGame()
    {
        gamePause = false;
        Time.timeScale = 1f;
        menu.SetActive(false);
    }
}
