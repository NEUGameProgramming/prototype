﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorOpen : MonoBehaviour
{

    public GameObject doorModel;

    public GameObject cow;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Vector3 doorPos = door.transform.position;
        Animation doorAnim = doorModel.gameObject.GetComponent<Animation>();

        if (other.gameObject.CompareTag("Player"))
        {
            print("Moving door");
            doorAnim.Play("open");
            
        }
    }
}
