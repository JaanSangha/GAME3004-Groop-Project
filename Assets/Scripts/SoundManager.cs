using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundsFX
{
    WALK,
    WALK_BRIDGE,
    JUMP,
    JUMP_LAND,
    JUMP_LAND_BRIDGE,
    PLAYER_DAMAGE
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioClip playerDamage;
    public AudioClip jumpUp, jumpLand, jumpLandBridge;
    public AudioClip walkingStep, walkingStepBridge;

    void Awake() 
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        
    }

    void Start()
    {
        // playerDamage = Resources.Load<AudioClip>("SFX/playerDamage");
        // jumpUp = Resources.Load<AudioClip>("sfx_jumpUp");
        // jumpLand = Resources.Load<AudioClip>("sfx_jumpLanding");
        // jumpLandBridge = Resources.Load<AudioClip>("sfx_jumpLandingBridge");
        // walkingStep = Resources.Load<AudioClip>("sfx_walkingStep");
        // walkingStepBridge = Resources.Load<AudioClip>("sfx_walkingStepBridge");
    }

    void Update()
    {
        
    }

    public void PlaySound(SoundsFX sound, GameObject audioSourceObj)
    {
        AudioSource audioSource = audioSourceObj.GetComponent<AudioSource>();

        if(audioSource == null) return;

        switch(sound)
        {
            case SoundsFX.WALK:
                if(audioSource.isPlaying == true) break;
                audioSource.PlayOneShot(walkingStep);
                break;
            case SoundsFX.WALK_BRIDGE:
                audioSource.clip = walkingStepBridge;
                if(audioSource.isPlaying == true) break;
                audioSource.Play();
                break;
            case SoundsFX.JUMP:
                audioSource.PlayOneShot(jumpUp);
                break;
            case SoundsFX.JUMP_LAND:
                audioSource.PlayOneShot(jumpLand);
                break;
            case SoundsFX.JUMP_LAND_BRIDGE:
                audioSource.PlayOneShot(jumpLandBridge);
                break;
            case SoundsFX.PLAYER_DAMAGE:
                audioSource.PlayOneShot(playerDamage);
                break;
        }
    }
}
