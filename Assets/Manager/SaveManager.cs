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
    //�漰�����Ĵ浵
    public bool IsIntroEnd { get; set; } = false;
    public bool IsOpenEnd { get; set; } = false;
    public bool IsLevelOneEnd { get; set; } = false;
    public bool IsPoolFinshed { get; set; } = false; // ѡ��������ֱ������
    public int IsAngel { get; set; } = 0; // 0 ��ʾ��û�н���ѡ��1 ��ʾ��ʹ�࣬2 ��ʾ��ħ��
    //����
    public void Init()
    {
        if (!SaveWriter.LoadString("Settings.json"))
        {
            //���Ȳ����ڣ������SaveWriterд��Ĭ�ϻ�����
            SaveWriter.Create("Settings")
                      .Write("ResoOpt", "highReso")
                      .Write("SFXvalue", "0.5")
                      .Write("BGMvalue","0.5")
                      .Write("LangOpt", "chnLang")
                      .Commit();
            IsHighResolution = true;
            IsChineseLanguage = true;
        }
        //��ϵͳ��ʼ����ʱ���ȡ�ļ�
        LoadSettings();
        //���ڹؿ�
        if (!SaveWriter.LoadString("LevelSettings.json"))
        {
            SaveWriter.Create("LevelSettings")
                .Write("Intro", "false")
                .Write("Open", "false")
                .Write("LevelOne", "false")
                .Write("Angel", "0")
                .Write("Pool","false")
                .Commit();
        }
        LoadLevel();
    }
    public void InitLevelSetting()
    {
        string path = Application.persistentDataPath + "/playerData.csv";
        File.Delete(path);
        SaveWriter.Create("LevelSettings")
        .Write("Intro", "false")
        .Write("Open", "false")
        .Write("LevelOne", "false")
        .Write("Angel", "0")
        .Write("Pool","false")
        .Commit();
        LoadLevel();
    }
    public void SaveLevel()
    {
        IsIntroEnd = true;
        IsOpenEnd = false;
        SaveWriter.Create("LevelSettings")
            .Write("Intro", "true")
            .Write("Open", "false")
            .Write("LevelOne", IsLevelOneEnd.ToString())
            .Write("Angel", IsAngel.ToString())
            .Write("Pool",IsPoolFinshed.ToString())
            .Commit();
    }
    public void SetTeam(int select)
    {
        IsAngel = select;
        SaveWriter.Create("LevelSettings")
            .Write("Angel", select.ToString())
            .Commit();
    }
    public void SaveLevelFalse()
    {
        IsIntroEnd = false;
        IsOpenEnd = false;
        SaveWriter.Create("LevelSettings")
            .Write("Intro", "false")
            .Write("Open", "false")
            .Write("LevelOne", IsLevelOneEnd.ToString())
            .Write("Angel", IsAngel.ToString())
            .Write("Pool",IsPoolFinshed.ToString())
            .Commit();
    }
    public void SaveOpen()
    {
        IsOpenEnd = true;
        SaveWriter.Create("LevelSettings")
            .Write("Intro", "true")
            .Write("Open", "true")
            .Commit();
    }
    public void LoadLevel()
    {
        SaveReader.Create("LevelSettings")
            .Read<string>("Intro", (r) =>
            {
                IsIntroEnd = bool.Parse(r);
            })
            .Read<string>("Open", (r) =>
            {
                IsOpenEnd = bool.Parse(r);
            })
            .Read<string>("LevelOne", (r) =>
            {
                IsLevelOneEnd = bool.Parse(r);
            })
            .Read<string>("Angel", (r) =>
             {
                 IsAngel = int.Parse(r);
             })
            .Read<string>("Pool", (r) =>
            {
                IsPoolFinshed = bool.Parse(r);
            });
    }
    public void SaveSettingsTwo(List<string> settingsPref)
    {
        SaveWriter.Create("Settings")
                  .Write("ResoOpt", settingsPref[0])
                  .Write("SFXvalue", settingsPref[1])
                  .Write("BGMvalue", settingsPref[2])
                  .Write("LangOpt", settingsPref[3])
                  .Commit();
        LoadSettingsTwo();
    }
    public void LoadSettingsTwo()
    {
        SaveReader.Create("Settings")
                   .Read<string>("SFXvalue", (r) =>
                   {
                       AudioManager.Instance.ChangeSFXVolume(float.Parse(r));
                       SFXvalue = float.Parse(r);
                   })
                   .Read<string>("BGMvalue", (r) =>
                   {
                       AudioManager.Instance.ChangeBGMVolume(float.Parse(r));
                       BGMvalue = float.Parse(r);
                   });
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
                        SFXvalue = float.Parse(r);
                    })
                   .Read<string>("BGMvalue", (r) =>
                    {
                        AudioManager.Instance.ChangeBGMVolume(float.Parse(r));
                        BGMvalue = float.Parse(r);
                    })
                   .Read<string>("LangOpt", (r) =>
                    {
                        if (r == "engLang")
                        {
                            LanguageManager.Instance.SwitchLanguage(LanguageOption.English);
                            LanguageManager.Instance.hasLanguageChanged = true;
                            IsEnglishLanguage = true;
                        }
                        else if (r == "chnLang")
                        {
                            LanguageManager.Instance.SwitchLanguage(LanguageOption.Chinese);
                            LanguageManager.Instance.hasLanguageChanged = true;
                            IsChineseLanguage = true;
                        }
                    });
    }
}
