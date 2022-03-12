using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public GameObject PauseUIPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            PauseButton();
        }
    }

    public void RestartButton()
    {
       
        SceneManager.LoadScene("Main");
    }

    public void ExitButton()
    {
      
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void GoToMainButton()
    {
        SceneManager.LoadScene("Main");
    }
    public void GoToOptionsButton()
    {
        SceneManager.LoadScene("Options");
    }
    public void GoToMenuButton()
    {
        SceneManager.LoadScene("Menu");
    }
    public void PauseButton()
    {
        if(PauseUIPrefab != null)
        {
            Instantiate(PauseUIPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        }
        else Debug.Log("No pause UI");
    }
}
