using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelOneController : MonoBehaviour
{
    //public GameObject levelOneText;
    //public GameObject Nodes;
    //public GameObject CNTMP, EGTMP;
    //public LevelOneManager levelOneManager;
    //public DataController dataController;
    //public DialogContent LevelOneCN, LevelOneEg;
    //public DialogContent LevelOneUPCN, LevelOneUPEG;
    //public DialogContent LevelOneUPUPCN, LevelOneUPUPEG;
    //public GameObject IntroTouch;
    //public GameObject IntroTouchTwo;
    //public GameObject go;
    //public GameObject goTWO;
    //public GameObject goThree;
    //public bool onlyOnce = false;
    //public bool onlyOnceTW = false;
    //public bool onlyOnceTH = false;
    //public bool oonlyOnce = false;
    //public bool oooolyOnce = false;
    //public void Start()
    //{
    //    //先把关键参数设置default形态
    //    oonlyOnce = false;
    //    onlyOnce = false;
    //    onlyOnceTH = false;
    //    onlyOnceTW = false;
    //    oooolyOnce = false;
    //    Nodes.SetActive(false);
    //    CursorManager.Instance.ResetDuality();
    //    dataController.SetInital();
    //    IntroTouchTwo.SetActive(false);
    //    IntroTouch.SetActive(false);
    //    DialogManager.Instance.isBEfinished = false;
    //    AudioManager.Instance.StopBGM();
    //    CursorManager.Instance.ChangeSprite(FingerOption.five);
    //    AudioManager.Instance.PlayBGM(BackgroundMusic.LevelOneScene);
    //    if (LanguageManager.Instance.nowOption == LanguageOption.English)
    //    {
    //        EGTMP.SetActive(true); CNTMP.SetActive(false);
    //        DialogManager.Instance.Init("levelOne", LevelOneEg);
    //    }
    //    else if (LanguageManager.Instance.nowOption == LanguageOption.Chinese)
    //    {
    //        EGTMP.SetActive(false); CNTMP.SetActive(true);
    //        DialogManager.Instance.Init("levelOne", LevelOneCN);
    //    }
    //}
    //public void Update()
    //{
    //    if (DialogManager.Instance.isBEfinished == true && !onlyOnce)
    //    {
    //        if (!IntroTouch.activeInHierarchy)
    //        {
    //            IntroTouch.SetActive(true);
    //            onlyOnce = true;
    //            DialogManager.Instance.isBEfinished = false;
    //            TextMeshProUGUI GO = DialogManager.Instance.textObject.GetComponentInChildren<TextMeshProUGUI>();
    //            GameObject go = GO.gameObject;
    //            go.SetActive(false);
    //        }
    //    }
    //    if (!IntroTouch.activeInHierarchy && !onlyOnceTW && onlyOnce)
    //    {
    //        //下一阶段的对话
    //        goTWO.SetActive(true);
    //        if (!oonlyOnce)
    //        {
    //            if (LanguageManager.Instance.nowOption == LanguageOption.English)
    //            {
    //                DialogManager.Instance.isBEfinished = false;
    //                EGTMP.SetActive(true); CNTMP.SetActive(false);
    //                DialogManager.Instance.Init("levelOneTW", LevelOneUPEG, goTWO);
    //            }
    //            else if (LanguageManager.Instance.nowOption == LanguageOption.Chinese)
    //            {
    //                DialogManager.Instance.isBEfinished = false;
    //                EGTMP.SetActive(false); CNTMP.SetActive(true);
    //                DialogManager.Instance.Init("levelOneTW", LevelOneUPCN, goTWO);
    //            }
    //            oonlyOnce = true;
    //        }
    //        if (DialogManager.Instance.isBEfinished)
    //        {
    //            goTWO.SetActive(false);
    //            IntroTouchTwo.SetActive(true);
    //            onlyOnceTW = true;
    //            DialogManager.Instance.isBEfinished = false;
    //        }
    //    }
    //    if (!IntroTouchTwo.activeInHierarchy && onlyOnce && onlyOnceTW)
    //    {
    //        Nodes.SetActive(true);
    //        goThree.SetActive(true);
    //        if (!oooolyOnce)
    //        {
    //            if (LanguageManager.Instance.nowOption == LanguageOption.English)
    //            {
    //                DialogManager.Instance.isBEfinished = false;
    //                EGTMP.SetActive(true); CNTMP.SetActive(false);
    //                DialogManager.Instance.Init("levelOneTH", LevelOneUPUPEG, goThree);
    //            }
    //            else if (LanguageManager.Instance.nowOption == LanguageOption.Chinese)
    //            {
    //                DialogManager.Instance.isBEfinished = false;
    //                EGTMP.SetActive(false); CNTMP.SetActive(true);
    //                DialogManager.Instance.Init("levelOneTH", LevelOneUPUPCN, goThree);
    //            }
    //            oooolyOnce = true;
    //        }
    //        if (DialogManager.Instance.isBEfinished && goThree.activeInHierarchy)
    //        {
    //            goThree.SetActive(false);
    //            levelOneManager.WaveStart();
    //        }
    //     }
    //}
}
