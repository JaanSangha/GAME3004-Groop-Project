using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// JS -> Joystick, TB -> Touch Buttons
public static class ButtonOrientation
{
    public const int JSLeftTBRight = 0;
    public const int JSRightTBLeft = 1;
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public SaveLoadGame saveLoad;

    [Header("Touch Controls")]
    public Joystick joystick;
    public TouchButtonInput touchButtons;
    [SerializeField]
    PlayerController player;

    void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else if(instance == null)
        {
            instance = this;
        }

        saveLoad = GetComponent<SaveLoadGame>();
    }

    public void ApplyKeyMappingPosition(int buttonOrientation)
    {
        var JSTransform = joystick.gameObject.GetComponent<RectTransform>();
        var TBTransform = touchButtons.gameObject.GetComponent<RectTransform>();
        
        switch(buttonOrientation)
        {
            case ButtonOrientation.JSLeftTBRight:
                JSTransform.anchoredPosition = new Vector2(-Mathf.Abs(JSTransform.anchoredPosition.x), JSTransform.anchoredPosition.y);
                TBTransform.anchoredPosition = new Vector2(Mathf.Abs(TBTransform.anchoredPosition.x), TBTransform.anchoredPosition.y);
                break;
            case ButtonOrientation.JSRightTBLeft:
                JSTransform.anchoredPosition = new Vector2(Mathf.Abs(JSTransform.anchoredPosition.x), JSTransform.anchoredPosition.y);
                TBTransform.anchoredPosition = new Vector2(-Mathf.Abs(TBTransform.anchoredPosition.x), TBTransform.anchoredPosition.y);
                break;
        }
    }

    public void GetJoystickInversions(bool invertX, bool invertY)
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        
        if(player == null)
        {
            Debug.Log("Player does not exist");
            return;
        }

        switch(invertY)
        {
            case true:
                player.JSInvertY = -Mathf.Abs(player.JSInvertY);
                break;
            case false:
                player.JSInvertY = Mathf.Abs(player.JSInvertY);
                break;
        }

        switch(invertX)
        {
            case true:
                player.JSInvertX = -Mathf.Abs(player.JSInvertX);
                break;
            case false:
                player.JSInvertX = Mathf.Abs(player.JSInvertX);
                break;
        }
    }
}
