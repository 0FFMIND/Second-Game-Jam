using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    //挂载在每个小skill按键下面
    public SkillManager skillManager;
    public SkillData skillData;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        AudioManager.Instance.PlaySFX(SoundEffect.UISelect);
        skillManager.activeSkill = skillData;
        skillManager.DisplaySkillInfo();
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.Instance.PlaySFX(SoundEffect.UISelect);
        skillManager.activeSkill = skillData;
        skillManager.DisplaySkillInfo();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }
}
