using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPickup : MonoBehaviour
{
    private float startY;
    private float minY;
    private float maxY;
    private float moveDir = 0.5f;
    public bool FullRotation;

    private void Start()
    {
        startY = transform.position.y;
        minY = startY - 0.3f;
        maxY = startY + 0.3f;
    }
    // Update is called once per frame
    void Update()
    {
        if(FullRotation)
        {
            transform.Rotate(new Vector3(Time.deltaTime * 100, Time.deltaTime * 200, Time.deltaTime * 100));
        }
        else
        {
            transform.Rotate(new Vector3(0, 0, Time.deltaTime * 200));
        }

        if (transform.position.y >= maxY)
        {
            moveDir = -0.5f;
        }
        else if(transform.position.y <= minY)
        {
            moveDir = 0.5f;
        }

        transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime * moveDir, transform.position.z);

    }
}
