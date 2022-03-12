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
    public SaveData savedData;

    private void SaveSettings()
    {
        PlayerPrefs.SetFloat(savedData.SFXVolume, savedData.soundVolSlider.value);
        PlayerPrefs.SetFloat(savedData.MusicVolume, savedData.musicVolSlider.value);

        PlayerPrefs.SetInt(savedData.InvertAxisX, savedData.InvertXAxis.isOn == true ? 1 : 0);
        PlayerPrefs.SetInt(savedData.InvertAxisY, savedData.InvertYAxis.isOn == true ? 1 : 0);
        PlayerPrefs.SetInt(savedData.DropdownOrientation, savedData.OrientationDropDown.value);

        PlayerPrefs.Save();
        Debug.Log("Game data saved!");
    }

    private void LoadSettings()
    {
        savedData.soundVolSlider.value = LoadFloatKey(savedData.SFXVolume);
        savedData.musicVolSlider.value = LoadFloatKey(savedData.MusicVolume);

        // Key Mapping Settings
        savedData.InvertXAxis.isOn = Convert.ToBoolean(LoadIntKey(savedData.InvertAxisX));
        savedData.InvertYAxis.isOn = Convert.ToBoolean(LoadIntKey(savedData.InvertAxisY));
        savedData.OrientationDropDown.value = LoadIntKey(savedData.DropdownOrientation);
    }

    float LoadFloatKey(string dataLoaded)
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
        LoadSettings();
    }
}