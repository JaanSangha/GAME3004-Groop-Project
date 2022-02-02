using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MovingPlatform : MonoBehaviour
{
    public Transform PointA;
    public Transform PointB;
    
    public bool isActive = false;
    
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
        transform.position = Vector3.Lerp(PointA.position, PointB.position, 
                speedCurve.Evaluate(Mathf.PingPong(percentComplete, 1)));
        
    }

    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player collision");
            isActive = true;
            other.transform.parent = this.transform;
        }
    }

    private void OnCollisionExit(Collision other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            isActive = false;
            other.transform.SetParent(null);
        }
    }
}