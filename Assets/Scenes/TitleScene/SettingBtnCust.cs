using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SettingBtnCust : Button
{
    public enum E_nowOptions{
        highReso,
        lowReso,
        highSize,
        lowSize,
        EngLang,
        ChnLang
    }
    //提供Highlighted方法与Press方法给外界注册
    public event Action<SettingBtnCust> PressedBtn;
    public event Action<SettingBtnCust> UnPressedBtn;
    public event Action<SettingBtnCust> HighlightedBtn;
    public E_nowOptions nowOptions;
    public bool isFirst = false;
    public bool isPressed;
    public bool isHighlighted;
    //给Event Trigger提供
    public void SettingPointerEnter()
    {
        if (!isPressed)
        {
            AudioManager.Instance.PlaySFX(SoundEffect.Select);
        }
        isHighlighted = true;
        HighlightedBtn.Invoke(this);
    }
    public void SettingPointerExit()
    {
        isHighlighted = false;
        HighlightedBtn.Invoke(this);
    }
    public void Init() {
            if (isPressed)
            {
                PressedBtn.Invoke(this);
            }
            else if (!isPressed)
            {
                UnPressedBtn.Invoke(this);
            }
        }
}
