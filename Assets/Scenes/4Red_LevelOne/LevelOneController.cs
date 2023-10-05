using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelOneController : MonoBehaviour
{
    public List<Card> cards = new List<Card>();
    public RawImage[] skills;
    private List<string> description = new List<string>();
    public GameObject[] descriptionPanel;
    public GameObject[] buildingDspPanel;
    public static bool isFinished = false;
    public DialogContent IntroDialog;
    public GameObject IntroPanel;
    public TextMeshProUGUI moneyText;
    public GameObject textOne;
    public GameObject[] buttonPanel;
    private void Start()
    {
        textOne.SetActive(false);
        AudioManager.Instance.StopBGM();
        AudioManager.Instance.bgm[1].Stop();
        AudioManager.Instance.PlayBGM(BackgroundMusic.LevelOneScene);
        CardStorePref.Instance.GetCard();
        int id;int level;CardState state;
        foreach (var obj in CardStorePref.Instance.loadPlayerCards)
        {
            id = obj.Key; level = obj.Value; state = CardStorePref.Instance.stateCard[id];
            if(state == CardState.Deck)
            {
                cards.Add(CardStorePref.Instance.CardLibrary[id]);
            }
        }
        for (int i = 0; i < cards.Count; i++)
        {
            skills[i].texture = cards[i].tImage;
            description.Add(cards[i].cardEffect);
        }
        foreach (var obj in descriptionPanel)
        {
            obj.SetActive(false);
        }
        foreach (var obj in buildingDspPanel)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in buttonPanel)
        {
            obj.SetActive(false);
        }
        // 开启对话
        DialogManager.Instance.isIntroFinished = false;
        DialogManager.Instance.isBEfinished = true;
        IntroPanel.SetActive(true);
        DialogManager.Instance.Init("Intro", IntroDialog);
    }
    // 给TextAnimator用的
    public void PlayTypping()
    {
        AudioManager.Instance.PlaySFX(SoundEffect.Typing);
    }
    private void Update()
    {
        if (DialogManager.Instance.isIntroFinished || Input.GetMouseButtonDown(1))
        {
            if (IntroPanel.activeSelf)
            {
                foreach (GameObject obj in buttonPanel)
                {
                    obj.SetActive(true);
                }
                textOne.SetActive(true);
                IntroPanel.SetActive(false);
            }
        }
        if (GameObject.FindWithTag("Regulator"))
        {
            GameObject.FindWithTag("RAIN").SetActive(false);
        }
    }
    public void BuildingDescription(int id)
    {
        buildingDspPanel[0].SetActive(true);
        buildingDspPanel[1].GetComponent<TextMeshProUGUI>().text = CardStorePref.Instance.loadNameBuildings[id] + "：" + CardStorePref.Instance.loadDspBuildings[id];
        buildingDspPanel[1].SetActive(true);
    }
    public void DisableBuildings()
    {
        foreach (var obj in buildingDspPanel)
        {
            obj.SetActive(false);
        }
    }
    public void SkillDescription(int id)
    {
        descriptionPanel[0].SetActive(true);
        descriptionPanel[1].GetComponent<TextMeshProUGUI>().text = description[id];
        descriptionPanel[1].SetActive(true);
    }
    public void DisableDescrip()
    {
        foreach (var obj in descriptionPanel)
        {
            obj.SetActive(false);
        }
    }
}
