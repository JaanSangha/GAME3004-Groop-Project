using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    Button[] gameObjButtons;

    [SerializeField, Tooltip("Camera speakers are the global audio source. It's a key tool for soundtracks")]
    AudioSource CameraAudio;
    [SerializeField]
    public SoundAssets soundAssets;
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

        // Finds the camera audio source
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        if(camera != null)
        {
            CameraAudio = camera.GetComponent<AudioSource>();
        }

        // Finds all buttons that exist in the scene (added "true" to add inactive objects)
        gameObjButtons = GameObject.FindObjectsOfType<Button>(true);

        foreach(Button b in gameObjButtons)
        {
            b.onClick.AddListener(
                delegate
                {
                    PlayMenuSound(SFX.UI_SFX.BUTTON_CLICK);
                }
            );
        }
    }

    void Start()
    {
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
                CameraAudio.PlayOneShot(soundAssets.UIButtonClick);
                break;
        }
    }


    // To use this function, attach an audio source to the gameObject you
    // want the sound clip to play from
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
                //if(audioSource.isPlaying) audioSource.Stop();
                audioSource.clip = soundAssets.walkingStep;
                if(audioSource.isPlaying && audioSource.clip == soundAssets.walkingStep) break;
                audioSource.PlayOneShot(soundAssets.walkingStep);
                break;
            case SFX.PlayerSFX.WALK_BRIDGE:
                //if(audioSource.isPlaying) audioSource.Stop();
                audioSource.clip = soundAssets.walkingStepBridge;
                if(audioSource.isPlaying && audioSource.clip == soundAssets.walkingStepBridge) break;
                audioSource.PlayOneShot(soundAssets.walkingStepBridge);
                break;
            case SFX.PlayerSFX.JUMP:
                if(audioSource.isPlaying) audioSource.Stop();
                audioSource.clip = soundAssets.jumpUp;
                if(audioSource.isPlaying && audioSource.clip == soundAssets.jumpUp) break;
                audioSource.PlayOneShot(soundAssets.jumpUp);
                break;
            case SFX.PlayerSFX.JUMP_LAND:
                if(audioSource.isPlaying) audioSource.Stop();
                audioSource.clip = soundAssets.jumpLand;
                if(audioSource.isPlaying && audioSource.clip == soundAssets.jumpLand) break;
                audioSource.PlayOneShot(soundAssets.jumpLand);
                break;
            case SFX.PlayerSFX.JUMP_LAND_BRIDGE:
                if(audioSource.isPlaying) audioSource.Stop();
                audioSource.clip = soundAssets.jumpLandBridge;
                if(audioSource.isPlaying && audioSource.clip == soundAssets.jumpLandBridge) break;
                audioSource.PlayOneShot(soundAssets.jumpLandBridge);
                break;
            case SFX.PlayerSFX.PLAYER_DAMAGE:
                if(audioSource.isPlaying) audioSource.Stop();
                audioSource.clip = soundAssets.playerDamage;
                if(audioSource.isPlaying && audioSource.clip == soundAssets.playerDamage) break;
                audioSource.PlayOneShot(soundAssets.playerDamage);
                break;
            case SFX.PlayerSFX.PICKUP:
                if(audioSource.isPlaying) audioSource.Stop();
                audioSource.clip = soundAssets.Pickup;
                if(audioSource.isPlaying && audioSource.clip == soundAssets.Pickup) break;
                audioSource.PlayOneShot(soundAssets.Pickup);
                break;
        }
    }

    private void PlayInAudioSource(AudioSource specificSource, AudioClip thisClip)
    {
        if(specificSource.isPlaying) specificSource.Stop();
        if(thisClip == null)
        {
            Debug.Log("Sound Manager has no clip for this sound");
        }

        specificSource.clip = thisClip;

        // skips playing if already playing
        if(specificSource.isPlaying && specificSource.clip == thisClip) return;
        specificSource.PlayOneShot(thisClip);
    }
}
