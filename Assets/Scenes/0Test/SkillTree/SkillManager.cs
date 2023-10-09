using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillManager : MonoBehaviour
{
    //挂载在画布上面
    public SkillData activeSkill;
    [Header("UI")]
    public Text skillNameText, skillDesText;
    public GameObject AngleBtn;
    public GameObject DemonBtn;
    public GameObject skillOne;
    public GameObject skillTwo;
    public GameObject skillThree;
    public GameObject skillFour;
    public GameObject skillFive;
    public GameObject skillSix;
    public void Start()
    {
        SaveManager.Instance.LoadLevel();
        if (SaveManager.Instance.IsAngel == 1)
        {
            AngleBtn.SetActive(true);
            DemonBtn.SetActive(false);
        }
        else if (SaveManager.Instance.IsAngel == 2)
        {
            AngleBtn.SetActive(false);
            DemonBtn.SetActive(true);
        }
        if(SaveManager.Instance.isBuffOne == true)
        {
            skillOne.GetComponent<Image>().color = new Color(1f, 1f, 1f,1f);
        }
        if (SaveManager.Instance.isBuffThree == true)
        {
            skillThree.GetComponent<Image>().color = new Color(1f, 1f, 1f,1f);
        }
        if (SaveManager.Instance.isBuffFive == true)
        {
            skillFive.GetComponent<Image>().color = new Color(1f, 1f, 1f,1f);
        }
        if (SaveManager.Instance.isBuffTwo)
        {
            skillTwo.GetComponent<Image>().color = new Color(1f, 1f, 0f, 1f);
        }
        if (SaveManager.Instance.isBuffFour)
        {
            skillFour.GetComponent<Image>().color = new Color(1f, 1f, 0f, 1f);
        }
        if (SaveManager.Instance.isBuffSix)
        {
            skillSix.GetComponent<Image>().color = new Color(1f, 1f, 0f, 1f);
        }
    }
    public void AngleTwoClick()
    {
        SaveManager.Instance.LoadLevel();
        if (SaveManager.Instance.isBuffOne && SaveManager.Instance.isBuffThree && SaveManager.Instance.isBuffFive) return;
        int num = PlayerPrefs.GetInt("Star") -2;
        if (num < 0) return;
        PlayerPrefs.SetInt("Star", num);
        if (!SaveManager.Instance.isBuffOne)
        {
            SaveManager.Instance.isBuffOne = true;
            SaveManager.Instance.SaveLevel();
            SaveManager.Instance.LoadLevel();
            skillOne.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            return;
        }
        if(SaveManager.Instance.isBuffOne && !SaveManager.Instance.isBuffThree)
        {
            SaveManager.Instance.isBuffThree = true;
            SaveManager.Instance.SaveLevel();
            SaveManager.Instance.LoadLevel();
            skillThree.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            return;
        }
        if (SaveManager.Instance.isBuffOne && SaveManager.Instance.isBuffThree && !SaveManager.Instance.isBuffFive)
        {
            SaveManager.Instance.isBuffFive = true;
            SaveManager.Instance.SaveLevel();
            SaveManager.Instance.LoadLevel();
            skillFive.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            return;
        }
    }
    public void AngleFourClick()
    {
        SaveManager.Instance.LoadLevel();
        if (SaveManager.Instance.isBuffOne && SaveManager.Instance.isBuffThree && SaveManager.Instance.isBuffFive) return;
        int num = PlayerPrefs.GetInt("Star") - 4;
        if (num < 0) return;
        PlayerPrefs.SetInt("Star", num);
        if (!SaveManager.Instance.isBuffOne)
        {
            SaveManager.Instance.isBuffOne = true;
            SaveManager.Instance.SaveLevel();
            SaveManager.Instance.LoadLevel();
            skillOne.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            return;
        }
        if (SaveManager.Instance.isBuffOne && !SaveManager.Instance.isBuffThree)
        {
            SaveManager.Instance.isBuffThree = true;
            SaveManager.Instance.SaveLevel();
            SaveManager.Instance.LoadLevel();
            skillThree.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            return;
        }
        if (SaveManager.Instance.isBuffOne && SaveManager.Instance.isBuffThree && !SaveManager.Instance.isBuffFive)
        {
            SaveManager.Instance.isBuffFive = true;
            SaveManager.Instance.SaveLevel();
            SaveManager.Instance.LoadLevel();
            skillFive.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            return;
        }
    }
    public void DemonTwoClick()
    {
        SaveManager.Instance.LoadLevel();
        if (SaveManager.Instance.isBuffTwo && SaveManager.Instance.isBuffFour && SaveManager.Instance.isBuffSix) return;
        int num = PlayerPrefs.GetInt("Star") - 2;
        if (num < 0) return;
        PlayerPrefs.SetInt("Star", num);
        if (!SaveManager.Instance.isBuffTwo)
        {
            SaveManager.Instance.isBuffTwo = true;
            SaveManager.Instance.SaveLevel();
            SaveManager.Instance.LoadLevel();
            skillTwo.GetComponent<Image>().color = new Color(1f, 1f, 0f, 1f);
            return;
        }
        if (SaveManager.Instance.isBuffTwo && !SaveManager.Instance.isBuffFour)
        {
            SaveManager.Instance.isBuffFour = true;
            SaveManager.Instance.SaveLevel();
            SaveManager.Instance.LoadLevel();
            skillFour.GetComponent<Image>().color = new Color(1f, 1f, 0f, 1f);
            return;
        }
        if (SaveManager.Instance.isBuffOne && SaveManager.Instance.isBuffFour && !SaveManager.Instance.isBuffSix)
        {
            SaveManager.Instance.isBuffSix = true;
            SaveManager.Instance.SaveLevel();
            SaveManager.Instance.LoadLevel();
            skillSix.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            return;
        }
    }
    public void DemonFourClick()
    {
        SaveManager.Instance.LoadLevel();
        if (SaveManager.Instance.isBuffTwo && SaveManager.Instance.isBuffFour && SaveManager.Instance.isBuffSix) return;
        int num = PlayerPrefs.GetInt("Star") - 4;
        if (num < 0) return;
        PlayerPrefs.SetInt("Star", num);
        if (!SaveManager.Instance.isBuffTwo)
        {
            SaveManager.Instance.isBuffTwo = true;
            SaveManager.Instance.SaveLevel();
            SaveManager.Instance.LoadLevel();
            skillTwo.GetComponent<Image>().color = new Color(1f, 1f, 0f, 1f);
            return;
        }
        if (SaveManager.Instance.isBuffTwo && !SaveManager.Instance.isBuffFour)
        {
            SaveManager.Instance.isBuffFour = true;
            SaveManager.Instance.SaveLevel();
            SaveManager.Instance.LoadLevel();
            skillFour.GetComponent<Image>().color = new Color(1f, 1f, 0f, 1f);
            return;
        }
        if (SaveManager.Instance.isBuffOne && SaveManager.Instance.isBuffFour && !SaveManager.Instance.isBuffSix)
        {
            SaveManager.Instance.isBuffSix = true;
            SaveManager.Instance.SaveLevel();
            SaveManager.Instance.LoadLevel();
            skillSix.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            return;
        }
    }
    public void DisplaySkillInfo()
    {
        if(LanguageManager.Instance.CurrentLanguage == LanguageOption.Chinese)
        {
            skillNameText.text = activeSkill.skillNameCN;
            skillDesText.text = activeSkill.skillDetailCN;
        }if(LanguageManager.Instance.CurrentLanguage == LanguageOption.English)
        {
            skillNameText.text = activeSkill.skillNameEN;
            skillDesText.text = activeSkill.skillDetailEN;
        }
    }

}
