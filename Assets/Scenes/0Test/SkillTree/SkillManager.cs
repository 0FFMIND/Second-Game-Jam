using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    //�����ڻ�������
    public SkillData activeSkill;
    [Header("UI")]
    public Image skillImage;
    public Text skillNameText, skillLvText, skillDesText;
    public void DisplaySkillInfo()
    {
        skillImage.sprite = activeSkill.skillSprite;
        skillNameText.text = activeSkill.skillNameCN;
        skillDesText.text = "������ϸ�� \n" + activeSkill.skillDetailCN; // ���ı�
        skillLvText.text = "����ǰ���ܵĵȼ���" + activeSkill.skillLevel;
    }
}
