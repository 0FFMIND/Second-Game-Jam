using UnityEngine;
using UnityEngine.UI;

public class IntroController : MonoBehaviour
{
    public GameObject introSave;
    public GameObject introText;
    bool isSaveEnd = false;
    bool isInit = false;
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
        //����һ����ʾ�˵�����ʾIntro�����Ѿ����浽continue������
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
            if (DialogManager.Instance.isIntroFinished)//��dialogManager�����ź�
            {
                if (!isSaveEnd)
                {
                    isSaveEnd = true;
                    SaveManager.Instance.SaveLevel();
                }
            }
            if (isSaveEnd)
            {
                introText.SetActive(true);
                introSave.SetActive(false);
                DialogManager.Instance.Init("Intro", IntroDialog);
                isSaveEnd = false;
                isInit = true;
            }
        }
        if((DialogManager.Instance.index == 0 || DialogManager.Instance.index == 5)&& isInit)
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
            TransManager.Instance.ChangeScene("OpenScene");
        }

    }
}
