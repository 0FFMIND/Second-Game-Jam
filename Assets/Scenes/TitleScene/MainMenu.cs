using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : TitleMenuBase<MainMenu>
{
    public void OnStartPressed()
    {
        TransManager.Instance.ChangeScene("IntroScene");
    }
    public void OnContinuePressed()
    {
        gameObject.GetComponentInParent<TitleController>().continueMenu.Open();
    }
    public void OnDeveloperPressed()
    {
        gameObject.GetComponentInParent<TitleController>().developerMenu.Open();
    }
    public void OnSettingPressed()
    {
        gameObject.GetComponentInParent<TitleController>().mainBackground.SetActive(false);
        gameObject.GetComponentInParent<TitleController>().subBackground.SetActive(true);
        gameObject.GetComponentInParent<TitleController>().settingMenu.Open();
    }
    public override void OnBackPressed()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
