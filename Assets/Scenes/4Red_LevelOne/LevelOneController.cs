using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelOneController : MonoBehaviour
{
    public static bool isCard11;
    public static bool isCard5;
    public List<Card> cards = new List<Card>();
    public RawImage[] skills;
    private List<string> description = new List<string>();
    public GameObject[] descriptionPanel;
    public GameObject[] buildingDspPanel;
    public static bool isFinished = false;
    public static bool onlyOnce;
    public DialogContent IntroDialog;
    public GameObject IntroPanel;
    public TextMeshProUGUI moneyText;
    public GameObject textOne;
    public GameObject[] buttonPanel;
    public GameObject rainPrefab;
    public GameObject pollutedWater;
    public GameObject BuildingManager;
    public GameObject Ghost;
    public GameObject[] Mouse;
    public Collider2D canPlace;
    public bool isStarOne;
    public bool isStarTwo;
    public bool isStarThree;
    float timer = 0;
    public GameObject successpanel;
    private void Start()
    {
        canPlace.offset = new Vector2(canPlace.offset.x, -10f);
        successpanel.SetActive(false);
        isStarOne = false; isStarTwo = false; isStarThree = false;
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
            skills[i].gameObject.GetComponentInParent<ReadId>().id = cards[i].id;
            if(cards[i].id >= 7)
            {
                ColorBlock colorBlock = new ColorBlock();
                colorBlock.normalColor = Color.yellow; colorBlock.pressedColor = Color.red; colorBlock.highlightedColor = Color.yellow; colorBlock.disabledColor = Color.green; colorBlock.colorMultiplier = 1;
                colorBlock.selectedColor = Color.yellow;
                colorBlock.fadeDuration = 0.1f; skills[i].gameObject.GetComponentInParent<Button>().colors = colorBlock;
            }
            if(cards[i].id == 5 || cards[i].id == 11)
            {
                ColorBlock colorBlock = new ColorBlock();
                colorBlock.normalColor = Color.red; colorBlock.pressedColor = Color.red; colorBlock.highlightedColor = Color.red; colorBlock.disabledColor = Color.red; colorBlock.colorMultiplier = 1;
                colorBlock.selectedColor = Color.red;
                colorBlock.fadeDuration = 0.1f;
                skills[i].gameObject.GetComponentInParent<Button>().colors = colorBlock;
            }
            if(cards[i].id == 11)
            {
                isCard11 = true;
            }
            if(cards[i].id == 5)
            {
                isCard5 = true;
            }
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
        onlyOnce = true;
        Ghost.SetActive(false);
        BuildingManager.SetActive(false);
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
        timer += Time.deltaTime;
        if(timer > 30f && timer < 30.01f)
        {
            AudioManager.Instance.PlaySFX(SoundEffect.Beep);
        }
        if(timer > 30f && Mouse[0] != null)
        {
            Mouse[0].SetActive(true);
        }
        if(timer > 43f && Mouse[1] != null && !isCard5)
        {
            Mouse[1].SetActive(true);
        }
        if(timer > 43f && Mouse[1] != null && isCard5)
        {
            Mouse[1].SetActive(true);
            Destroy(Mouse[1]);
        }
        if(timer > 45f && Mouse[2] != null && !isCard5)
        {
            Mouse[2].SetActive(true);
        }
        if (timer > 45f && Mouse[2] != null && isCard5)
        {
            Mouse[2].SetActive(true);
            Destroy(Mouse[2]);
        }
        if (DialogManager.Instance.isIntroFinished || Input.GetMouseButtonDown(1))
        {
            BuildingManager.SetActive(true);
            Ghost.SetActive(true);
            onlyOnce = false;
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
        if(isStarOne && isStarTwo && isStarThree && !SaveManager.Instance.IsLevelOneEnd)
        {
            SaveManager.Instance.IsLevelOneEnd = true;
            SaveManager.Instance.LevelOneStar = 3;
            PlayerPrefs.SetInt("Stars", 3);
            SaveManager.Instance.SaveLevel();
            SaveManager.Instance.LoadLevel();
            successpanel.SetActive(true);
        }
        if (isStarOne && isStarTwo && isStarThree && SaveManager.Instance.IsLevelOneEnd)
        {
            successpanel.SetActive(true);

        }
        if (GameObject.FindWithTag("Regulator"))
        {
            AudioManager.Instance.PlaySFX(SoundEffect.UISelect);
            rainPrefab.SetActive(false);
            isStarOne = true;
        }
        if (ResourceManager.Instance.isWaterClear)
        {
            AudioManager.Instance.PlaySFX(SoundEffect.UISelect);
            pollutedWater.SetActive(false);
            isStarTwo = true;
            canPlace.offset = new Vector2(canPlace.offset.x, -0.597f);
        }
        for (int i = 0; i < Mouse.Length; i++)
        {
            if(Mouse[i] != null)
            {
                isStarThree = false;
                break;
            }
            else
            {
                isStarThree = true;
            }
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
