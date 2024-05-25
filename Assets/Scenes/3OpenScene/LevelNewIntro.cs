using UnityEngine;
using UnityEngine.Playables;

public class LevelNewIntro : MonoBehaviour
{
    public GameObject introText;
    public DialogContent introDialog;
    public static bool isFinished = false;
    // ���������Timeline�Ŀ���
    public PlayableDirector introNewDirector;
    private void Start()
    {
        AudioManager.Instance.StopBGM();
        AudioManager.Instance.PlayBGM(BackgroundMusic.TitleScene);
        if (!SaveManager.Instance.IsOpenEnd)
        {
            introText.transform.parent.gameObject.SetActive(true);
            // �����ʼ�Ĵ����߼�
            DialogManager.Instance.BeginDialog(introText, introDialog, 0);
            EventManager.Instance.AddEventListener("dialogFinished", HandleFinished);
        }
    }
    private void HandleFinished()
    {
        EventManager.Instance.RemoveEventListener("dialogFinished", HandleFinished);
        SaveManager.Instance.IsOpenEnd = true;
        SaveManager.Instance.SaveLevel();
        introNewDirector.Play();
        introText.transform.parent.gameObject.SetActive(false);
    }
    public void PlayTypping()
    {
        AudioManager.Instance.PlaySFX(SoundEffect.Typing);
    }
}
