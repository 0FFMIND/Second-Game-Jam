using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectButton : MonoBehaviour
{
    private List<SettingBtnCust> firstButtons = new List<SettingBtnCust>();
    private List<SettingBtnCust> secondButtons = new List<SettingBtnCust>();
    private List<SettingBtnCust> storageButtons = new List<SettingBtnCust>();
    private void SaveSettings()
    {
        List<string> storageSettings = new List<string>();
        foreach (var singleBtn in storageButtons)
        {
            switch (singleBtn.nowOptions)
            {
                case SettingBtnCust.E_nowOptions.highReso:
                     storageSettings.Add("highReso");
                     Screen.SetResolution(1920, 1080, true);
                     break;
                case SettingBtnCust.E_nowOptions.lowReso:
                     Screen.SetResolution(1280, 720, true);
                     storageSettings.Add("lowReso");
                     break;
                case SettingBtnCust.E_nowOptions.SFXvalue:
                    //TO DO
                    break;
                case SettingBtnCust.E_nowOptions.BGMvalue:
                    //TO DO
                    break;
                case SettingBtnCust.E_nowOptions.EngLang:
                     LanguageManager.Instance.CurrentLanguage = LanguageOption.English;
                     storageSettings.Add("engLang");
                     break;
                case SettingBtnCust.E_nowOptions.ChnLang:
                     LanguageManager.Instance.CurrentLanguage = LanguageOption.Chinese;
                     storageSettings.Add("chnLang");
                     break;
                default:
                     break;
            }
        }
        SaveManager.Instance.SaveSettings(storageSettings);
    }
    private void Update()
    {
        //TO DO 当音量键变化
        foreach (var singleBtn in storageButtons)
        {
            if (singleBtn.isPressed)
            {
                SaveSettings();
            }
        }
    }
    public void SetButtons()
    {
        Transform[] parentTransforms = { gameObject.transform.Find("ResoSetting"), gameObject.transform.Find("LangSetting") };
        // Initialize buttons and assign options and events.
        for (int i = 0; i < parentTransforms.Length; i++)
        {
            InitializeButton(firstButtons, parentTransforms, i, 0);
            InitializeButton(secondButtons, parentTransforms, i, 1);
        }
        UpdateButtonsFromSavedSettings();
        foreach (SettingBtnCust button in storageButtons) button.Init();
    }

    // Move button initialization to a separate method for clarity and reuse.
    private void InitializeButton(List<SettingBtnCust> buttonList, Transform[] parentTransforms, int parentIndex, int childIndex)
    {
        SettingBtnCust button = parentTransforms[parentIndex].GetChild(childIndex).gameObject.GetComponent<SettingBtnCust>();
        buttonList.Add(button);
        storageButtons.Add(button);

        button.nowOptions = (SettingBtnCust.E_nowOptions)(2 * parentIndex + childIndex);
        button.isFirst = childIndex == 1;
        button.PressedBtn += SettingPressedBtn;
        button.UnPressedBtn += SettingUnpressedBtn;
        button.HighlightedBtn += SettingHighlightedButton;
    }

    // Update button states based on saved settings.
    private void UpdateButtonsFromSavedSettings()
    {
        UpdateButtonState(SaveManager.Instance.IsHighResolution, SaveManager.Instance.IsLowResolution, 0);
        UpdateButtonState(SaveManager.Instance.IsEnglishLanguage, SaveManager.Instance.IsChineseLanguage, 1);
    }
    // Update the state of a pair of buttons based on their saved settings.
    private void UpdateButtonState(bool firstSetting, bool secondSetting, int index)
    {
        if (firstSetting)
        {
            SettingPressedBtn(firstButtons[index]);
            SettingUnpressedBtn(secondButtons[index]);
        }
        else if (secondSetting)
        {
            SettingUnpressedBtn(firstButtons[index]);
            SettingPressedBtn(secondButtons[index]);
        }
    }

    // Event handlers for button actions.
    private void SettingPressedBtn(SettingBtnCust button)
    {
        // Get the other button in the pair.
        SettingBtnCust otherButton = button.isFirst ? secondButtons[(int)button.nowOptions / 2] : firstButtons[(int)button.nowOptions / 2];

        // Unpress the other button.
        SettingUnpressedBtn(otherButton);
        button.isPressed = true;
    }

    private void SettingUnpressedBtn(SettingBtnCust button)
    {
        button.isPressed = false;
    }

    private void SettingHighlightedButton(SettingBtnCust button)
    {
        button.isHighlighted = true;
    }
    public void SFXchange()
    {

    }
    public void BGMchange()
    {

    }
}
