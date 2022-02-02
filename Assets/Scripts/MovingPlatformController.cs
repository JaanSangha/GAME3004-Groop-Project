using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MovingPlatformController : MonoBehaviour
{
    public Transform PointA;
    public Transform PointB;
    
    public bool isActive;
    
    public float desiredTime = 3f;
    [SerializeField]
    private AnimationCurve speedCurve;
    private float elapsedTime;

    void Start()
    {
        //speedCurve = new AnimationCurve();
        //PointA.position = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        elapsedTime += Time.deltaTime;
        float percentComplete = elapsedTime / desiredTime;
        transform.position = Vector3.LerpUnclamped(PointA.position, PointB.position, 
                speedCurve.Evaluate(Mathf.PingPong(percentComplete, 1)));
        
    }
}