using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonEvent : MonoBehaviour
{
    /// <summary>
    /// 将颜色恢复
    /// </summary>
    public void TextSet()
    {
        GetComponentInChildren<Text>().color = new Color(180 / 255f, 205 / 255f, 1f, 1f);
    }
    /// <summary>
    /// 将字体设置为黑
    /// </summary>
    public void TextSetBlack()
    {
        GetComponentInChildren<Text>().color = Color.black;
    }
    /// <summary>
    /// 点击音效，改个色
    /// </summary>
    public void OnMouseClick()
    {
        GetComponentInChildren<Text>().color = new Color(180 / 255f, 205 / 255f, 1f, 1f);
        AudioManager.Instance.PlaySFX(SoundEffect.Click);
    }
    public void OnMouseClickTwo()
    {
        GetComponentInChildren<Text>().color = new Color(180 / 255f, 205 / 255f, 1f, 1f);
        AudioManager.Instance.PlaySFX(SoundEffect.Click);
    }
    /// <summary>
    /// 播放Select音频
    /// </summary>
    public void OnMouseSelected()
    {
        AudioManager.Instance.PlaySFX(SoundEffect.Select);
    }
    public void OnMouseSelectedTwo()
    {
        GetComponentInChildren<Text>().color = Color.blue;
        AudioManager.Instance.PlaySFX(SoundEffect.Select);
    }
    public void OnMouseExitTwo()
    {
        GetComponentInChildren<Text>().color = new Color(180 / 255f, 205 / 255f, 1f, 1f);
        AudioManager.Instance.PlaySFX(SoundEffect.Select);
    }
    public void SettingSounds()
    {
        AudioManager.Instance.PlaySFX(SoundEffect.Select);
    }
    public void SettingClickSounds()
    {
        AudioManager.Instance.PlaySFX(SoundEffect.Click);
    }
}
