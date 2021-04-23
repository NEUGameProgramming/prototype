using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScan : MonoBehaviour
{

    public GameObject doorModel;

    public AudioClip doorSFX;

    public Material red;

    public Material green;

    public Material yellow;

    bool isScanned;

    Renderer render;


    // Start is called before the first frame update
    void Start()
    {
        doorModel.transform.position = new Vector3(doorModel.transform.position.x, -.67f, doorModel.transform.position.z);
        isScanned = false;
        render = GetComponent<Renderer>();
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

            if (!isScanned && CardCount.cardCounter > 0)
            {
                AudioSource.PlayClipAtPoint(doorSFX, Camera.main.transform.position);
                Debug.Log(doorAnim);
                doorAnim.Play("open");
                CardCount.cardCounter--;
                render.material = green;

            } else
            {
                render.material = red;
                Invoke("YellowChange", 2);
            }

        }
    }
    
    private void YellowChange()
    {
        render.material = yellow;
    }
}
