using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StrategyChange : MonoBehaviour
{
    public Text text;
    void Start()
    {
        SaveManager.Instance.LoadLevel();
        if(SaveManager.Instance.IsAngel == 1)
        {
            if(LanguageManager.Instance.currentLanguage == LanguageOption.Chinese)
            {
                text.text = "ÎÂºÍ";
            }
            else if (LanguageManager.Instance.currentLanguage == LanguageOption.English)
            {
                text.text = "Gentle";
            }
        }
        if (SaveManager.Instance.IsAngel == 2)
        {
            if (LanguageManager.Instance.currentLanguage == LanguageOption.Chinese)
            {
                text.text = "¼¤½ø";
            }
            else if (LanguageManager.Instance.currentLanguage == LanguageOption.English)
            {
                text.text = "Aggresive";
            }
        }
    }

}
