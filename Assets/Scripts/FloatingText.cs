using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    RectTransform rt;
    float upperY = 5;
    float bottomY = -5;
    float startTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        rt = gameObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        float lerpY = Mathf.Lerp(upperY, bottomY, Mathf.PingPong(Time.time, 1f));
        transform.position = new Vector2(transform.position.x, lerpY);
    }
}
