using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MooPoint : MonoBehaviour
{
    public int index;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                EnemyAI enemy = GameObject.FindGameObjectWithTag(index.ToString()).GetComponent<EnemyAI>();
                enemy.MooWander(transform.position);
            }
        }
    }
}
