using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalPanning : MonoBehaviour
{
    public int edgeScrollingBoundary = 10;
    public float cameraSpeed = 20f;
    public GameObject selectedObject;
    Vector3 rotateLock = new Vector3(-10000, 0, 0);
    Vector3 lockDistance;

    public AudioClip mooSFX;

    float currentRotation;

    // Start is called before the first frame update
    void Start()
    {
        currentRotation = Camera.main.transform.eulerAngles.y;
        lockDistance = new Vector3(Mathf.Sin((currentRotation + 180) * Mathf.PI / 180) * 87, 50f,
            Mathf.Cos((currentRotation + 180) * Mathf.PI / 180) * 91);

        selectedObject = MoveToClickNavMesh.cowObj;
        FocusOnTarget(selectedObject.transform.position);
        print("PANNING");
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManage.isGameOver)
        {
            selectedObject = MoveToClickNavMesh.cowObj;

            if (Input.GetKeyDown("f") && selectedObject)
            {
                FocusOnTarget(selectedObject.transform.position);
            }
            else
            {
                FreeCamera();
            }
        }
    }

    void FreeCamera()
    {
        ZoomCamera(Input.GetAxis("Mouse ScrollWheel"));

        if (Input.GetKey(KeyCode.R))
        {
            RotateCamera();
        }
        else
        {
            rotateLock = new Vector3(-10000, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            AudioSource.PlayClipAtPoint(mooSFX, Camera.main.transform.position);
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

    public void FocusOnTarget(Vector3 targetPos)
    {
        Vector3 newPos = targetPos + lockDistance;
        newPos.y = 50f;
        transform.position = newPos;
    }


    void RotateCamera()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentRotation += 30 * Time.deltaTime;
        }
        else
        {
            currentRotation -= 30 * Time.deltaTime;
        }
        transform.rotation = Quaternion.Euler(30f, currentRotation, 0f);
        lockDistance = new Vector3(Mathf.Sin((currentRotation + 180) * Mathf.PI / 180) * 87, 50f,
            Mathf.Cos((currentRotation + 180) * Mathf.PI / 180) * 91);
        if (rotateLock.x == -10000)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0));
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 3))
            {
                Vector3 point = hit.point;
                point.y = 0;
                rotateLock = point;
            }
            else
            {
                FocusOnTarget(selectedObject.transform.position);
            }
        }
        else
        {
            FocusOnTarget(rotateLock);
        }
    }

    void ZoomCamera(float scroll)
    {
        if ((Camera.main.orthographicSize > 5 && scroll < 0) || (Camera.main.orthographicSize < 100 && scroll > 0))
        {
            Camera.main.orthographicSize +=  2 * scroll;
        }
    }
}
