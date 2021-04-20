using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpin : MonoBehaviour
{

    public int spinSpeed = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * spinSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            CardCount.cardCounter++;
            gameObject.SetActive(false);
            print("adding 1");
        }
    }
}
