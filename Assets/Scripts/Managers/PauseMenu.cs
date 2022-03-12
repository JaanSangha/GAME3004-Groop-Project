using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    static PauseMenu singleInstance;

    GameObject resumeButton, saveButton;
    GameObject loadButton, optionsButton, quitButton;

    GameObject parent;

    public GameObject savingtext;

    void Awake() 
    {
        Time.timeScale = 0;

        parent = transform.parent.gameObject;

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
        Button[] allChildren = GetComponentsInChildren<Button>();
        foreach(Button b in allChildren)
        {
            if(b.gameObject.name == "ResumeButton")
            {
                resumeButton = b.gameObject;
            }
            else if(b.gameObject.name == "SaveButton")
            {
                saveButton = b.gameObject;
            }
            else if(b.gameObject.name == "LoadButton")
            {
                loadButton = b.gameObject;
            }
            else if(b.gameObject.name == "OptionsButton")
            {
                optionsButton = b.gameObject;
            }
            else if(b.gameObject.name == "QuitButton")
            {
                quitButton = b.gameObject;
            }
        }

        resumeButton.GetComponent<Button>().onClick.AddListener(onResumeButton);
        saveButton.GetComponent<Button>().onClick.AddListener(onSaveButton);
        loadButton.GetComponent<Button>().onClick.AddListener(onLoadButton);
        //optionsButton.GetComponent<Button>().onClick.AddListener(onOptionsButton);
        quitButton.GetComponent<Button>().onClick.AddListener(onQuitButton);
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
       
    }

    void onLoadButton()
    {
        SoundManager.instance.PlayMenuSound(SFX.UI_SFX.BUTTON_CLICK);
    }

    public void onOptionsButton()
    {
        SoundManager.instance.PlayMenuSound(SFX.UI_SFX.BUTTON_CLICK);
        SceneManager.LoadScene("Options");
    }

    public void onQuitButton()
    {
        SoundManager.instance.PlayMenuSound(SFX.UI_SFX.BUTTON_CLICK);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    IEnumerator SavingText()
    {
        Debug.Log("Saving");
        savingtext.SetActive(true);
        yield return new WaitForSeconds(1);
        savingtext.SetActive(false);
    }

}
