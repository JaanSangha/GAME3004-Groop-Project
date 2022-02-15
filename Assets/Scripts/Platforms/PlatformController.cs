using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public bool isActive = false;
    public float desiredTime = 2f;
    [SerializeField]
    public AnimationCurve speedCurve;
    protected float elapsedTime;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
