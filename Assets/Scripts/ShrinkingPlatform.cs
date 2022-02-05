using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkingPlatform : MonoBehaviour
{
    private Vector3 originalScale;
    private Vector3 finalScale;
    public bool isActive = false;
    public float desiredTime = 2f;
    public float percentOfOriginalSize = 1f;
    public AnimationCurve speedCurve;


    private float elapsedTime;
    private GameObject playerCollided;
    private Vector3 collisionOriginalScale;
    private Collider platformCollider;

    void Start()
    {
        originalScale = transform.localScale;
        finalScale = Vector3.right + Vector3.up + (Vector3.forward * percentOfOriginalSize / 100f);

        platformCollider = transform.GetComponentInChildren<Collider>();
    }

    void Update()
    {
        // if(transform.localScale.z > percentOfOriginalSize / 100f)
        // {
        //     transform.localScale -= Vector3.forward * Time.deltaTime / desiredTime;
        // }

        if(isActive == true) Shrink();
    }

    void Shrink()
    {
        elapsedTime += Time.deltaTime;
        float percentComplete = elapsedTime / desiredTime;
        transform.localScale = Vector3.Lerp(originalScale, finalScale, 
                speedCurve.Evaluate(Mathf.PingPong(percentComplete, 1)));

        // z scaling goes from 0 to 1. if it reaches close to finalScale z plus 5% of 1.
        if(transform.localScale.z < finalScale.z + 5.0f/100.0f)
        {
            platformCollider.enabled = false;
        }
        else if(transform.localScale.z >= finalScale.z + 5.0f/100.0f)
        {
            platformCollider.enabled = true;
        }
    }

    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player collision");
            isActive = true;

            // we need the player gameobject for the shrink() function
            playerCollided = other.gameObject;
            collisionOriginalScale = playerCollided.transform.localScale;
            
            //other.transform.parent = this.transform;
        }
    }

    private void OnCollisionExit(Collision other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            isActive = false;
            //other.transform.SetParent(null);
        }
    }
}
