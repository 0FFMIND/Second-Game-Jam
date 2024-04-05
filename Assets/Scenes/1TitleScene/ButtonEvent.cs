using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonEvent : MonoBehaviour
{
    /// <summary>
    /// ����ɫ�ָ�
    /// </summary>
    public void TextSet()
    {
        GetComponentInChildren<Text>().color = new Color(180 / 255f, 205 / 255f, 1f, 1f);
    }
    /// <summary>
    /// ����������Ϊ��
    /// </summary>
    public void TextSetBlack()
    {
        GetComponentInChildren<Text>().color = Color.black;
    }
    /// <summary>
    /// �����Ч���ĸ�ɫ
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
    /// ����Select��Ƶ
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
