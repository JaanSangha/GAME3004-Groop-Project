using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour // Singleton<SceneManagement>
{
    public static SceneManagement instance;

    public Settings playerSettingsSO;

    void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    void LoadSoundSettings()
    {
        SoundManager.instance.MusicVolume = playerSettingsSO.MusicVolume;
        SoundManager.instance.SoundVolume = playerSettingsSO.SoundVolume;
    }

    void LoadKeyMappings()
    {

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
        LoadSoundSettings();
        SoundManager.instance.NewSceneSoundManager(scene, mode);

        if(scene.name != "Menu" && scene.name != "Options")
        {
            Debug.Log("Game Time");
            // finding onscreen buttons here
            LoadKeyMappings();
        }
    }
}