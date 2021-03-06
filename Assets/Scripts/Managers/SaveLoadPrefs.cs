using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    [Header("Non-Saved UI")]
    public Button KeyMappingOptsToggle;
    public Button backButton;
    public Button backToPause;
}

public class SaveLoadPrefs : MonoBehaviour
{
    public Settings playerSettingsSO;
    public SaveData UIDisplay;

    void Awake()
    {
        // unfortunately, no way of seeing how many functions you add to button listener
        UIDisplay.backButton.gameObject.SetActive(true);
        UIDisplay.backToPause.gameObject.SetActive(false);

        UIDisplay.backButton.onClick.AddListener(goBackToMainMenu);
    }

    void goBackToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    void OnEnable()
    {
        Debug.Log(UIDisplay.backButton.onClick.GetPersistentEventCount());
        LoadSettingsUI();
        if(UIDisplay.backButton.onClick.GetPersistentEventCount() < 1)
        {
            Debug.Log("Empty Button");
            UIDisplay.backButton.onClick.AddListener(goBackToMainMenu);
        }
    }

    private void SaveSettings()
    {
        playerSettingsSO.MusicVolume = UIDisplay.musicVolSlider.value;
        playerSettingsSO.SoundVolume = UIDisplay.soundVolSlider.value;

        playerSettingsSO.InvertXAxis = UIDisplay.InvertXAxis.isOn;
        playerSettingsSO.InvertYAxis = UIDisplay.InvertYAxis.isOn;
        playerSettingsSO.DropdownOrientation = UIDisplay.DropdownOrientation.value;
        Debug.Log("Setting data saved!");
    }

    private void LoadSettingsUI()
    {
        UIDisplay.musicVolSlider.value = playerSettingsSO.MusicVolume;
        UIDisplay.soundVolSlider.value = playerSettingsSO.SoundVolume;
        
        UIDisplay.InvertXAxis.isOn = playerSettingsSO.InvertXAxis;
        UIDisplay.InvertYAxis.isOn = playerSettingsSO.InvertYAxis;
        UIDisplay.DropdownOrientation.value = playerSettingsSO.DropdownOrientation;
        Debug.Log("Setting data loaded!");
    }

    void OnDisable()
    {
        SaveSettings();
    }
}