
using UnityEngine;

public class IntroController : MonoBehaviour
{
    //public GameObject introSave;
    //public GameObject introText;
    //bool isSaveEnd = false;
    //bool isInit = false;
    //public DialogContent BeIntroDialogEn,
    //                     BeIntroDialogCn,
    //                     IntroDialogEn,
    //                     IntroDialogCn;
    //public void Start()
    //{
    //    CursorManager.Instance.ChangeSprite(FingerOption.five);
    //    AudioManager.Instance.StopBGM();
    //    AudioManager.Instance.PlayBGM(BackgroundMusic.IntroScene);
    //    DialogManager.Instance.isIntroFinished = false;
    //    DialogManager.Instance.firstdialogFinished = false;
    //    isInit = false;
    //    isSaveEnd = false;
    //    //弹出一个显示菜单，显示Intro界面已经储存到continue里面了
    //    if(LanguageManager.Instance.nowOption == LanguageOption.English)
    //    {
    //        DialogManager.Instance.Init("beforeIntro", BeIntroDialogEn);
    //    }
    //    else if(LanguageManager.Instance.nowOption == LanguageOption.Chinese)
    //    {
    //        DialogManager.Instance.Init("beforeIntro", BeIntroDialogCn);
    //    }
        
    //    introSave.SetActive(true);
    //}
    //public void PlayTypping()
    //{
    //    AudioManager.Instance.PlaySFX(SoundEffect.Typing);
    //}
    //public void Update()
    //{
    //    if (!isInit)
    //    {
    //        if (DialogManager.Instance.firstdialogFinished)//用dialogManager来传信号
    //        {
    //            if (!isSaveEnd)
    //            {
    //                isSaveEnd = true;
    //            }
    //        }
    //        if (isSaveEnd)
    //        {
    //            introText.SetActive(true);
    //            introSave.SetActive(false);
    //            if (LanguageManager.Instance.nowOption == LanguageOption.English)
    //            {
    //                DialogManager.Instance.Init("Intro", IntroDialogEn);
    //            }
    //            else if (LanguageManager.Instance.nowOption == LanguageOption.Chinese)
    //            {
    //                DialogManager.Instance.Init("Intro", IntroDialogCn);
    //            }
    //            isSaveEnd = false;
    //            isInit = true;
    //        }
    //    }
    //    if (DialogManager.Instance.isIntroFinished)
    //    {
    //        TransManager.Instance.ChangeScene("LevelOne");
    //    }

    //}
}
