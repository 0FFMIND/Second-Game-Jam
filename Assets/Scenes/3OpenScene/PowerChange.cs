using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerChange : MonoBehaviour
{
    private Text text;
    public OpenPackage openPackage;
    private void Start()
    {
        text = gameObject.GetComponent<Text>();
    }
    private void Update()
    {
        text.text = PlayerPrefs.GetInt("Stars").ToString();
    }
    public void CostMoney(int money)
    {
        int num = PlayerPrefs.GetInt("Stars") - money;
        if (num < 0) return;
        PlayerPrefs.SetInt("Stars", num);
        if(money == 3)
        {
            openPackage.OnClickOpen();
        }
    } 
}
