using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


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
            DontDestroyOnLoad(this);
        }
    }

    void Start()
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
                PlayInAudioSource(audioSource, soundAssets.walkingStep);
                break;
            case SFX.PlayerSFX.WALK_BRIDGE:
                //if(audioSource.isPlaying) audioSource.Stop();
                PlayInAudioSource(audioSource, soundAssets.walkingStepBridge);
                break;
            case SFX.PlayerSFX.JUMP:
                if(audioSource.isPlaying) audioSource.Stop();
                PlayInAudioSource(audioSource, soundAssets.jumpUp);
                break;
            case SFX.PlayerSFX.JUMP_LAND:
                if(audioSource.isPlaying) audioSource.Stop();
                PlayInAudioSource(audioSource, soundAssets.jumpLand);
                // audioSource.clip = soundAssets.jumpLand;
                // if(audioSource.isPlaying && audioSource.clip == soundAssets.jumpLand) break;
                // audioSource.PlayOneShot(soundAssets.jumpLand);
                break;
            case SFX.PlayerSFX.JUMP_LAND_BRIDGE:
                if(audioSource.isPlaying) audioSource.Stop();
                PlayInAudioSource(audioSource, soundAssets.jumpLandBridge);
                // audioSource.clip = soundAssets.jumpLandBridge;
                // if(audioSource.isPlaying && audioSource.clip == soundAssets.jumpLandBridge) break;
                // audioSource.PlayOneShot(soundAssets.jumpLandBridge);
                break;
            case SFX.PlayerSFX.PLAYER_DAMAGE:
                if(audioSource.isPlaying) audioSource.Stop();
                PlayInAudioSource(audioSource, soundAssets.playerDamage);
                break;
            case SFX.PlayerSFX.PICKUP:
                if(audioSource.isPlaying) audioSource.Stop();
                PlayInAudioSource(audioSource, soundAssets.Pickup);
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
        specificSource.PlayOneShot(thisClip);
    }


    // NEW LEVEL LOADED IMPLEMENTATION FUNCTIONS

    void OnEnable() 
    {
        SceneManager.sceneLoaded += onNewSceneLoaded;
    }

    void OnDisable() 
    {
        SceneManager.sceneLoaded -= onNewSceneLoaded;
    }

    void onNewSceneLoaded(Scene scene, LoadSceneMode mode)
    {
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
}
