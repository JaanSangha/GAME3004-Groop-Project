using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySystem : MonoBehaviour
{
    public Image tint1, tint2, tint3;
    public TMP_Text counter1, counter2, counter3;
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
        if (num2 > 0)
        {
            tint2.gameObject.SetActive(false);
        }
        if (num3 > 0)
        {
            tint3.gameObject.SetActive(false);
        }

    }
}
