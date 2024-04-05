using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMix : MonoBehaviour
{
    public AudioMixer audioMixer;
    public void SetSFX(float volume)
    {
        audioMixer.SetFloat("SFXVolume", volume);
        audioMixer.SetFloat("MasterVolume", volume);
    }
    public void SetBGM(float volume)
    {
        audioMixer.SetFloat("BGMVolume", volume);
        audioMixer.SetFloat("MasterVolume", volume);
    }
}
