using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ParseLangTwo : MonoBehaviour
{
    public Text Succee;
    public Text Return;
    public Text reTurn;
    public Text Continue;
    public Text Pause;
    public Text Fail;
    void Start()
    {
        SaveManager.Instance.LoadSettings();
        if (!SaveManager.Instance.IsEnglishLanguage)
        {
            Succee.text = "Succeed";
            Return.text = "Return";
            reTurn.text = "Return";
            Continue.text = "Continue";
            Pause.text = "Pause";
            if (Fail != null)
            {
                Fail.text = "Failed";
            }

        }
        else if(SaveManager.Instance.IsEnglishLanguage)
        {
            Succee.text = "恭喜通关";
            Return.text = "返回";
            reTurn.text = "返回星球";
            Continue.text = "继续游戏";
            Pause.text = "暂停游戏";
            if (Fail != null)
            {
                Fail.text = "任务失败";
            }
        }
    }

}
