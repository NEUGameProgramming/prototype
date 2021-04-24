using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeBehavior : MonoBehaviour
{
    public float lookRadius = 45f;
    public float lookCenter = 90f;
    public float lookSpeed = 1;
    float colliderCenterY = 0.7879247f;
    float colliderSizeY = 1.58784f;
    float colliderSizeZ = 2.204834f;
    int raysVertical = 20;
    int raysHorizontal = 20;

    Vector3 farmerPosition;
    Vector3 cowPosition;
    Vector3 direction;
    Vector3 perpendicular;

    GameManage manager;

    public bool rotation;

    void Start()
    {
        manager = FindObjectOfType<GameManage>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rotation && !GameManage.isGameOver)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, lookCenter + lookRadius * Mathf.Sin(Time.time / 2 * lookSpeed), 0));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!GameManage.isGameOver && other.CompareTag("Player"))
        {
            farmerPosition = gameObject.transform.position;
            cowPosition = other.transform.position;
            direction = cowPosition - farmerPosition;
            perpendicular = Vector3.Cross(Vector3.up, direction).normalized;
            gameObject.GetComponent<MeshCollider>().enabled = false;
            CastRays();
        }
    }

    void CastRays()
    {
        bool stopCasting = false;
        for (int i = -raysHorizontal / 2; i < raysHorizontal / 2; i++)
        {
            if (stopCasting)
            {
                break;
            }
            Vector3 offset = perpendicular * -3 * i * colliderSizeZ / raysHorizontal;
            for (int j = 0; j < raysVertical; j++)
            {
                if (stopCasting)
                {
                    break;
                }
                float y = (colliderCenterY - colliderSizeY / 2) + colliderSizeY / raysVertical * j;
                Vector3 rayDir = cowPosition - farmerPosition + new Vector3(offset.x, 2 * y, offset.z);
                //Debug.DrawRay(farmerPosition, rayDir, Color.red, 20);
                RaycastHit hit;
                if (Physics.Raycast(farmerPosition, rayDir, out hit, Vector3.Distance(farmerPosition, cowPosition) + 5))
                {
                    if (hit.collider.gameObject.CompareTag("Player"))
                    {
                        stopCasting = true;
                        manager.LevelLost();
                    }
                }
            }
        }
        gameObject.GetComponent<MeshCollider>().enabled = true;
    }
}
