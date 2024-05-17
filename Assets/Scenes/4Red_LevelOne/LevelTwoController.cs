using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelTwoController : MonoBehaviour
{
    public static LevelTwoController Instance;
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
    public GameObject[] pollutedWater;
    public GameObject BuildingManager;
    public GameObject Ghost;
    public GameObject[] Mouse;
    public Collider2D FirstWater;
    public Collider2D SecondWater;
    public bool isStarOne;
    public bool isStarTwo;
    public bool isStarThree;
    public static bool isWaterClear;
    public bool isWaterOneClear;
    public bool isWaterTwoClear;
    float timer = 0;
    public GameObject successpanel;
    private void Start()
    {
        FirstWater.offset = new Vector2(FirstWater.offset.x, -10f);
        SecondWater.offset = new Vector2(SecondWater.offset.x, -10f);
        successpanel.SetActive(false);
        isStarOne = false; isStarTwo = false; isStarThree = false;
        textOne.SetActive(false);
        AudioManager.Instance.StopBGM();
        AudioManager.Instance.PlayBGM(BackgroundMusic.LevelOneScene);
        AudioManager.Instance.StopBGM();
        AudioManager.Instance.PlayBGM(BackgroundMusic.LevelOneScene);
        CardStorePref.Instance.GetCard();
        int id; int level; CardState state;
        foreach (var obj in CardStorePref.Instance.loadPlayerCards)
        {
            id = obj.Key; level = obj.Value; state = CardStorePref.Instance.stateCard[id];
            if (state == CardState.Deck)
            {
                cards.Add(CardStorePref.Instance.CardLibrary[id]);
            }
        }
        for (int i = 0; i < cards.Count; i++)
        {
            skills[i].texture = cards[i].tImage;
            skills[i].gameObject.GetComponentInParent<ReadId>().id = cards[i].id;
            if (cards[i].id >= 7)
            {
                ColorBlock colorBlock = new ColorBlock();
                colorBlock.normalColor = Color.yellow; colorBlock.pressedColor = Color.red; colorBlock.highlightedColor = Color.yellow; colorBlock.disabledColor = Color.green; colorBlock.colorMultiplier = 1;
                colorBlock.selectedColor = Color.yellow;
                colorBlock.fadeDuration = 0.1f; skills[i].gameObject.GetComponentInParent<Button>().colors = colorBlock;
            }
            if (cards[i].id == 5 || cards[i].id == 11)
            {
                ColorBlock colorBlock = new ColorBlock();
                colorBlock.normalColor = Color.red; colorBlock.pressedColor = Color.red; colorBlock.highlightedColor = Color.red; colorBlock.disabledColor = Color.red; colorBlock.colorMultiplier = 1;
                colorBlock.selectedColor = Color.red;
                colorBlock.fadeDuration = 0.1f;
                skills[i].gameObject.GetComponentInParent<Button>().colors = colorBlock;
            }
            if (cards[i].id == 11)
            {
                isCard11 = true;
            }
            if (cards[i].id == 5)
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
    public void SetFinishedFalse()
    {
        isFinished = false;
    }
    public void SetFinished()
    {
        isFinished = true;
    }
    private void Update()
    {
        Vector3 vector3 = InputManager.Instance.GetPos();
        Vector2 vector2 = new Vector2(vector3.x, vector3.y);
        for (int i = 0; i < pollutedWater.Length; i++)
        {
            if(pollutedWater.Length == 2 || pollutedWater.Length == 3)
            {
                float dis = Vector2.Distance(vector2, pollutedWater[i].transform.position);
                if (dis < 0.5f)
                {
                    pollutedWater[i].SetActive(false);
                    if (i == 0)
                    {
                        FirstWater.offset = new Vector2(FirstWater.offset.x, 2.198f);
                        isWaterOneClear = true;
                    }
                    else if (i == 1)
                    {
                        SecondWater.offset = new Vector2(SecondWater.offset.x, -.85f);
                        isWaterOneClear = false;
                    }
                }
            }
            else if(pollutedWater.Length == 1)
            {
                float dis = Vector2.Distance(vector2, pollutedWater[i].transform.position);
                if (dis < 0.5f)
                {
                    pollutedWater[i].SetActive(false);
                    if (!isWaterOneClear)
                    {
                        FirstWater.offset = new Vector2(FirstWater.offset.x, 2.198f);
                        isWaterOneClear = true;
                    }
                    else if (isWaterOneClear)
                    {
                        SecondWater.offset = new Vector2(SecondWater.offset.x, -0.85f);
                        isWaterOneClear = false;
                    }
                }
            }


        }
        if(GameObject.FindWithTag("water") == null && GameObject.FindWithTag("water2") == null)
        {
            isStarTwo = true;
        }
        if(IntroPanel.activeSelf == false)
        {
            timer += Time.deltaTime;
        }
        if (timer > 30f && timer <30.01f)
        {
            AudioManager.Instance.PlaySFX(SoundEffect.Beep);
        }
        if (timer > 30f && Mouse[0] != null)
        {
            Mouse[0].SetActive(true);
        }
        if (timer > 40f && Mouse[3] != null)
        {
            Mouse[3].SetActive(true);
        }
        if (timer > 43f && Mouse[8] != null)
        {
            Mouse[8].SetActive(true);
        }
        if (timer > 45f && Mouse[2] != null)
        {
            Mouse[2].SetActive(true);
            if(isCard5)
            Destroy(Mouse[2]);
        }
        if (timer > 48f && Mouse[6] != null)
        {
            Mouse[6].SetActive(true);
        }
        if (timer > 49f && Mouse[4] != null)
        {
            Mouse[4].SetActive(true);
            if(isCard5)
            Destroy(Mouse[4]);
        }
        if (timer > 53f && Mouse[1] != null)
        {
            Mouse[1].SetActive(true);
        }
        if (timer > 55f && Mouse[7] != null)
        {
            Mouse[7].SetActive(true);
            if(isCard5)
            Destroy(Mouse[7]);
        }
        if (timer > 59f && Mouse[5] != null)
        {
            Mouse[5].SetActive(true);
        }
        if (timer > 61f && Mouse[10] != null)
        {
            Mouse[10].SetActive(true);
        }
        if (timer > 62f && Mouse[9] != null)
        {
            Mouse[9].SetActive(true);
        }
        if (timer > 64f && Mouse[13] != null)
        {
            Mouse[13].SetActive(true);
        }
        if (timer > 65f && Mouse[12] != null)
        {
            Mouse[12].SetActive(true);
        }
        if (timer > 66f && Mouse[11] != null)
        {
            Mouse[11].SetActive(true);
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
        if (isStarOne && isStarTwo && isStarThree && !SaveManager.Instance.IsLevelTwoEnd)
        {
            SaveManager.Instance.IsLevelTwoEnd = true;
            int money = PlayerPrefs.GetInt("Stars");
            PlayerPrefs.SetInt("Stars", 3 + money);
            SaveManager.Instance.SaveLevel();
            SaveManager.Instance.LoadLevel();
            successpanel.SetActive(true);
        }
        if (isStarOne && isStarTwo && isStarThree && SaveManager.Instance.IsLevelTwoEnd)
        {
            successpanel.SetActive(true);
        }
        if (GameObject.FindGameObjectsWithTag("Regulator") != null)
        {
            if(GameObject.FindGameObjectsWithTag("Regulator").Length >= 2)
            {
                rainPrefab.SetActive(false);
                isStarOne = true;
            }
            else
            {
                isStarOne = false;
            }

        }

        for (int i = 0; i < Mouse.Length; i++)
        {
            if (Mouse[i] != null)
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
