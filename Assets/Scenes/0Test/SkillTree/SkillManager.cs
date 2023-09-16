using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    //挂载在画布上面
    public SkillData activeSkill;
    [Header("UI")]
    public Image skillImage;
    public Text skillNameText, skillLvText, skillDesText;
    public void DisplaySkillInfo()
    {
        skillImage.sprite = activeSkill.skillSprite;
        skillNameText.text = activeSkill.skillNameCN;
        skillDesText.text = "技能详细： \n" + activeSkill.skillDetailCN; // 富文本
        skillLvText.text = "您当前技能的等级：" + activeSkill.skillLevel;
    }
}
