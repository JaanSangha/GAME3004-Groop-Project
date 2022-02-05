using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySystem : MonoBehaviour
{
    [SerializeField]
    private Image tint1, tint2, tint3;
    [SerializeField]
    private TMP_Text counter1, counter2, counter3;
    [SerializeField]
    private Button image1, image2, image3;

    public int num1, num2, num3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        counter1.text = num1.ToString();
        counter2.text = num2.ToString();
        counter3.text = num3.ToString();
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
        if (num3 > 0)
        {
            tint3.gameObject.SetActive(false);
        }
        else
        {
            tint3.gameObject.SetActive(true);
        }

    }
    public void onButtonOneClick()
    {
        if(num1>0)
        {
            num1--;
            Debug.Log("using boost!");
        }
    }
    public void onButtonTwoClick()
    {
        if(num2>0)
        {
            num2--;
            Debug.Log("using invincibility!");
        }
    }
    public void onButtonThreeClick()
    {
        if(num3>0)
        {
            num3--;
            Debug.Log("using projectile!");
        }
    }
}
