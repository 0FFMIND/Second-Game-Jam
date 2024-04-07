using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveManager : Singleton<SaveManager>
{
    public bool IsHighResolution { get; private set; } = false;
    public bool IsLowResolution { get; private set; } = false;
    public float SFXvalue { get; private set; } = 0;
    public float BGMvalue { get; private set; } = 0;
    //涉及场景的存档
    public bool IsIntroEnd { get; set; } = false; //是否在介绍模式，牵扯到一些UI显示
    public bool IsOpenEnd { get; set; } = false;
    public bool IsLevelOneEnd { get; set; } = false;
    public bool IsPoolFinshed { get; set; } = false; // 选好了手牌直接跳过
    public int IsAngel { get; set; } = 0; // 0 表示还没有进行选择，1 表示天使侧，2 表示恶魔侧
    public int LevelOneStar { get; set; } = 0;
    public bool IsLevelTwoEnd { get; set; } = false;
    public int LevelTwoStar { get; set; } = 0;
    public bool isBuffOne { get; set; } = false;
    public bool isBuffTwo { get; set; } = false;
    public bool isBuffThree { get; set; } = false;
    public bool isBuffFour { get; set; } = false;
    public bool isBuffFive { get; set; } = false;
    public bool isBuffSix { get; set; } = false;
    public bool isLevelThreeEnd { get; set; } = false;
    public bool isLevelFourEnd { get; set; } = false;
    public bool isLevelFiveEnd { get; set; } = false;
    public bool isLevelSixEnd { get; set; } = false;
    //读档
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
        }
        //在系统初始化的时候读取文件
        LoadMenuSettings();

        ///这个后面必删
        
        //关于关卡
        if (!SaveWriter.LoadString("LevelSettings.json"))
        {
            PlayerPrefs.SetInt("Stars", 0);
            SaveWriter.Create("LevelSettings")
                .Write("Intro", "false")
                .Write("Open", "false")
                .Write("LevelOne", "false")
                .Write("LevelOneStar","0")
                .Write("Angel", "0")
                .Write("Pool","false")
                .Write("LevelTwo","false")
                .Write("LevelTwoStar","0")
                .Write("isBuffOne","false")
                .Write("isBuffTwo","false")
                .Write("isBuffThree","false")
                .Write("isBuffFour","false")
                .Write("isBuffFive","false")
                .Write("isBuffSix","false")
                .Write("isLevelThreeEnd","false")
                .Write("isLevelFourEnd","false")
                .Write("isLevelFiveEnd","false")
                .Write("isLevelSixEnd","false")
                .Commit();
        }
        LoadLevel();
    }
    public void InitLevelSetting()
    {
        string path = Application.persistentDataPath + "/playerData.csv";
        File.Delete(path);
        PlayerPrefs.SetInt("Star", 0);
        SaveWriter.Create("LevelSettings")
        .Write("Intro", "false")
        .Write("Open", "false")
        .Write("LevelOne", "false")
        .Write("LevelOneStar", "0")
        .Write("Angel", "0")
        .Write("Pool","false")
        .Write("LevelTwo", "false")
        .Write("LevelTwoStar", "0")
                        .Write("isBuffOne", "false")
                .Write("isBuffTwo", "false")
                .Write("isBuffThree", "false")
                .Write("isBuffFour", "false")
                .Write("isBuffFive", "false")
                .Write("isBuffSix", "false")
                        .Write("isLevelThreeEnd", "false")
                .Write("isLevelFourEnd", "false")
                .Write("isLevelFiveEnd", "false")
                .Write("isLevelSixEnd", "false")
        .Commit();
        LoadLevel();
    }
    public void SaveLevel()
    {
        SaveWriter.Create("LevelSettings")
            .Write("Intro", IsIntroEnd.ToString())
            .Write("Open", IsOpenEnd.ToString())
            .Write("LevelOne", IsLevelOneEnd.ToString())
            .Write("Angel", IsAngel.ToString())
            .Write("Pool", IsPoolFinshed.ToString())
            .Write("LevelOneStar", LevelOneStar.ToString())
            .Write("LevelTwo", IsLevelTwoEnd.ToString())
            .Write("LevelTwoStar", LevelTwoStar.ToString())
                                                .Write("isBuffOne", isBuffOne.ToString())
                .Write("isBuffTwo", isBuffTwo.ToString())
                .Write("isBuffThree", isBuffThree.ToString())
                .Write("isBuffFour", isBuffFour.ToString())
                .Write("isBuffFive", isBuffFive.ToString())
                .Write("isBuffSix", isBuffSix.ToString())
                        .Write("isLevelThreeEnd", isLevelThreeEnd.ToString())
            .Write("isLevelFourEnd", isLevelFourEnd.ToString())
            .Write("isLevelFiveEnd", isLevelFiveEnd.ToString())
            .Write("isLevelSixEnd", isLevelSixEnd.ToString())
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
            .Write("LevelOneStar", LevelOneStar.ToString())
            .Write("LevelTwo", IsLevelTwoEnd.ToString())
            .Write("LevelTwoStar", LevelTwoStar.ToString())
            .Write("isBuffOne", isBuffOne.ToString())
            .Write("isBuffTwo", isBuffTwo.ToString())
            .Write("isBuffThree", isBuffThree.ToString())
            .Write("isBuffFour", isBuffFour.ToString())
            .Write("isBuffFive", isBuffFive.ToString())
            .Write("isBuffSix", isBuffSix.ToString())
            .Write("isLevelThreeEnd",isLevelThreeEnd.ToString())
            .Write("isLevelFourEnd",isLevelFourEnd.ToString())
            .Write("isLevelFiveEnd",isLevelFiveEnd.ToString())
            .Write("isLevelSixEnd",isLevelSixEnd.ToString())
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
            .Read<string>("LevelOneStar", (r) =>
            {
                LevelOneStar = int.Parse(r);
             })
            .Read<string>("LevelTwo", (r) => {
                IsLevelTwoEnd = bool.Parse(r);
            })
            .Read<string>("LevelTwoStar", (r) => {
                LevelTwoStar = int.Parse(r);
            })
            .Read<string>("isBuffOne", (r) =>
            {
                isBuffOne = bool.Parse(r);
            })
            .Read<string>("isBuffTwo", (r) =>
            {
                 isBuffTwo = bool.Parse(r);
            })
            .Read<string>("isBuffThree", (r) =>
            {
                isBuffThree = bool.Parse(r);
            })
            .Read<string>("isBuffFour", (r) =>
            {
                isBuffFour = bool.Parse(r);
            })
            .Read<string>("isBuffFive", (r) =>
            {
                isBuffFive = bool.Parse(r);
            })
            .Read<string>("isBuffSix", (r) =>
            {
                isBuffSix = bool.Parse(r);
            })
            .Read<string>("isLevelThreeEnd", (r) =>
            {
                isLevelThreeEnd = bool.Parse(r);
            })
            .Read<string>("isLevelFourEnd", (r) =>
            {
                isLevelFourEnd = bool.Parse(r);
            })
            .Read<string>("isLevelFiveEnd", (r) =>
            {
                isLevelFiveEnd = bool.Parse(r);
            })
            .Read<string>("isLevelSixEnd", (r) =>
            {
                isLevelSixEnd = bool.Parse(r);
            })
            .Read<string>("Pool", (r) =>
            {
                IsPoolFinshed = bool.Parse(r);
            });
    }
    public void SaveMenuSettings(List<string> settingsPref)
    {
        SaveWriter.Create("Settings")
                  .Write("ResoOpt", settingsPref[0])
                  .Write("SFXvalue", settingsPref[1])
                  .Write("BGMvalue", settingsPref[2])
                  .Write("LangOpt", settingsPref[3])
                  .Commit();
        SaveAudioSettings();
    }
    public void SaveAudioSettings()
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

    public void LoadMenuSettings()
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
                        if (r == "chnLang")
                        {
                            LanguageManager.Instance.SwitchLanguage(LanguageOption.Chinese);
                            LanguageManager.Instance.hasLanguageChanged = true;
                        }
                        else if (r == "engLang")
                        {
                            LanguageManager.Instance.SwitchLanguage(LanguageOption.English);
                            LanguageManager.Instance.hasLanguageChanged = true;
                        }
                    });
    }
}
