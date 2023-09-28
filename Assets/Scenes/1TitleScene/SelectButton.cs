using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectButton : MonoBehaviour
{
    private List<SettingBtnCust> firstButtons = new List<SettingBtnCust>();
    private List<SettingBtnCust> secondButtons = new List<SettingBtnCust>();
    private List<SettingBtnCust> storageButtons = new List<SettingBtnCust>();
    public Slider SFXSlider;
    public Slider BGMSlider;
    
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
    private void Start()
    {
        SetButtons();
    }
    private void OnEnable()
    {
        if (SaveManager.Instance.SFXvalue < SFXSlider.minValue) SFXSlider.value = SFXSlider.minValue;
        if (SaveManager.Instance.BGMvalue < BGMSlider.minValue) BGMSlider.value = BGMSlider.minValue;
        if (SaveManager.Instance.SFXvalue > SFXSlider.maxValue) SFXSlider.value = SFXSlider.maxValue;
        if (SaveManager.Instance.BGMvalue > BGMSlider.maxValue) BGMSlider.value = BGMSlider.maxValue;
        if ((SaveManager.Instance.SFXvalue >= SFXSlider.minValue) && (SaveManager.Instance.SFXvalue <= SFXSlider.maxValue))
        {
            SFXSlider.value = SaveManager.Instance.SFXvalue;
        }
        if((SaveManager.Instance.BGMvalue >= BGMSlider.minValue) && (SaveManager.Instance.BGMvalue <= BGMSlider.maxValue))
        {
            BGMSlider.value = SaveManager.Instance.BGMvalue;
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
    }

    // Move button initialization to a separate method for clarity and reuse.
    private void InitializeButton(List<SettingBtnCust> buttonList, Transform[] parentTransforms, int parentIndex, int childIndex)
    {
        SettingBtnCust button = parentTransforms[parentIndex].GetChild(childIndex).gameObject.GetComponent<SettingBtnCust>();
        buttonList.Add(button);
        button.nowOptions = (SettingBtnCust.E_nowOptions)(2 * parentIndex + childIndex);
        button.isFirst = childIndex == 1;
        button.PressedBtn += SettingPressedBtn;
        button.UnPressedBtn += SettingUnpressedBtn;
        button.HighlightedBtn += SettingHighlightedButton;
    }

    // Update button states based on saved settings.
    private void UpdateButtonsFromSavedSettings()
    {
        storageButtons.Clear();
        UpdateButtonState(SaveManager.Instance.IsHighResolution, SaveManager.Instance.IsLowResolution, 0);
        UpdateButtonState(SaveManager.Instance.IsEnglishLanguage, SaveManager.Instance.IsChineseLanguage, 1);
        UpdateButtonStateTwo(SaveManager.Instance.IsHighResolution, SaveManager.Instance.IsLowResolution, 0);
        UpdateButtonStateTwo(SaveManager.Instance.IsEnglishLanguage, SaveManager.Instance.IsChineseLanguage, 1);
    }
    // Update the state of a pair of buttons based on their saved settings.
    private void UpdateButtonState(bool firstSetting, bool secondSetting, int index)
    {
        if (firstSetting)
        {
            storageButtons.Add(firstButtons[index]);
        }
        else if (secondSetting)
        {
            storageButtons.Add(secondButtons[index]);
        }
    }
    private void UpdateButtonStateTwo(bool firstSetting, bool secondSetting, int index)
    {
        if (firstSetting)
        {
            SettingPressedBtn(firstButtons[index]);
        }
        else if (secondSetting)
        {
            SettingPressedBtn(secondButtons[index]);
        }
    }
    // Event handlers for button actions.
    public void SettingPressedBtn(SettingBtnCust button)
    {
        // Get the other button in the pair.
        SettingBtnCust otherButton = button.isFirst ? firstButtons[(int)button.nowOptions / 2] : secondButtons[(int)button.nowOptions / 2];
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

    private void SettingHighlightedButton(SettingBtnCust button)
    {
        button.isHighlighted = true;
    }
}
