using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CloseText : MonoBehaviour
{

    public GameObject textToClose;

    // Start is called before the first frame update
    void Start()
    {
        textToClose.SetActive(true);
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 1;
            textToClose.SetActive(false);
        }
    }
}
