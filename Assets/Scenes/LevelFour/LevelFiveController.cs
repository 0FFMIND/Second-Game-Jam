using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelFiveController : MonoBehaviour
{
    public static LevelFiveController Instance;
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
    public Collider2D[] Water;
    public bool isStarOne;
    public bool isStarTwo;
    public bool isStarThree;
    public static bool isWaterClear;
    float timer = 0;
    public GameObject successpanel;
    public GameObject failPanel;
    private void Start()
    {
        foreach (var collider in Water)
        {
            collider.offset = new Vector2(collider.offset.x, collider.offset.y - 10f);
        }
        successpanel.SetActive(false);
        isStarOne = false; isStarTwo = false; isStarThree = false;
        textOne.SetActive(false);
        AudioManager.Instance.StopBGM();
        AudioManager.Instance.PlayBGM(BackgroundMusic.LevelTwoScene);
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
        //DialogManager.Instance.isIntroFinished = false;
        //DialogManager.Instance.isBEfinished = true;
        //IntroPanel.SetActive(true);
        //DialogManager.Instance.Init("Intro", IntroDialog);
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
        //Vector3 vector3 = InputManager.Instance.GetPos();
        //Vector2 vector2 = new Vector2(vector3.x, vector3.y);
        //for (int i = 0; i < pollutedWater.Length; i++)
        //{
        //    float dis = Vector2.Distance(vector2, pollutedWater[i].transform.position);
        //    if (dis < 1f)
        //    {
        //        pollutedWater[i].SetActive(false);
        //        Water[i].offset = new Vector2(Water[i].offset.x, Water[i].offset.y + 10f);
        //    }
        //}
        if (GameObject.FindWithTag("water") == null)
        {
            isStarTwo = true;
        }
        if (IntroPanel.activeSelf == false)
        {
            timer += Time.deltaTime;
        }
        if (timer > 30f && timer < 30.01f)
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
            if (isCard5)
                Destroy(Mouse[2]);
        }
        if (timer > 48f && Mouse[6] != null)
        {
            Mouse[6].SetActive(true);
        }
        if (timer > 49f && Mouse[4] != null)
        {
            Mouse[4].SetActive(true);
            if (isCard5)
                Destroy(Mouse[4]);
        }
        if (timer > 53f && Mouse[1] != null)
        {
            Mouse[1].SetActive(true);
        }
        if (timer > 55f && Mouse[7] != null)
        {
            Mouse[7].SetActive(true);
            if (isCard5)
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
        if (timer > 66f && Mouse[11] != null)
        {
            Mouse[11].SetActive(true);
            if (isCard5)
                Destroy(Mouse[11]);
        }
        if (timer > 67f && Mouse[12] != null)
        {
            Mouse[12].SetActive(true);
        }
        if (timer > 70f && Mouse[13] != null)
        {
            Mouse[13].SetActive(true);
            if (isCard5)
                Destroy(Mouse[13]);
        }
        if (timer > 66f && Mouse[14] != null)
        {
            Mouse[14].SetActive(true);
        }
        if (timer > 72f && Mouse[15] != null)
        {
            Mouse[15].SetActive(true);
        }
        if (timer > 75f && Mouse[16] != null)
        {
            Mouse[16].SetActive(true);
        }
        if (timer > 76f && Mouse[16] != null)
        {
            Mouse[16].SetActive(true);
        }
        if (timer > 80f && Mouse[17] != null)
        {
            Mouse[17].SetActive(true);
        }
        if (timer > 81f && Mouse[18] != null)
        {
            Mouse[18].SetActive(true);
        }
        //if (DialogManager.Instance.isIntroFinished || Input.GetMouseButtonDown(1))
        //{
        //    BuildingManager.SetActive(true);
        //    Ghost.SetActive(true);
        //    onlyOnce = false;
        //    if (IntroPanel.activeSelf)
        //    {
        //        foreach (GameObject obj in buttonPanel)
        //        {
        //            obj.SetActive(true);
        //        }
        //        textOne.SetActive(true);
        //        IntroPanel.SetActive(false);
        //    }
        //}
        if (isStarOne && isStarTwo && isStarThree && !SaveManager.Instance.isLevelFiveEnd)
        {
            SaveManager.Instance.isLevelFiveEnd = true;
            int money = PlayerPrefs.GetInt("Stars");
            PlayerPrefs.SetInt("Stars", 3 + money);
            SaveManager.Instance.SaveLevel();
            SaveManager.Instance.LoadLevel();
            successpanel.SetActive(true);
        }
        else if (isStarOne && isStarTwo && isStarThree)
        {
            successpanel.SetActive(true);
        }
        if (GameObject.FindGameObjectsWithTag("Regulator") != null)
        {
            if (GameObject.FindGameObjectsWithTag("Regulator").Length >= 3)
            {
                rainPrefab.SetActive(false);
                isStarOne = true;
            }
            else
            {
                isStarOne = false;
            }

        }
        if (GameObject.FindGameObjectWithTag("base") != null)
        {
            if (GameObject.FindGameObjectWithTag("base").GetComponent<HealthSystem>().IsDead())
            {
                failPanel.SetActive(true);
            }
        }
        else if (GameObject.FindGameObjectWithTag("base") == null)
        {
            failPanel.SetActive(true);

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