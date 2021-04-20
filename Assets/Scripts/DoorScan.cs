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

    Renderer renderer;


    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
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

            if (CardCount.cardCounter > 0)
            {
                AudioSource.PlayClipAtPoint(doorSFX, Camera.main.transform.position);
                doorAnim.Play("open");
                CardCount.cardCounter--;
                renderer.material = green;
            } else
            {
                renderer.material = red;
                Invoke("YellowChange", 2);
            }

        }
    }
    
    private void YellowChange()
    {
        renderer.material = yellow;
    }
}
