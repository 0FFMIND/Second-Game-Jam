using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Febucci.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class DialogManager : Singleton<DialogManager>
{
    public GameObject textObject;
    public TextMeshProUGUI text;
    public TextAnimator textAnimator;
    public TextAnimatorPlayer textAnimatorPlayer;
    public DialogContent DContent;
    public float textSpeed = 0.03f;
    public bool isBEfinished = false;
    public bool isIntroFinished = false;
    public int index;
    public bool isTyping;
    private bool onlyOnce = false;
    public void UnInit()
    {
        textObject = GameObject.FindWithTag(name == "beforeIntro" ? "BETEXT" : "TEXT");
        text = textObject.GetComponent<TextMeshProUGUI>();
        textAnimatorPlayer = textObject.GetComponent<TextAnimatorPlayer>();
        textAnimator = textObject.GetComponent<TextAnimator>();
    }
    public void Init(string name, DialogContent dialogContent)
    {
        textObject = GameObject.FindWithTag(name == "beforeIntro" ? "BETEXT" : "TEXT");
        DContent = dialogContent;
        BeginDialog(DContent);
    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().name != "TitleScene")
        {
            if (Input.GetMouseButtonDown(0))
            {
                DialogControl();
            }
            else if(Input.touches.Length != 0 && Input.touches[0].phase == TouchPhase.Began)
            {
                DialogControl();
            }
        }
    }

    private void DialogControl()
    {
        if (SceneManager.GetActiveScene().name == "BELevelOne")
        {
            if (index <= DContent.CNdialogList.Count && BELevelText.isFinished)
            {
                BELevelText.isFinished = false;
                StartCoroutine(StartDialog(DContent.CNdialogList, DContent.ENdialogList));
            }
            else if (!BELevelText.isFinished)
            {
                textAnimatorPlayer.SkipTypewriter();
                BELevelText.isFinished = true;
            }
        }
        if (SceneManager.GetActiveScene().name == "IntroScene" || SceneManager.GetActiveScene().name == "EndScene")
        {
            if (index == 2 && !IntroController.isInit && IntroController.isFinished)
            {
                isIntroFinished = true;
            }
            if (index == 5 && IntroController.isFinished && SceneManager.GetActiveScene().name == "IntroScene")
            {
                isBEfinished = true;
            }
            else if (index == 3 && IntroController.isFinished && SceneManager.GetActiveScene().name == "EndScene")
            {
                isBEfinished = true;
            }
            else if (IntroController.isFinished)
            {
                IntroController.isFinished = false;
                StartCoroutine(StartDialog(DContent.CNdialogList, DContent.ENdialogList));
            }
            else if (!IntroController.isFinished)
            {
                textAnimatorPlayer.SkipTypewriter();
                IntroController.isFinished = true;
            }
        }
        if (SceneManager.GetActiveScene().name == "OpenScene")
        {
            if (!SaveManager.Instance.IsOpenEnd && index <= DContent.CNdialogList.Count && LevelNewIntro.isFinished)
            {
                LevelNewIntro.isFinished = false;
                StartCoroutine(StartDialog(DContent.CNdialogList, DContent.ENdialogList));
            }
            else if (!LevelNewIntro.isFinished && !SaveManager.Instance.IsOpenEnd)
            {
                textAnimatorPlayer.SkipTypewriter();
                LevelNewIntro.isFinished = true;
            }
            if (SaveManager.Instance.IsOpenEnd)
            {
                return;
            }
        }
        if (SceneManager.GetActiveScene().name == "LevelOne")
        {
            if (index <= DContent.CNdialogList.Count && LevelOneController.isFinished && LevelOneController.onlyOnce)
            {
                LevelOneController.isFinished = false;
                StartCoroutine(StartDialog(DContent.CNdialogList, DContent.ENdialogList));
            }
            else if (!LevelOneController.isFinished && LevelOneController.onlyOnce)
            {
                textAnimatorPlayer.SkipTypewriter();
                LevelOneController.isFinished = true;
            }
        }
        if (SceneManager.GetActiveScene().name == "LevelTwo")
        {
            if (index <= DContent.CNdialogList.Count && LevelTwoController.isFinished && LevelTwoController.onlyOnce)
            {
                LevelTwoController.isFinished = false;
                StartCoroutine(StartDialog(DContent.CNdialogList, DContent.ENdialogList));
            }
            else if (!LevelTwoController.isFinished && LevelTwoController.onlyOnce)
            {
                textAnimatorPlayer.SkipTypewriter();
                LevelTwoController.isFinished = true;
            }
        }
        if (SceneManager.GetActiveScene().name == "LevelThree")
        {
            if (index <= DContent.CNdialogList.Count && LevelThreeController.isFinished && LevelThreeController.onlyOnce)
            {
                LevelThreeController.isFinished = false;
                StartCoroutine(StartDialog(DContent.CNdialogList, DContent.ENdialogList));
            }
            else if (!LevelThreeController.isFinished && LevelThreeController.onlyOnce)
            {
                textAnimatorPlayer.SkipTypewriter();
                LevelThreeController.isFinished = true;
            }
        }
        if (GameObject.Find("BackgroundCanvas").gameObject.GetComponent<LevelFourController>() != null)
        {
            if (index <= DContent.CNdialogList.Count && LevelFourController.isFinished && LevelFourController.onlyOnce)
            {
                LevelFourController.isFinished = false;
                StartCoroutine(StartDialog(DContent.CNdialogList, DContent.ENdialogList));
            }
            else if (!LevelFourController.isFinished && LevelFourController.onlyOnce)
            {
                textAnimatorPlayer.SkipTypewriter();
                LevelFourController.isFinished = true;
            }
        }
        if (GameObject.Find("BackgroundCanvas").gameObject.GetComponent<LevelFiveController>() != null)
        {
            if (index <= DContent.CNdialogList.Count && LevelFiveController.isFinished && LevelFiveController.onlyOnce)
            {
                LevelFiveController.isFinished = false;
                StartCoroutine(StartDialog(DContent.CNdialogList, DContent.ENdialogList));
            }
            else if (!LevelFiveController.isFinished && LevelFiveController.onlyOnce)
            {
                textAnimatorPlayer.SkipTypewriter();
                LevelFiveController.isFinished = true;
            }
        }
        if (GameObject.Find("BackgroundCanvas").gameObject.GetComponent<LevelSixController>() != null)
        {
            if (index <= DContent.CNdialogList.Count && LevelSixController.isFinished && LevelSixController.onlyOnce)
            {
                LevelSixController.isFinished = false;
                StartCoroutine(StartDialog(DContent.CNdialogList, DContent.ENdialogList));
            }
            else if (!LevelSixController.isFinished && LevelSixController.onlyOnce)
            {
                textAnimatorPlayer.SkipTypewriter();
                LevelSixController.isFinished = true;
            }
        }
    }

    public void BeginDialog(DialogContent content)
    {
        text = textObject.GetComponent<TextMeshProUGUI>();
        textAnimatorPlayer = textObject.GetComponent<TextAnimatorPlayer>();
        textAnimator = textObject.GetComponent<TextAnimator>();
        index = 0;
        StartCoroutine(StartDialog(content.CNdialogList,content.ENdialogList));
    }
    public IEnumerator StartDialog(List<string> cnDialogs,List<string> enDialogs)
    {
        List<string> dialogs;
        LanguageOption option = LanguageManager.Instance.CurrentLanguage;
        if(option == LanguageOption.Chinese)
        {
            dialogs = cnDialogs;
        }else
        {
            textAnimatorPlayer.waitForNormalChars = 0.04f;
            dialogs = enDialogs;
        }
        if (SceneManager.GetActiveScene().name == "IntroScene" && isIntroFinished)
        {
            textAnimatorPlayer.ShowText(dialogs[index]);
            index++;
        }
        else if (SceneManager.GetActiveScene().name == "IntroScene" && !IntroController.isInit)
        {
            if (index <= 2 && !isIntroFinished)
            {
                textAnimatorPlayer.ShowText(dialogs[index]);
                index++;
            }
        }
        else if (index < dialogs.Count)
        {
            textAnimatorPlayer.ShowText(dialogs[index]);
            index++;
        }
        else if (index >= dialogs.Count && !isIntroFinished)
        {
            isIntroFinished = true;
        }

        yield return null;
    }
}
