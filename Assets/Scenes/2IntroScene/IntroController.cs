using UnityEngine;
using UnityEngine.UI;

public class IntroController : MonoBehaviour
{
    public static IntroController Instance;
    public GameObject introSave;
    public GameObject introText;
    public bool isSaveEnd = false;
    public static bool isInit = false;
    public DialogContent BeforeIntroDialog,
                         IntroDialog;
    public Image[] storyboards;
    public static bool isFinished = false;
    public void Start()
    {
        DialogManager.Instance.isBEfinished = false;
        AudioManager.Instance.StopBGM();
        AudioManager.Instance.PlayBGM(BackgroundMusic.IntroScene);
        DialogManager.Instance.isIntroFinished = false;
        isInit = false;
        isSaveEnd = false;
        //弹出一个显示菜单，显示Intro界面已经储存到continue里面了
        DialogManager.Instance.Init("beforeIntro", BeforeIntroDialog);
        introSave.SetActive(true);
    }
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
        if (!isInit)
        {
            if (DialogManager.Instance.isIntroFinished)//用dialogManager来传信号
            {
                introText.SetActive(true);
                introSave.SetActive(false);
                DialogManager.Instance.Init("Intro", IntroDialog);
                isInit = true;
            }
        }
        if((DialogManager.Instance.index == 0 || DialogManager.Instance.index == 4)&& isInit)
        {
            int index = DialogManager.Instance.index / 4;
            foreach (var image in storyboards)
            {
                image.gameObject.SetActive(false);
            }
            storyboards[index].gameObject.SetActive(true);
        }
        if (DialogManager.Instance.isBEfinished)
        {
            SaveManager.Instance.IsIntroEnd = true;
            SaveManager.Instance.SaveLevel();
            SaveManager.Instance.LoadLevel();
            TransManager.Instance.ChangeScene("OpenScene");
        }

    }
}
