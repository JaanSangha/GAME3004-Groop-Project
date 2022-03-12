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

    void LoadPlayerSettings()
    {
        SoundManager.instance.MusicVolume = playerSettingsSO.MusicVolume;
        SoundManager.instance.SoundVolume = playerSettingsSO.SoundVolume;
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
        LoadPlayerSettings();
        SoundManager.instance.NewSceneSoundManager(scene, mode);

        if(scene.name != "Menu" && scene.name != "Options")
        {
            
        }
    }
}