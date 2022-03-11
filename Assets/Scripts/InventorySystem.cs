using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySystem : MonoBehaviour
{
    public GameObject playerRef;
    [SerializeField]
    private Image tint1, tint2;
    [SerializeField]
    private TMP_Text counter1, counter2;
    [SerializeField]
    private Button image1, image2;

    public int num1, num2;

    void Update()
    {
        counter1.text = num1.ToString();
        counter2.text = num2.ToString();
        if(num1>0)
        {
            tint1.gameObject.SetActive(false);
        }
        else
        {
            tint1.gameObject.SetActive(true);
        }
        if (num2 > 0)
        {
            tint2.gameObject.SetActive(false);
        }
        else
        {
            tint2.gameObject.SetActive(true);
        }

    }
    public void onButtonOneClick()
    {
        if(num1>0 && !playerRef.GetComponent<PlayerController>().isBoosted)
        {
            num1--;
            SoundManager.instance.PlayMenuSound(SFX.UI_SFX.BUTTON_CLICK);
            Debug.Log("using boost!");
            playerRef.GetComponent<PlayerController>().BoostPowerUp();
        }
    }
    public void onButtonTwoClick()
    {
        if(num2>0)
        {
            num2--;
            SoundManager.instance.PlayMenuSound(SFX.UI_SFX.BUTTON_CLICK);
            Debug.Log("using invincibility!");
            playerRef.GetComponent<PlayerController>().InvinciblePowerUp();
        }
    }
}
