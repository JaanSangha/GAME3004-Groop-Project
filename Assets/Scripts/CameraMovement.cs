using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;
    public float xPos;
    public float zPos;
    public float yPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        xPos = player.transform.position.x;
        zPos = player.transform.position.z - 8;
        yPos = player.transform.position.y + 2;
        transform.position = new Vector3(player.position.x, player.position.y + 2, player.position.z -8);
    }
}
