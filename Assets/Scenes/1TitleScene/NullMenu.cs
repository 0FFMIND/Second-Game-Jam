using UnityEngine;

public class NullMenu : TitleMenuBase<NullMenu>
{
    public GameObject infoPop;
    public void PlaySelect()
    {
        AudioManager.Instance.PlaySFX(SoundEffect.Select);
    }
    public void PlayClick()
    {
        AudioManager.Instance.PlaySFX(SoundEffect.Click);
    }
    public void ReturnThink()
    {
        infoPop.SetActive(false);
        gameObject.GetComponentInParent<TitleController>().CloseMenu();
    }
    public void ChangeScene()
    {
        SaveManager.Instance.InitLevelSetting();
        TransManager.Instance.ChangeScene("IntroScene");
    }
    public override void OnBackPressed()
    {
        Close();
    }
}
