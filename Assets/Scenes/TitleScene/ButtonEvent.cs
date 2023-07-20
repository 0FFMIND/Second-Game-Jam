using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonEvent : MonoBehaviour
{
    public void TextSet()
    {
        GetComponentInChildren<Text>().color = Color.white;
    }
    public void TextSetBlack()
    {
        GetComponentInChildren<Text>().color = Color.black;
    }
    public void OnMouseClick()
    {
        GetComponentInChildren<Text>().color = Color.black;
        AudioManager.Instance.PlaySFX(SoundEffect.Click);
    }
    public void OnMouseSelected()
    {
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
