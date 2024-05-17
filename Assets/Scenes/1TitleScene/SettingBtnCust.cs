using System;
using UnityEngine;
using UnityEngine.UI;
public class SettingBtnCust : Button
{
    public enum E_nowOptions{
        highReso,
        lowReso,
        EngLang,
        ChnLang
    }
    //提供Highlighted方法与Press方法给外界注册
    public event Action<SettingBtnCust> PressedBtn;
    public event Action<SettingBtnCust> UnPressedBtn;
    public E_nowOptions nowOptions;
    public bool isReso = false;
    public bool isPressed = false;
    //给Event Trigger提供
    public void SettingPointerEnter()
    {
        if (!isPressed)
        {
            AudioManager.Instance.PlaySFX(SoundEffect.Select);
        }
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
