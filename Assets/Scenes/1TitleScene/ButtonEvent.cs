using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonEvent : MonoBehaviour
{
    public void TextSet()
    {
        GetComponentInChildren<Text>().color = new Color(180 / 255f, 205 / 255f, 1f, 1f);
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
    public void OnMouseClickTwo()
    {
        GetComponentInChildren<Text>().color = new Color(180 / 255f, 205 / 255f, 1f, 1f);
        AudioManager.Instance.PlaySFX(SoundEffect.Click);
    }
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
