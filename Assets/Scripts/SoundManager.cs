using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    AudioSource thisAudio;
    [SerializeField, Tooltip("Camera speakers are the global audio source. It's a key tool for soundtracks")]
    AudioSource CameraAudio;

    [Header("Player SFX")]
    public AudioClip playerDamage;
    public AudioClip jumpUp, jumpLand, jumpLandBridge;
    public AudioClip walkingStep, walkingStepBridge;
    public AudioClip Pickup;

    [Header("UI SFX")]
    public AudioClip UIButtonClick;

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

        thisAudio = GetComponent<AudioSource>();

        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        if(camera != null)
        {
            CameraAudio = camera.GetComponent<AudioSource>();
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

    // Add this function to UI button on clicks
    public void PlayMenuSound(SFX.UI_SFX sound)
    {
        if(CameraAudio == null) return;

        switch(sound)
        {
            case SFX.UI_SFX.BUTTON_CLICK:
                //if(thisAudio.isPlaying == true) break;
                CameraAudio.PlayOneShot(UIButtonClick);
                break;
        }
    }

    public void PlaySound(SFX.PlayerSFX sound, GameObject audioSourceObj)
    
    {
        AudioSource audioSource = audioSourceObj.GetComponent<AudioSource>();

        if(audioSource == null)
        {
            Debug.Log(audioSourceObj.name + " has no audio source");
            return;
        }

        switch(sound)
        {
            case SFX.PlayerSFX.WALK:
                audioSource.clip = walkingStep;
                if(audioSource.isPlaying && audioSource.clip == walkingStep) break;
                audioSource.Play();
                break;
            case SFX.PlayerSFX.WALK_BRIDGE:
                audioSource.clip = walkingStepBridge;
                if(audioSource.isPlaying && audioSource.clip == walkingStepBridge) break;
                audioSource.Play();
                break;
            case SFX.PlayerSFX.JUMP:
                audioSource.clip = jumpUp;
                if(audioSource.isPlaying && audioSource.clip == jumpUp) break;
                audioSource.Play();
                break;
            case SFX.PlayerSFX.JUMP_LAND:
                audioSource.clip = jumpLand;
                if(audioSource.isPlaying && audioSource.clip == jumpLand) break;
                audioSource.Play();
                break;
            case SFX.PlayerSFX.JUMP_LAND_BRIDGE:
                audioSource.clip = jumpLandBridge;
                if(audioSource.isPlaying && audioSource.clip == jumpLandBridge) break;
                audioSource.Play();
                break;
            case SFX.PlayerSFX.PLAYER_DAMAGE:
                audioSource.clip = playerDamage;
                if(audioSource.isPlaying && audioSource.clip == playerDamage) break;
                audioSource.Play();
                break;

        }
    }

    private void PlayInAudioSource(AudioSource specificSource, AudioClip thisClip)
    {
        if(thisClip == null)
        {
            Debug.Log("Sound Manager has no clip for this sound");
        }

        specificSource.clip = thisClip;

        // skips playing if already playing
        if(specificSource.isPlaying && specificSource.clip == thisClip) return;
        specificSource.Play();
    }
}
