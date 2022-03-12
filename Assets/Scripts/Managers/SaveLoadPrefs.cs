using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

[System.Serializable]
public class SaveData
{
    [Header("Sound Settings")]
    public Slider soundVolSlider;
    public string SFXVolume = "SFXVolume";
    public Slider musicVolSlider;
    public string MusicVolume = "MusicVolume";
    [Header("Key Mapping Settings")]
    public Toggle InvertYAxis;
    public string InvertAxisY = "InvertYAxis";
    public Toggle InvertXAxis;
    public string InvertAxisX = "InvertXAxis";
    public TMP_Dropdown OrientationDropDown;
    public string DropdownOrientation = "OrientationDropDown";

    public SaveData()
    {}
}

public class SaveLoadPrefs : MonoBehaviour
{
    public Settings playerSettingsSO;
    public SaveData savedData;

    private void SaveSettings()
    {
        playerSettingsSO.MusicVolume = savedData.soundVolSlider.value;
        playerSettingsSO.SoundVolume = savedData.musicVolSlider.value;

        playerSettingsSO.InvertXAxis = savedData.InvertXAxis.isOn;
        playerSettingsSO.InvertYAxis = savedData.InvertYAxis.isOn;
        playerSettingsSO.DropdownOrientation = savedData.OrientationDropDown.value;
        Debug.Log("Game data saved!");
    }

    private void LoadSettingsUI()
    {
        /* savedData.soundVolSlider.value = LoadFloatKey(savedData.SFXVolume);
        savedData.musicVolSlider.value = LoadFloatKey(savedData.MusicVolume);

        // Key Mapping Settings
        savedData.InvertXAxis.isOn = Convert.ToBoolean(LoadIntKey(savedData.InvertAxisX));
        savedData.InvertYAxis.isOn = Convert.ToBoolean(LoadIntKey(savedData.InvertAxisY));
        savedData.OrientationDropDown.value = LoadIntKey(savedData.DropdownOrientation); */

        savedData.soundVolSlider.value = playerSettingsSO.MusicVolume;
        savedData.musicVolSlider.value = playerSettingsSO.SoundVolume;
        
        savedData.InvertXAxis.isOn = playerSettingsSO.InvertXAxis;
        savedData.InvertYAxis.isOn = playerSettingsSO.InvertYAxis;
        savedData.OrientationDropDown.value = playerSettingsSO.DropdownOrientation;
    }

    public float LoadFloatKey(string dataLoaded)
    {
        if (PlayerPrefs.HasKey(dataLoaded))
        {
            var floatValue = PlayerPrefs.GetFloat(dataLoaded);

            Debug.Log("Game data loaded!");
            return floatValue;
        }
        else
        {
            Debug.LogError("There is no save data!");
        }

        return 0;
    }

    int LoadIntKey(string dataLoaded)
    {
        if (PlayerPrefs.HasKey(dataLoaded))
        {
            var IntValue = PlayerPrefs.GetInt(dataLoaded);

            Debug.Log("Game data loaded!");
            return IntValue;
        }
        else
        {
            Debug.LogError("There is no save data!");
        }

        return 0;
    }

    string LoadStringKey(string dataLoaded)
    {
        if (PlayerPrefs.HasKey(dataLoaded))
        {
            var stringValue = PlayerPrefs.GetString(dataLoaded);

            Debug.Log("Game data loaded!");
            return stringValue;
        }
        else
        {
            Debug.LogError("There is no save data!");
        }

        return null;
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

    void OnDestroy()
    {
        SaveSettings();
    }

    void onNewSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LoadSettingsUI();
    }
}