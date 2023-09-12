using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Febucci.UI;
using UnityEngine.SceneManagement;

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
    private bool wait;
    public bool isTyping;
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
                if (index <= DContent.CNdialogList.Count && IntroController.isFinished)
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
        if (index < dialogs.Count)
        {
            textAnimatorPlayer.ShowText(dialogs[index]);
            index++;
        }
        else if (index >= dialogs.Count && !isIntroFinished)
        {
            isIntroFinished = true;
        }
        else if (index >= dialogs.Count - 2 && !isBEfinished)
        {
            isBEfinished = true;
        }

        yield return null;
    }
}
