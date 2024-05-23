using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Febucci.UI;

public class DialogManager : SingletonMono<DialogManager>
{
    private TextMeshProUGUI text;
    private TextAnimatorPlayer textAnimatorPlayer;
    private List<string> dialogs;
    private bool canSkip = true;
    private int index = 0;
    private void Start()
    {
        EventManager.Instance.AddEventListener("OnMouseDown", HandleSkip);
    }
    private void HandleSkip()
    {
        if (!canSkip)
        {
            StartCoroutine(StartDialog(dialogs));
        }
        if (text != null && canSkip)
        {
            textAnimatorPlayer.textAnimator.ShowAllCharacters(canSkip);
        }
        canSkip = !canSkip;
    }

    public void BeginDialog(GameObject textObject, DialogContent content, int num)
    {
        text = textObject.GetComponent<TextMeshProUGUI>();
        textAnimatorPlayer = textObject.GetComponent<TextAnimatorPlayer>();
        index = num;
        if (LanguageManager.Instance.currentLanguage == LanguageOption.Chinese)
        {
            dialogs = content.CNdialogList;
            textAnimatorPlayer.waitForNormalChars = 0.07f;
            StartCoroutine(StartDialog(dialogs));
        }
        else
        {
            dialogs = content.ENdialogList;
            textAnimatorPlayer.waitForNormalChars = 0.04f;
            StartCoroutine(StartDialog(dialogs));
        }
    }
    public IEnumerator StartDialog(List<string> dialogs)
    {  
        if (index < dialogs.Count)
        {
            textAnimatorPlayer.ShowText(dialogs[index]);
            EventManager.Instance.EventTrigger<int>("dialogIndex", index++);
        }
        else
        {
            EventManager.Instance.EventTrigger("dialogFinished");
        }
        yield return null;
    }
}
