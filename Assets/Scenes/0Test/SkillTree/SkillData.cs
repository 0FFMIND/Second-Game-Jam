using UnityEngine;
[CreateAssetMenu(menuName = "New Skill", fileName = "Skill")]
public class SkillData : ScriptableObject
{
    public int skillID;
    public string skillNameCN;
    public string skillNameEN;
    [TextArea(1, 8)]
    public string skillDetailCN;
    [TextArea(1, 8)]
    public string skillDetailEN;
    public bool isUnlocked;
    public SkillData[] preSkills;
}
