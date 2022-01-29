using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;
    public float xPos;
    public float zPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        xPos = player.transform.position.x;
        zPos = player.transform.position.z - 8;
        transform.position = new Vector3(xPos, transform.position.y, zPos);
    }
}
