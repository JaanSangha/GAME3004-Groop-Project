using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public TMP_Dropdown DropdownOrientation;
}

public class SaveLoadPrefs : MonoBehaviour
{
    public Settings playerSettingsSO;
    public SaveData UIDisplay;

    void Start()
    {
        LoadSettingsUI();
    }

    private void SaveSettings()
    {
        playerSettingsSO.MusicVolume = UIDisplay.musicVolSlider.value;
        playerSettingsSO.SoundVolume = UIDisplay.soundVolSlider.value;

        playerSettingsSO.InvertXAxis = UIDisplay.InvertXAxis.isOn;
        playerSettingsSO.InvertYAxis = UIDisplay.InvertYAxis.isOn;
        playerSettingsSO.DropdownOrientation = UIDisplay.DropdownOrientation.value;
        Debug.Log("Game data saved!");
    }

    private void LoadSettingsUI()
    {
        UIDisplay.musicVolSlider.value = playerSettingsSO.MusicVolume;
        UIDisplay.soundVolSlider.value = playerSettingsSO.SoundVolume;
        
        UIDisplay.InvertXAxis.isOn = playerSettingsSO.InvertXAxis;
        UIDisplay.InvertYAxis.isOn = playerSettingsSO.InvertYAxis;
        UIDisplay.DropdownOrientation.value = playerSettingsSO.DropdownOrientation;
    }

    void OnDestroy()
    {
        SaveSettings();
    }
}