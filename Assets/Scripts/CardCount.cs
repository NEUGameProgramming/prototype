using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardCount : MonoBehaviour
{

    public static int cardCounter = 0;

    public GameObject keyPanel;

    bool isUsed = false;

    Button[] keys;

    // Start is called before the first frame update
    void Start()
    {
        keys = keyPanel.GetComponentsInChildren<Button>();
        keyPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isUsed)
        {
            if (cardCounter > 0)
            {
                isUsed = true;
                keyPanel.SetActive(true);
                SetKeys();
            }
            else
            {
                keyPanel.SetActive(false);
            }
        }
    }

    void SetKeys()
    {
        int i = 0;

        foreach (Button keyIcon in keys)
        {
            if (i < cardCounter)
            {
                keyIcon.transform.localScale = new Vector3(1, 1, 1);
            } else
            {
                keyIcon.transform.localScale = new Vector3(0, 0, 0);
            }

            i++;
        }
    }
}
