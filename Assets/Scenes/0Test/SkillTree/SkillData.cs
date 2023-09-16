using UnityEngine;
[CreateAssetMenu(menuName = "New Skill", fileName = "Skill")]
public class SkillData : ScriptableObject
{
    public int skillID;
    public Sprite skillSprite;
    public string skillNameCN;
    public string skillNameEN;
    public int skillLevel;
    [TextArea(1, 8)]
    public string skillDetailCN;
    [TextArea(1, 8)]
    public string skillDetailEN;
    public bool isUnlocked;
    public SkillData[] preSkills;
}
