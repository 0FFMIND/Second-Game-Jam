using TMPro;
using UnityEngine;

public class BELevelText : MonoBehaviour
{
    public GameObject[] introCG;
    public GameObject teamSelect;
    public GameObject introTextOne;
    public GameObject introTextTwo;
    public GameObject introTextThree;
    public GameObject introTextThreeEN;
    public GameObject introCG2;
    public GameObject blackCard;
    public GameObject introTextFour;
    public DialogContent IntroDialog;
    public DialogContent OtherDialog;
    public GameObject[] panelAndHover;
    public GameObject cardSystem;
    public GameObject cardPool;
    public static bool isFinished = false;
    public bool firstFinish = false;
    public bool cardFinish = false;
    public bool poolFinish = false;
    public void Start()
    {
        AudioManager.Instance.StopBGM();
        AudioManager.Instance.PlayBGM(BackgroundMusic.IntroScene);
        introTextTwo.SetActive(false);
        introTextThree.SetActive(false);
        introTextThreeEN.SetActive(false);
        teamSelect.SetActive(false);
        introCG[0].SetActive(true);
        introCG2.SetActive(false);
        introTextFour.SetActive(false);
        blackCard.SetActive(false);
        cardSystem.SetActive(false);
        cardPool.SetActive(false);
        foreach (var obj in panelAndHover)
        {
            obj.SetActive(true);
        }
        DialogManager.Instance.isIntroFinished = false;
        DialogManager.Instance.isBEfinished = true;
        DialogManager.Instance.Init("Intro", IntroDialog);
    }
    // 给TextAnimator用的三个组件
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
    public void SetCardPoolFinished()
    {
        poolFinish = true;
    }
    public void Update()
    {
        if(DialogManager.Instance.isIntroFinished && firstFinish && cardFinish && poolFinish)
        {
            poolFinish = false;
            cardFinish = false;
            cardPool.SetActive(false);
            cardSystem.SetActive(false);
            //新系统蹦出来
        }
        // 选择结束进入黑卡和抽牌引导
        if(DialogManager.Instance.isIntroFinished && SaveManager.Instance.IsAngel != 0 && teamSelect.activeSelf && firstFinish)
        {
            AudioManager.Instance.PlaySFX(SoundEffect.Beep);
            teamSelect.SetActive(false);
            introCG2.SetActive(true);
            DialogManager.Instance.isIntroFinished = false;
            DialogManager.Instance.isBEfinished = true;
            DialogManager.Instance.Init("Intro", OtherDialog);
        }
        if(DialogManager.Instance.index == 2 && firstFinish)
        {
            AudioManager.Instance.PlaySFX(SoundEffect.CardPlace);
            blackCard.SetActive(true);
            cardFinish = true;
        }
        if(DialogManager.Instance.isIntroFinished && firstFinish && cardFinish)
        {
            introTextFour.SetActive(true);
            introTextTwo.SetActive(false);
            blackCard.SetActive(false);
            introCG2.SetActive(false);
            foreach (var obj in panelAndHover)
            {
                obj.SetActive(false);
            }
            cardSystem.SetActive(true);
            cardPool.SetActive(true);
        }
        if (!DialogManager.Instance.isIntroFinished && DialogManager.Instance.index == 3 && !firstFinish)//用dialogManager来传信号
        {
            introCG[0].SetActive(false);introCG[1].SetActive(true);
        }
        if (DialogManager.Instance.isIntroFinished && !firstFinish)
        {
            firstFinish = true;
            SaveManager.Instance.LoadLevel();
            // 已经选择过了又进入新手关卡
            if(SaveManager.Instance.IsPoolFinshed && introCG[1].activeSelf)
            {
                introTextOne.SetActive(false);introTextTwo.SetActive(true);
                if(LanguageManager.Instance.CurrentLanguage == LanguageOption.Chinese)
                {
                    introTextThree.SetActive(true);
                }else if(LanguageManager.Instance.CurrentLanguage == LanguageOption.English)
                {
                    introTextThreeEN.SetActive(true);
                }
                if (Input.GetMouseButtonDown(0))
                {
                    TransManager.Instance.ChangeScene("LevelOne");
                }
            }
            if(SaveManager.Instance.IsAngel == 0)
            {
                introCG[1].SetActive(false); introTextTwo.SetActive(true); introTextOne.SetActive(false); teamSelect.SetActive(true);
            }
        }
    }
}
