using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundEffect
{
    Click,
    Select,
    Typing,
    Mousehit,
    Stronghit,
}
public enum BackgroundMusic
{
    TitleScene,
    IntroScene,
    LevelOneScene,
    LevelTwoScene,
}
public class AudioManager : Singleton<AudioManager>
{
    [Header("Sound Effects")]
    public AudioSource[] sfx;
    [Header("Background Music")]
    public AudioSource[] bgm;
    public void PlaySFX(SoundEffect soundEffectenum)
    {
        if((int)soundEffectenum < sfx.Length)
        {
            sfx[(int)soundEffectenum].Play();
        }
    }
    public void StopBGM()
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            bgm[i].Stop();
        }
    }
    public void PlayBGM(BackgroundMusic backgroundMusicenum)
    {
        foreach (var single in bgm)
        {
            single.Stop();
        }
        if((int)backgroundMusicenum < bgm.Length)
        {
            bgm[(int)backgroundMusicenum].Play();
        }
    }

}
