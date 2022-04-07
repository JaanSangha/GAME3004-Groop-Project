using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardSFX : MonoBehaviour
{
    public AudioSource Spike;
    public AudioSource Saw;
    public AudioSource Axe;
    // Start is called before the first frame update
    void Start()
    {
        Spike = GetComponent<AudioSource>();
        Saw = GetComponent<AudioSource>();
        Axe = GetComponent<AudioSource>();
        InvokeRepeating("SpikeSound", 1f, 1f);
        InvokeRepeating("SawSound", 1f, 1f);
        InvokeRepeating("AxeSound", 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpikeSound()
    { // creates a function.
        Spike.Play();
    }
    public void SawSound()
    { // creates a function.
        Saw.Play();
    }
    public void AxeSound()
    { // creates a function.
        Axe.Play();
    }

}
