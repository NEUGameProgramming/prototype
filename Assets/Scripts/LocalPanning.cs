using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalPanning : MonoBehaviour
{
    public int edgeScrollingBoundary = 10;
    public float cameraSpeed = 10f;
    public float cameraDistance = 50f;
    public GameObject selectedObject = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (selectedObject)
        {

        }
        else
        {
            FreeCamera();
        }
    }

    void FreeCamera()
    {
        if ((Input.mousePosition.x > Screen.width - edgeScrollingBoundary) || (Input.GetKey("d")))
        {
            //transform.position += Vector3.right * cameraSpeed * Time.deltaTime;
            transform.position += Vector3.forward * cameraSpeed * Time.deltaTime;
        }
        if ((Input.mousePosition.x < 0 + edgeScrollingBoundary) || (Input.GetKey("a")))
        {
            //transform.position += Vector3.left * cameraSpeed * Time.deltaTime;
            transform.position += Vector3.back * cameraSpeed * Time.deltaTime;
        }
        if ((Input.mousePosition.y < 0 + edgeScrollingBoundary) || (Input.GetKey("s")))
        {
            transform.position += Vector3.down * cameraSpeed * Time.deltaTime;
        }
        if ((Input.mousePosition.y > Screen.height - edgeScrollingBoundary) || (Input.GetKey("w")))
        {
            transform.position += Vector3.up * cameraSpeed * Time.deltaTime;
        }
    }

    public void FocusOnTarget()
    {
        transform.position = selectedObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z) + Vector3.forward * cameraDistance;
    }

    public void SetFocus(GameObject focus)
    {
        selectedObject = focus;
    }
}
