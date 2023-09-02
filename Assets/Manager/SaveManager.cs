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
    }
    public void SaveSettings(List<string> settingsPref)
    {
        SaveWriter.Create("Settings")
                  .Write("ResoOpt", settingsPref[0])
                  //TO DO
                  .Write("SFXvalue", "0.5")
                  .Write("BGMvalue", "0.5")
                  .Write("LangOpt", settingsPref[2])
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
                   .Read<string>("SizeOpt", (r) =>
                    {
                        if (r == "highSize")
                        {
                            Screen.fullScreen = true;
                            ishighSize = true;
                        }
                        else if (r == "lowSize")
                        {
                            Screen.fullScreen = false;
                            islowSize = true;
                        }
                    })
                    .Read<string>("LangOpt", (r) =>
                    {
                        if (r == "engLang")
                        {
                            LanguageManager.Instance.nowOption = LanguageOption.English;
                            isengLang = true;
                        }
                        else if (r == "chnLang")
                        {
                            LanguageManager.Instance.nowOption = LanguageOption.Chinese;
                            ischnLang = true;
                        }
                    });
    }
}