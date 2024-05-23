using UnityEngine;
using UnityEngine.UI;
public class IntroController : MonoBehaviour
{
    public GameObject FirstIntro;
    public GameObject SecondIntro;
    public DialogContent IntroDialog;
    public Image[] storyboards;
    private bool isOnce = false;
    public void Start()
    {
        AudioManager.Instance.StopBGM();
        AudioManager.Instance.PlayBGM(BackgroundMusic.IntroScene);
        FirstIntro.transform.parent.gameObject.SetActive(true);
        // 显示FirstIntro菜单并开始打字
        EventManager.Instance.AddEventListener<int>("dialogIndex", HandleIndex);
        DialogManager.Instance.BeginDialog(FirstIntro, IntroDialog, 0);
        EventManager.Instance.AddEventListener("dialogFinished",HandleFinished);
    }
    private void HandleIndex(int index)
    {
        if(index == 2 && !isOnce)
        {
            isOnce = true;
            FirstIntro.transform.parent.gameObject.SetActive(false);
            DialogManager.Instance.BeginDialog(SecondIntro, IntroDialog, 2);
        }
        if(index == 4)
        {
            storyboards[0].gameObject.SetActive(false);
        }
    }
    private void HandleFinished()
    {
        TransManager.Instance.ChangeScene("OpenScene");
    }
    public void PlayTypping()
    {
        AudioManager.Instance.PlaySFX(SoundEffect.Typing);
    }
}
