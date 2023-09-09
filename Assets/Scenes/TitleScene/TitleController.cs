using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TitleController : MonoBehaviour
{
    public GameObject mainBackground;
    public GameObject subBackground;
    public List<GameObject> CN = new List<GameObject>();
    public List<GameObject> EN = new List<GameObject>();
    public bool hasLangChangeInTitle = true;
    public SelectButton selectButton;
    
    public MainMenu mainMenu;
    public ContinueMenu continueMenu;
    public DeveloperMenu developerMenu;
    public SettingMenu settingMenu;

    private Stack<TitleMenuBase> menuStack = new Stack<TitleMenuBase>();
    private void Start()
    {
        mainBackground.SetActive(true);
        subBackground.SetActive(false);
        mainMenu.Open();
        SaveManager.Instance.Init();
        AudioManager.Instance.StopBGM();
        AudioManager.Instance.PlayBGM(BackgroundMusic.TitleScene);
        selectButton.SetButtons();
    }
    private void Update()
    {
        if (hasLangChangeInTitle)
        {
            if(LanguageManager.Instance.CurrentLanguage == LanguageOption.Chinese)
            {
                AbleCNLanguage(true);
            }
            else if(LanguageManager.Instance.CurrentLanguage == LanguageOption.English)
            {
                AbleCNLanguage(false);
            }
            hasLangChangeInTitle = false;
        }
    }
    private void AbleCNLanguage(bool okCN)
    {
        foreach (var obj in EN)
        {
            obj.SetActive(!okCN);
        }
        foreach (var obj in CN)
        {
            obj.SetActive(okCN);
        }
    }
    public void OpenMenu(TitleMenuBase menu)
    {
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

        var topmenu = menuStack.Peek();
        topmenu.gameObject.SetActive(true);
    }
}
