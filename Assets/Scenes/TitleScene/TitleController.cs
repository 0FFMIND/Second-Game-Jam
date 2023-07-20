using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TitleController : MonoBehaviour
{
    public GameObject mainBackground;
    public GameObject subBackground;
    public GameObject cnTMP;
    public GameObject egTMP;
    public LanguageOption preOp;
    public LanguageOption nowOp;
    //MenuSystem
    public MainMenu mainMenu;
    public ContinueMenu continueMenu;
    public DeveloperMenu developerMenu;
    public SettingMenu settingMenu;
    private Stack<TitleMenuBase> menuStack = new Stack<TitleMenuBase>();
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        mainBackground.SetActive(true);
        subBackground.SetActive(false);
        mainMenu.Open();
        SaveManager.Instance.Init();
        AudioManager.Instance.StopBGM();
        settingMenu.selectButton.SetButtons();
        AudioManager.Instance.PlayBGM(BackgroundMusic.TitleScene);
        LanguageManager.Instance.setChange = true;
        CursorManager.Instance.ChangeSprite(FingerOption.five);
        if (LanguageManager.Instance.nowOption == LanguageOption.Chinese)
        {
            cnTMP.SetActive(true); egTMP.SetActive(false);
        }
        else if (LanguageManager.Instance.nowOption == LanguageOption.English)
        {
            egTMP.SetActive(true); cnTMP.SetActive(false);
        }
        preOp = LanguageManager.Instance.nowOption;

    }
    private void Update()
    {
        if(preOp != LanguageManager.Instance.nowOption)
        {
            if (LanguageManager.Instance.nowOption == LanguageOption.Chinese)
            {
                cnTMP.SetActive(true); egTMP.SetActive(false);
            }
            else if (LanguageManager.Instance.nowOption == LanguageOption.English)
            {
                egTMP.SetActive(true); cnTMP.SetActive(false);
            }
            preOp = LanguageManager.Instance.nowOption;
        }

    }
    public void OpenMenu(TitleMenuBase menu)
    {
        //Deactivate top menu
        if (menuStack.Count > 0)
        {
            foreach (var singlemenu in menuStack)
            {
                singlemenu.gameObject.SetActive(false);
            }
        }
        menuStack.Push(menu);
    }
    public void CloseMenu()
    {
        var instance = menuStack.Pop();
        instance.gameObject.SetActive(false);
        //Reactivate top menu
        var topmenu = menuStack.Peek();
        topmenu.gameObject.SetActive(true);
    }
}
