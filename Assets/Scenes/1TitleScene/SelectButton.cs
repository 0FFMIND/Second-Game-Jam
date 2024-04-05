using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectButton : MonoBehaviour
{
    private List<SettingBtnCust> ResoSettingButtons = new List<SettingBtnCust>();
    private List<SettingBtnCust> LangSettingButtons = new List<SettingBtnCust>();
    private List<SettingBtnCust> storageButtons = new List<SettingBtnCust>();
    public Slider SFXSlider;
    public Slider BGMSlider;
    
    
    private void Start()
    {
        SetButtons();
    }
    private void OnEnable()
    {
        if ((SaveManager.Instance.SFXvalue >= SFXSlider.minValue) && (SaveManager.Instance.SFXvalue <= SFXSlider.maxValue))
        {
            SFXSlider.value = SaveManager.Instance.SFXvalue;
        }
        else if(SaveManager.Instance.SFXvalue < SFXSlider.minValue) SFXSlider.value = SFXSlider.minValue;
        else if(SaveManager.Instance.SFXvalue > SFXSlider.maxValue) SFXSlider.value = SFXSlider.maxValue;

        if ((SaveManager.Instance.BGMvalue >= BGMSlider.minValue) && (SaveManager.Instance.BGMvalue <= BGMSlider.maxValue))
        {
            BGMSlider.value = SaveManager.Instance.BGMvalue;
        }
        else if (SaveManager.Instance.BGMvalue < BGMSlider.minValue) BGMSlider.value = BGMSlider.minValue;
        else if (SaveManager.Instance.BGMvalue > BGMSlider.maxValue) BGMSlider.value = BGMSlider.maxValue;
    }
    public void SetButtons()
    {
        Transform[] parentTransforms = { gameObject.transform.Find("ResoSetting"), gameObject.transform.Find("LangSetting") };
        // Initialize buttons and assign options and events.
        InitializeButton(ResoSettingButtons, parentTransforms, 0);
        InitializeButton(LangSettingButtons, parentTransforms, 1);
        UpdateButtonsFromSavedSettings();
    }

    // Move button initialization to a separate method for clarity and reuse.
    private void InitializeButton(List<SettingBtnCust> buttonList, Transform[] parentTransforms, int parentIndex)
    {
        for (int childIndex = 0; childIndex < 2; childIndex++) 
        {
            SettingBtnCust button = parentTransforms[parentIndex].GetChild(childIndex).gameObject.GetComponent<SettingBtnCust>();
            buttonList.Add(button);
            button.nowOptions = (SettingBtnCust.E_nowOptions)(2 * parentIndex + childIndex);
            button.isReso = parentIndex == 0;
            button.PressedBtn += SettingPressedBtn;
            button.UnPressedBtn += SettingUnpressedBtn;
            button.HighlightedBtn += SettingHighlightedButton;
        }
    }

    private void SettingHighlightedButton(SettingBtnCust button)
    {
        button.isHighlighted = true;
    }

    #region Update Button

    // Update button states based on saved settings.
    private void UpdateButtonsFromSavedSettings()
    {
        storageButtons.Clear();
        UpdateButtonState(SaveManager.Instance.IsHighResolution, SaveManager.Instance.IsLowResolution, ResoSettingButtons);
        UpdateButtonState(SaveManager.Instance.IsEnglishLanguage, SaveManager.Instance.IsChineseLanguage, LangSettingButtons);
        UpdateButtonStateTwo(SaveManager.Instance.IsHighResolution, SaveManager.Instance.IsLowResolution, ResoSettingButtons);
        UpdateButtonStateTwo(SaveManager.Instance.IsEnglishLanguage, SaveManager.Instance.IsChineseLanguage, LangSettingButtons);
    }
    // Update the state of a pair of buttons based on their saved settings.
    private void UpdateButtonState(bool firstSetting, bool secondSetting, List<SettingBtnCust> buttonList)
    {
        if (firstSetting)
        {
            storageButtons.Add(buttonList[0]);
        }
        else if (secondSetting)
        {
            storageButtons.Add(buttonList[1]);
        }
    }
    private void UpdateButtonStateTwo(bool firstSetting, bool secondSetting, List<SettingBtnCust> buttonList)
    {
        if (firstSetting)
        {
            SettingPressedBtn(buttonList[0]);
        }
        else if (secondSetting)
        {
            SettingPressedBtn(buttonList[1]);
        }
    }
    #endregion

    // Event handlers for button actions.
    public void SettingPressedBtn(SettingBtnCust button)
    {
        // Get the other button in the pair.
        SettingBtnCust otherButton = button.isReso ? ResoSettingButtons[((int)button.nowOptions + 1) % 2] : LangSettingButtons[((int)button.nowOptions + 1) % 2];
        storageButtons.Remove(otherButton);
        if(!storageButtons.Exists(t=> t.Equals(button)))
        {
            storageButtons.Add(button);
        }
        SettingUnpressedBtn(otherButton);
        button.GetComponentInChildren<Text>().color = new Color(180 / 255f, 205 / 255f, 1f, 1f);
        button.colors = new ColorBlock {
            normalColor = new Color(1f, 1f, 1f, 1f),
            highlightedColor = Color.white,
            pressedColor = Color.red,
            selectedColor = Color.white,
            disabledColor = new Color(0.8f,0.8f,0.8f,0.8f),
            colorMultiplier = 1,
        };
        button.isPressed = true;
        SaveSettings();
    }


    public void SaveSettings()
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
                default:
                    break;
            }
        }
        storageSettings.Add(SFXSlider.value.ToString());
        storageSettings.Add(BGMSlider.value.ToString());
        foreach (var singleBtn in storageButtons)
        {
            switch (singleBtn.nowOptions)
            {
                case SettingBtnCust.E_nowOptions.EngLang:
                    LanguageManager.Instance.CurrentLanguage = LanguageOption.Chinese;
                    storageSettings.Add("engLang");
                    break;
                case SettingBtnCust.E_nowOptions.ChnLang:
                    LanguageManager.Instance.CurrentLanguage = LanguageOption.English;
                    storageSettings.Add("chnLang");
                    break;
                default:
                    break;
            }
        }
        SaveManager.Instance.SaveSettingsTwo(storageSettings);
    }

    public void SettingUnpressedBtn(SettingBtnCust button)
    {
        button.isPressed = false;
        button.GetComponentInChildren<Text>().color = new Color(180 / 255f, 205 / 255f, 1f, 1f);
        button.colors = new ColorBlock
        {
            normalColor = new Color(1f, 1f, 1f, 0f),
            highlightedColor = Color.white,
            pressedColor = Color.red,
            selectedColor = Color.white,
            disabledColor = new Color(0.8f, 0.8f, 0.8f, 0.8f),
            colorMultiplier = 1,
        };
    }

    
}
