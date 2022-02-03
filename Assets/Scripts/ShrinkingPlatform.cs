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

    void Start()
    {
        originalScale = transform.localScale;
        finalScale = Vector3.right + Vector3.up + (Vector3.forward * percentOfOriginalSize / 100f);
    }

    void Update()
    {
        // if(transform.localScale.z > percentOfOriginalSize / 100f)
        // {
        //     transform.localScale -= Vector3.forward * Time.deltaTime / desiredTime;
        // }

        Shrink();
    }

    void Shrink()
    {
        
        elapsedTime += Time.deltaTime;
        float percentComplete = elapsedTime / desiredTime;
        transform.localScale = Vector3.Lerp(originalScale, finalScale, 
                speedCurve.Evaluate(Mathf.PingPong(percentComplete, 1)));
    }

    // NOTE: WRONG, Collision happens on the subobject "Bridge". Revision needed
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
