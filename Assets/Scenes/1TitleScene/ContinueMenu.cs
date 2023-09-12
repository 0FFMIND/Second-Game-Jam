using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueMenu : TitleMenuBase<ContinueMenu>
{
    public void OnIntroPressed()
    {
        TransManager.Instance.ChangeScene("IntroScene");
    }
    public void OnLevelOnePressed()
    {
        TransManager.Instance.ChangeScene("LevelOne");
    }
    public void OnIntrotwoPressed()
    {
        TransManager.Instance.ChangeScene("IntroTwoScene");
    }
    public void OnLevelTwoPressed()
    {
        TransManager.Instance.ChangeScene("LevelTwo");
    }
    public override void OnBackPressed()
    {
        Close();
    }
}
