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
    //�ṩHighlighted������Press���������ע��
    public event Action<SettingBtnCust> PressedBtn;
    public event Action<SettingBtnCust> UnPressedBtn;
    public E_nowOptions nowOptions;
    public bool isReso = false;
    public bool isPressed = false;
    //��Event Trigger�ṩ
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
