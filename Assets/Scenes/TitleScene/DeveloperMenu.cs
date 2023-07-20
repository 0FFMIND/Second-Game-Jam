using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeveloperMenu : TitleMenuBase<DeveloperMenu>
{
    private GameObject go;
    public void Start()
    {
        if(LanguageManager.Instance.nowOption == LanguageOption.Chinese)
        {
            go = GameObject.FindWithTag("TEXT");
            go.GetComponent<TMP_Text>().text = "<wave>开发人员介绍</wave>";
        }
        else if(LanguageManager.Instance.nowOption == LanguageOption.English)
        {
            go.GetComponent<TMP_Text>().text = "<wave>DEVELOPERS</wave>";
        }
    }
    public override void OnBackPressed()
    {
        Close();
    }
}
