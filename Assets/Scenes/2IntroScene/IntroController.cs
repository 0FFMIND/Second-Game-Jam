using UnityEngine;
using UnityEngine.UI;
public class IntroController : MonoBehaviour
{
    public GameObject firstIntro;
    public GameObject secondIntro;
    public DialogContent introDialog;
    public Image[] storyBoards;
    private bool isOnce = false;
    private void Start()
    {
        AudioManager.Instance.StopBGM();
        AudioManager.Instance.PlayBGM(BackgroundMusic.IntroScene);
        firstIntro.transform.parent.gameObject.SetActive(true);
        // 显示FirstIntro菜单并开始打字
        EventManager.Instance.AddEventListener<int>("dialogIndex", HandleIndex);
        DialogManager.Instance.BeginDialog(firstIntro, introDialog, 0);
        EventManager.Instance.AddEventListener("dialogFinished",HandleFinished);
    }
    private void HandleIndex(int index)
    {
        if(index == 2 && !isOnce)
        {
            isOnce = true;
            firstIntro.transform.parent.gameObject.SetActive(false);
            DialogManager.Instance.BeginDialog(secondIntro, introDialog, 2);
        }
        if(index == 4)
        {
            storyBoards[0].gameObject.SetActive(false);
        }
    }
    private void HandleFinished()
    {
        EventManager.Instance.RemoveEventListener<int>("dialogIndex", HandleIndex);
        EventManager.Instance.RemoveEventListener("dialogFinished", HandleFinished);
        SaveManager.Instance.IsIntroEnd = true;
        SaveManager.Instance.SaveLevel();
        TransManager.Instance.ChangeScene("OpenScene");
    }
    public void PlayTypping()
    {
        AudioManager.Instance.PlaySFX(SoundEffect.Typing);
    }
}
