using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

[System.Serializable]
public struct SaveData
{
    [Header("Sound Settings")]
    public Slider soundVolSlider;
    public Slider musicVolSlider;
    [Header("Key Mapping Settings")]
    public Toggle InvertYAxis;
    public Toggle InvertXAxis;
    public TMP_Dropdown OrientationDropDown;
}

public class SaveLoadPrefs : MonoBehaviour
{
    public Settings playerSettingsSO;
    public SaveData savedData;

    void Start()
    {
        LoadSettingsUI();
    }

    private void SaveSettings()
    {
        playerSettingsSO.MusicVolume = savedData.musicVolSlider.value;
        playerSettingsSO.SoundVolume = savedData.soundVolSlider.value;

        playerSettingsSO.InvertXAxis = savedData.InvertXAxis.isOn;
        playerSettingsSO.InvertYAxis = savedData.InvertYAxis.isOn;
        playerSettingsSO.DropdownOrientation = savedData.OrientationDropDown.value;
        Debug.Log("Game data saved!");
    }

    private void LoadSettingsUI()
    {
        savedData.musicVolSlider.value = playerSettingsSO.MusicVolume;
        savedData.soundVolSlider.value = playerSettingsSO.SoundVolume;
        
        savedData.InvertXAxis.isOn = playerSettingsSO.InvertXAxis;
        savedData.InvertYAxis.isOn = playerSettingsSO.InvertYAxis;
        savedData.OrientationDropDown.value = playerSettingsSO.DropdownOrientation;
    }

    void OnDestroy()
    {
        SaveSettings();
    }
}