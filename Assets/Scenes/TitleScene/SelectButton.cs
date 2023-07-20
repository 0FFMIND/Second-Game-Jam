using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectButton : MonoBehaviour
{
    List<SettingBtnCust> firstButtons = new List<SettingBtnCust>();
    List<SettingBtnCust> secondButtons = new List<SettingBtnCust>();
    List<SettingBtnCust> storageButtons = new List<SettingBtnCust>();
    public GameObject successPanel;
    public void SaveSettings()
    {
        List<string> storageSettings = new List<string>();
        foreach (var singleBtn in storageButtons)
        {
            if (singleBtn.isPressed)
            {
                switch (singleBtn.nowOptions)
                {
                    //速速切换
                    case SettingBtnCust.E_nowOptions.highReso:
                        storageSettings.Add("highReso");
                        Screen.SetResolution(1920, 1080,true);
                        break;
                    case SettingBtnCust.E_nowOptions.lowReso:
                        Screen.SetResolution(1280, 720, true);
                        storageSettings.Add("lowReso");
                        break;
                    case SettingBtnCust.E_nowOptions.highSize:
                        storageSettings.Add("highSize");
                        Screen.fullScreen = true;
                        break;
                    case SettingBtnCust.E_nowOptions.lowSize:
                        Screen.fullScreen = false;
                        storageSettings.Add("lowSize");
                        break;
                    case SettingBtnCust.E_nowOptions.EngLang:
                        LanguageManager.Instance.nowOption = LanguageOption.English;
                        storageSettings.Add("engLang");
                        break;
                    case SettingBtnCust.E_nowOptions.ChnLang:
                        LanguageManager.Instance.nowOption = LanguageOption.Chinese;
                        storageSettings.Add("chnLang");
                        break;
                    default:
                        break;
                }
            }
        }
        if(LanguageManager.Instance.nowOption == LanguageOption.Chinese)
        {
            TMP_Text textMeshPro = successPanel.GetComponentInChildren<TMP_Text>();
            textMeshPro.text = "<wave>您的默认设置已成功完成更改并储存,请单击鼠标左键隐藏提示信息</wave>";
        }
        else if(LanguageManager.Instance.nowOption == LanguageOption.English)
        {
            TMP_Text textMeshPro = successPanel.GetComponentInChildren<TMP_Text>();
            textMeshPro.text = "<wave>Your default settings have succsfully changed. Please click to deactive showing panel</wave>";
        }
        successPanel.SetActive(true);
        SaveManager.Instance.SaveSettings(storageSettings);
    }
    public void Update()
    {
        if (successPanel.activeSelf)
        {
            if (Input.GetMouseButtonDown(0))
            {
                successPanel.SetActive(false);
            }
        }
    }
    public void SetButtons()
    {
        Transform[] parentTrans = { gameObject.transform.Find("ResoSetting"),gameObject.transform.Find("SizeSetting"),gameObject.transform.Find("LangSetting")};
        for (int i = 0; i < parentTrans.Length; i++)
        {
            firstButtons.Add(parentTrans[i].GetChild(0).gameObject.GetComponent<SettingBtnCust>());
            secondButtons.Add(parentTrans[i].GetChild(1).gameObject.GetComponent<SettingBtnCust>());
            storageButtons.Add(parentTrans[i].GetChild(0).gameObject.GetComponent<SettingBtnCust>());
            storageButtons.Add(parentTrans[i].GetChild(1).gameObject.GetComponent<SettingBtnCust>());
        }
        for (int i = 0; i < firstButtons.Count; i++)
        {
            int j = 2 * i;
            firstButtons[i].nowOptions = (SettingBtnCust.E_nowOptions)j;
            firstButtons[i].isFirst = true;
            firstButtons[i].isPressed = true;
            //注册事件
            firstButtons[i].PressedBtn += SettingPressedBtn;
            firstButtons[i].UnPressedBtn += SettingUnpressedBtn;
            firstButtons[i].HighlightedBtn += SettingHighlightedButton;
        }
        for (int i = 0; i < secondButtons.Count; i++)
        {
            int j = 1 + (2 * i);
            secondButtons[i].nowOptions = (SettingBtnCust.E_nowOptions)j;
            secondButtons[i].isFirst = false;
            secondButtons[i].isPressed = false;
            //注册事件
            secondButtons[i].PressedBtn += SettingPressedBtn;
            secondButtons[i].UnPressedBtn += SettingUnpressedBtn;
            secondButtons[i].HighlightedBtn += SettingHighlightedButton;
        }
        for (int i = 0; i < storageButtons.Count; i++)
        {
            if (SaveManager.Instance.ishighReso)
            {
                firstButtons[0].isPressed = true;
                secondButtons[0].isPressed = false;
            }else if (SaveManager.Instance.islowReso)
            {
                firstButtons[0].isPressed = false;
                secondButtons[0].isPressed = true;
            }
            if (SaveManager.Instance.ishighSize)
            {
                firstButtons[1].isPressed = true;
                secondButtons[1].isPressed = false;
            }else if (SaveManager.Instance.islowSize)
            {
                firstButtons[1].isPressed = false;
                secondButtons[1].isPressed = true;
            }
            if (SaveManager.Instance.isengLang)
            {
                firstButtons[2].isPressed = true;
                secondButtons[2].isPressed = false;
            }
            else if (SaveManager.Instance.ischnLang)
            {
                firstButtons[2].isPressed = false;
                secondButtons[2].isPressed = true;
            }
        }
        foreach (SettingBtnCust btncust in storageButtons)
        {
            btncust.Init();
        }
    }
    public void SettingHighlightedButton(SettingBtnCust settingBtnCust)
    {
        if (settingBtnCust.isHighlighted)
        {
            ColorBlock cb = new ColorBlock();
            if (!settingBtnCust.isPressed)
            {
                cb.normalColor = new Color(1f, 1f, 1f, 0.5f); cb.highlightedColor = new Color(1f, 1f, 1f, 0.5f); cb.selectedColor = new Color(1f, 1f, 1f, 0.5f); cb.pressedColor = Color.red; cb.colorMultiplier = 1f; cb.fadeDuration = 0.1f;
                settingBtnCust.colors = cb;
                settingBtnCust.GetComponentInChildren<Text>().color = Color.white;
            }
        }else if (!settingBtnCust.isHighlighted)
        {
            if (!settingBtnCust.isPressed)
            {
                settingBtnCust.GetComponentInChildren<Text>().color = Color.white;
                SettingUnpressedBtn(settingBtnCust);
            }
        }
    }
    public void SettingUnpressedBtn(SettingBtnCust settingBtnCust)
    {
        ColorBlock cb = new ColorBlock();
        cb.normalColor = Color.clear; cb.colorMultiplier = 1f; cb.fadeDuration = 0.1f;
        settingBtnCust.colors = cb;
        settingBtnCust.GetComponentInChildren<Text>().color = Color.white;
        settingBtnCust.isPressed = false;
    }
    public void SettingPressed(SettingBtnCust settingBtnCust)
    {
        ColorBlock cb = new ColorBlock();
        cb.normalColor = Color.white; cb.selectedColor = new Color(1f, 1f, 1f, 1f); cb.highlightedColor = new Color(1f, 1f, 1f, 1f); cb.pressedColor = Color.red; cb.colorMultiplier = 1f; cb.fadeDuration = 0.1f;
        settingBtnCust.colors = cb;
        settingBtnCust.GetComponentInChildren<Text>().color = Color.black;
        settingBtnCust.isPressed = true;
    }
    public void SettingPressedBtn(SettingBtnCust settingBtnCust)
    {
        SettingBtnCust targetCust;
        SettingBtnCust untargetCust;
            //实现二选一
            if (secondButtons.Exists(t => t.Equals(settingBtnCust)))
            {
                int index = secondButtons.FindIndex(t => t.Equals(settingBtnCust));
                targetCust = secondButtons[index];
                targetCust.isPressed = true;
                SettingPressed(targetCust);
                untargetCust = firstButtons[index];
                untargetCust.isPressed = false;
                SettingUnpressedBtn(untargetCust);
            }
            else
            {
                int index = firstButtons.FindIndex(t => t.Equals(settingBtnCust));
                targetCust = firstButtons[index];
                targetCust.isPressed = true;
                SettingPressed(targetCust);
                untargetCust = secondButtons[index];
                untargetCust.isPressed = false;
                SettingUnpressedBtn(untargetCust);
            }//targetCust即为待选择的对象
    }
}
