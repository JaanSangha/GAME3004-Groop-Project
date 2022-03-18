using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct MainPauseButtons
{
    [Header("Main Pause Buttons")]
    public GameObject resumeButton;
    public GameObject saveButton;
    public GameObject loadButton;
    public GameObject optionsButton;
    public GameObject quitButton;
}

public class PauseMenu : MonoBehaviour
{
    static PauseMenu singleInstance;
    public GameObject mainUI;
    public GameObject optionsUI;
    public SaveLoadPrefs UIDisplaySaved;
    public GameObject savingText;
    [SerializeField]
    MainPauseButtons widgets;

    GameObject parent;


    void Awake() 
    {
        Time.timeScale = 0;

        parent = gameObject.GetComponentInParent<Canvas>().gameObject;

        if(singleInstance != null)
        {
            Destroy(parent);
        }
        else
        {
            singleInstance = this;
        }
    }

    void Start()
    {
        widgets.resumeButton.GetComponent<Button>().onClick.AddListener(onResumeButton);
        widgets.saveButton.GetComponent<Button>().onClick.AddListener(onSaveButton);
        widgets.loadButton.GetComponent<Button>().onClick.AddListener(onLoadButton);
        widgets.optionsButton.GetComponent<Button>().onClick.AddListener(onOptionsButton);
        widgets.quitButton.GetComponent<Button>().onClick.AddListener(onQuitButton);

        UIDisplaySaved = optionsUI.GetComponent<SaveLoadPrefs>();
        
        // unfortunately, no way of seeing how many functions you add to button listener
        UIDisplaySaved.UIDisplay.backButton.gameObject.SetActive(false);
        UIDisplaySaved.UIDisplay.backToPause.gameObject.SetActive(true);
        // key mapping options error when trying to change settings in options menu
        UIDisplaySaved.UIDisplay.KeyMappingOptsToggle.gameObject.SetActive(false);

        UIDisplaySaved.UIDisplay.backToPause.onClick.AddListener(onOptionsBackButton);
        
        mainUI.SetActive(true);
        optionsUI.SetActive(false);

        SoundManager.instance.ApplySoundToWidgets();
    }

    void onResumeButton()
    {
        Time.timeScale = 1;
        SoundManager.instance.PlayMenuSound(SFX.UI_SFX.BUTTON_CLICK);
        Destroy(parent);
    }

    void onSaveButton()
    {
        StartCoroutine(SavingText());
        SoundManager.instance.PlayMenuSound(SFX.UI_SFX.BUTTON_CLICK);
        GameManager.instance.saveLoad.OnSaveButton_Pressed();
    }

    void onLoadButton()
    {
        SoundManager.instance.PlayMenuSound(SFX.UI_SFX.BUTTON_CLICK);
        GameManager.instance.saveLoad.OnLoadButton_Pressed();
    }

    public void onOptionsButton()
    {
        SoundManager.instance.PlayMenuSound(SFX.UI_SFX.BUTTON_CLICK);
        mainUI.SetActive(false);
        optionsUI.SetActive(true);
    }

    public void onOptionsBackButton()
    {
        SoundManager.instance.PlayMenuSound(SFX.UI_SFX.BUTTON_CLICK);
        mainUI.SetActive(true);
        optionsUI.SetActive(false);
    }

    public void onQuitButton()
    {
        SoundManager.instance.PlayMenuSound(SFX.UI_SFX.BUTTON_CLICK);
/* #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif */
        SceneManager.LoadScene("Menu");
    }

    IEnumerator SavingText()
    {
        Debug.Log("Saving");
        savingText.SetActive(true);
        yield return new WaitForSeconds(1);
        savingText.SetActive(false);
    }

}
