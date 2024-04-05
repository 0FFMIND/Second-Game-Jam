using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PowerChange : MonoBehaviour
{
    private Text text;
    public OpenPackage openPackage;
    public GameObject gObject;
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
        if (num < 0) {
            //gObject.SetActive(false);
            text.transform.DOShakePosition(1, 10, 10, 50, true);
            return;
        }
        else
        {
            PlayerPrefs.SetInt("Stars", num);
            openPackage.OnClickOpen();
            gObject.SetActive(true);
        }
    } 
}
