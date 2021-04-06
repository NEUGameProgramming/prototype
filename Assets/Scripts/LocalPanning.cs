using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalPanning : MonoBehaviour
{
    public int edgeScrollingBoundary = 10;
    public float cameraSpeed = 20f;
    public GameObject selectedObject = null;
    Vector3 lockDistance = new Vector3(60f, 37f, 33f);

    float currentRotation;

    // Start is called before the first frame update
    void Start()
    {
        selectedObject = MoveToClickNavMesh.cowObj;
        FocusOnTarget();

        currentRotation = -120f;
    }

    // Update is called once per frame
    void Update()
    {
        selectedObject = MoveToClickNavMesh.cowObj;

        if (Input.GetKeyDown("f") && selectedObject)
        {
            FocusOnTarget();
        }
        else
        {
            FreeCamera();
        }
    }

    void FreeCamera()
    {
        ZoomCamera(Input.GetAxis("Mouse ScrollWheel"));

        if (Input.GetKeyDown("r"))
        {
            RotateCamera();
        }
        if ((Input.mousePosition.x > Screen.width - edgeScrollingBoundary) || (Input.GetKey("d")))
        {
            //transform.position += Vector3.right * cameraSpeed * Time.deltaTime;
            transform.position += new Vector3(Mathf.Sin((currentRotation + 90f) * Mathf.PI / 180), 0,
                Mathf.Cos((currentRotation + 90f)* Mathf.PI / 180)).normalized * cameraSpeed * Time.deltaTime;
        }
        if ((Input.mousePosition.x < 0 + edgeScrollingBoundary) || (Input.GetKey("a")))
        {
            //transform.position += Vector3.left * cameraSpeed * Time.deltaTime;
            transform.position -= new Vector3(Mathf.Sin((currentRotation + 90f) * Mathf.PI / 180), 0,
                Mathf.Cos((currentRotation + 90f) * Mathf.PI / 180)).normalized * cameraSpeed * Time.deltaTime;
        }
        if ((Input.mousePosition.y < 0 + edgeScrollingBoundary) || (Input.GetKey("s")))
        {
            transform.position -= new Vector3(Mathf.Sin(currentRotation * Mathf.PI / 180), 0,
                Mathf.Cos(currentRotation * Mathf.PI / 180)).normalized * cameraSpeed * Time.deltaTime * 1.3f;
        }
        if ((Input.mousePosition.y > Screen.height - edgeScrollingBoundary) || (Input.GetKey("w")))
        {
            transform.position += new Vector3(Mathf.Sin(currentRotation * Mathf.PI / 180), 0,
                Mathf.Cos(currentRotation * Mathf.PI / 180)).normalized * cameraSpeed * Time.deltaTime * 1.3f;
        }
    }

    public void FocusOnTarget()
    {
        transform.position = selectedObject.transform.position + lockDistance;
    }

    void RotateCamera()
    {
        currentRotation -= 90f;
        transform.rotation = Quaternion.Euler(30f, currentRotation, 0f);
    }

    void ZoomCamera(float scroll)
    {
        if ((Camera.main.orthographicSize > 5 && scroll < 0) || (Camera.main.orthographicSize < 100 && scroll > 0))
        {
            Camera.main.orthographicSize += scroll;
        }
    }
}
