using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveManager : Singleton<SaveManager>
{
    public bool ishighReso = false;
    public bool islowReso = false;
    public bool ishighSize = false;
    public bool islowSize = false;
    public bool isengLang = false;
    public bool ischnLang = false;
    public void Init()
    {
        if (!SaveWriter.LoadString("Settings.json"))
        {
            //事先不存在，则调用SaveWriter写入默认化设置
            SaveWriter.Create("Settings")
                      .Write("ResoOpt", "highReso")
                      .Write("SizeOpt", "highSize")
                      .Write("LangOpt", "engLang")
                      .Commit();
            ishighReso = true;
            ishighSize = true;
            isengLang = true;
        }
        //在系统初始化的时候读取文件
        LoadSettings();
    }
    public void SaveSettings(List<string> settingsPref)
    {
        SaveWriter.Create("Settings")
                  .Write("ResoOpt", settingsPref[0])
                  .Write("SizeOpt", settingsPref[1])
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
                            ishighReso = true;
                        }
                        else if (r == "lowReso") {
                            Screen.SetResolution(1280, 720, true);
                            islowReso = true;
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
