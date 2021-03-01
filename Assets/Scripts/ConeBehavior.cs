using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeBehavior : MonoBehaviour
{
    public bool gameOver = false;
    public float lookCenter = 90f;
    public float lookRadius = 45f;
    public float lookSpeed = 1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, lookCenter + lookRadius * Mathf.Sin(Time.time / 2 * lookSpeed), 0));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cow"))
        {
            gameOver = true;
        }
    }
}
