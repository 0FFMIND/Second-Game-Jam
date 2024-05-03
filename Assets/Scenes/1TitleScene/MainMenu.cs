using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : TitleMenuBase<MainMenu>
{
    public GameObject infoTrigger;
    public void OnStartPressed()
    {
        if (SaveManager.Instance.IsIntroEnd)
        {
            gameObject.GetComponentInParent<TitleController>().nullMenu.Open();
            infoTrigger.SetActive(true);
        }
        else
            TransManager.Instance.ChangeScene("IntroScene");
    }

    public void OnContinuePressed()
    {
        if (SaveManager.Instance.IsIntroEnd)
        {
            TransManager.Instance.ChangeScene("OpenScene");
            
        }else if (!SaveManager.Instance.IsIntroEnd)
        {
            TransManager.Instance.ChangeScene("IntroScene");
        }
    }
    public void OnDeveloperPressed()
    {
        ShowSubBackground();
        gameObject.GetComponentInParent<TitleController>().developerMenu.Open();
    }
    public void OnSettingPressed()
    {
        ShowSubBackground();
        gameObject.GetComponentInParent<TitleController>().settingMenu.Open();
    }
    public override void OnBackPressed()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
    private void ShowSubBackground()
    {
        gameObject.GetComponentInParent<TitleController>().mainBackground.SetActive(false);
        gameObject.GetComponentInParent<TitleController>().subBackground.SetActive(true);
    }
}
