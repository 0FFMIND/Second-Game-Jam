using UnityEngine;

public class LevelNewIntro : MonoBehaviour
{
    public GameObject introBG;
    public GameObject introText;
    public DialogContent IntroDialog;
    public static bool isFinished = false;
    public void Start()
    {
        AudioManager.Instance.StopBGM();
        AudioManager.Instance.PlayBGM(BackgroundMusic.TitleScene);
        DialogManager.Instance.isIntroFinished = false;
        DialogManager.Instance.Init("Intro", IntroDialog);
        introBG.SetActive(true);
    }
    // ��TextAnimator�õ��������
    public void PlayTypping()
    {
        AudioManager.Instance.PlaySFX(SoundEffect.Typing);
    }
    public void SetFinishedFalse()
    {
        isFinished = false;
    }
    public void SetFinished()
    {
        isFinished = true;
    }
    public void Update()
    {
        if (DialogManager.Instance.isIntroFinished)//��dialogManager�����ź�
        {
            SaveManager.Instance.SaveOpen();
            this.gameObject.SetActive(false);
        }
    }
}
