using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveManager : Singleton<SaveManager>
{
    public bool IsHighResolution { get; private set; } = false;
    public bool IsLowResolution { get; private set; } = false;
    public float SFXvalue { get; private set; } = 0;
    public float BGMvalue { get; private set; } = 0;
    public bool IsEnglishLanguage { get; private set; } = false;
    public bool IsChineseLanguage { get; private set; } = false;
    public void Init()
    {
        if (!SaveWriter.LoadString("Settings.json"))
        {
            //事先不存在，则调用SaveWriter写入默认化设置
            SaveWriter.Create("Settings")
                      .Write("ResoOpt", "highReso")
                      .Write("SFXvalue", "0.5")
                      .Write("BGMvalue","0.5")
                      .Write("LangOpt", "chnLang")
                      .Commit();
            IsHighResolution = true;
            IsChineseLanguage = true;
        }
        //在系统初始化的时候读取文件
        LoadSettings();
    }
    public void SaveSettings(List<string> settingsPref)
    {
        SaveWriter.Create("Settings")
                  .Write("ResoOpt", settingsPref[0])
                  .Write("SFXvalue", settingsPref[1])
                  .Write("BGMvalue", settingsPref[2])
                  .Write("LangOpt", settingsPref[3])
                  .Commit();
        LoadSettings();
    }
    public void LoadSettings()
    {
        SaveReader.Create("Settings")
                  .Read<string>("ResoOpt", (r) =>
                    {
                        if (r == "highReso")
                        {
                            Screen.SetResolution(1920, 1080, true);
                            IsHighResolution = true;
                        }
                        else if (r == "lowReso")
                        {
                            Screen.SetResolution(1280, 720, true);
                            IsLowResolution = true;
                        }
                    })
                   .Read<string>("SFXvalue", (r) =>
                    {
                        AudioManager.Instance.ChangeSFXVolume(float.Parse(r));
                    })
                   .Read<string>("SFXvalue", (r) =>
                    {
                        AudioManager.Instance.ChangeBGMVolume(float.Parse(r));
                    })
                   .Read<string>("LangOpt", (r) =>
                    {
                        if (r == "engLang")
                        {
                            LanguageManager.Instance.CurrentLanguage = LanguageOption.English;
                            LanguageManager.Instance.hasLanguageChanged = true;
                        }
                        else if (r == "chnLang")
                        {
                            LanguageManager.Instance.CurrentLanguage = LanguageOption.Chinese;
                            LanguageManager.Instance.hasLanguageChanged = true;
                        }
                    });
    }
}
