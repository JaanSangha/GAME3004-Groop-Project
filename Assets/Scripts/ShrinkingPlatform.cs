using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkingPlatform : MonoBehaviour
{
    private Vector3 originalScale;
    public bool isActive;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        if(transform.localScale.z > 0.01f)
        {
            transform.localScale -= new Vector3(0, 0, 0.1f) * Time.deltaTime;
        }
    }
}
