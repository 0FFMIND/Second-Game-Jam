using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public Transform deckPanel;
    public Transform libraryPanel;
    // 数据
    public GameObject cardPrefab;
    // 
    public bool isDeckFinished = false;
    // 指示框
    public GameObject recheck;
    public GameObject confirmBtn;
    // 随场景一起消失
    public GameObject Text1;
    public GameObject Text2;
    public GameObject Text3;
    public void LoadLibrary()
    {
        CardStorePref.Instance.GetCard();
        foreach (var item in CardStorePref.Instance.loadPlayerCards)
        {
            GameObject newCard = Instantiate(cardPrefab, libraryPanel);
            int cardid = item.Key; int cardlevel = item.Value;
            newCard.GetComponent<CardDisplay>().card = CardStorePref.Instance.CardLibrary[cardid];
            newCard.GetComponent<CardDisplay>().card.id = cardid;
            newCard.GetComponent<CardDisplay>().card.state = CardState.Library;
            newCard.GetComponent<CardDisplay>().card.level = cardlevel;
            newCard.AddComponent<ZoomUI>();
        }
    }
    public void UpdateDeck()
    {
        isDeckFinished = false;
        if (libraryPanel.childCount > 2) // 才能进行操作
        {
            for (int i = 0; i < libraryPanel.childCount; i++)
            {
                GameObject card = libraryPanel.GetChild(i).gameObject;
                int id = card.GetComponent<CardDisplay>().card.id;
                CardState state = card.GetComponent<CardDisplay>().state;
                if (state == CardState.Deck)
                {
                    Destroy(card);
                    GameObject newCard = Instantiate(cardPrefab, deckPanel);
                    newCard.GetComponent<CardDisplay>().card = CardStorePref.Instance.CardLibrary[id];
                    newCard.GetComponent<CardDisplay>().card.level = CardStorePref.Instance.loadPlayerCards[id];
                    newCard.GetComponent<CardDisplay>().card.state = CardState.Deck;
                    newCard.GetComponent<CardDisplay>().state = CardState.Deck;
                    newCard.AddComponent<ZoomUI>();
                    break;
                }
            }
        }
        if (deckPanel.childCount <= 3 && deckPanel.childCount > 0) // 才能执行操作
        {
            for (int i = 0; i < deckPanel.childCount; i++)
            {
                GameObject card = deckPanel.GetChild(i).gameObject;
                int id = card.GetComponent<CardDisplay>().card.id;
                CardState state = card.GetComponent<CardDisplay>().state;
                if (state == CardState.Library)
                {
                    Destroy(card);
                    GameObject newCard = Instantiate(cardPrefab, libraryPanel);
                    newCard.GetComponent<CardDisplay>().card = CardStorePref.Instance.CardLibrary[id];
                    newCard.GetComponent<CardDisplay>().card.level = CardStorePref.Instance.loadPlayerCards[id];
                    newCard.GetComponent<CardDisplay>().card.state = CardState.Library;
                    newCard.GetComponent<CardDisplay>().state = CardState.Library;
                    newCard.AddComponent<ZoomUI>();
                    break;
                }
            }
        }
    }

    private void Start()
    {
        LoadLibrary();
        recheck.SetActive(false);
    }
    private void Update()
    {
        UpdateDeck();
    }
    public void RecheckDisable()
    {
        recheck.SetActive(false);
        confirmBtn.SetActive(true);
    }
    public void DeckFinish()
    {
        if (deckPanel.childCount == 3 && libraryPanel.childCount == 2)
        {
            isDeckFinished = true;
        }
        if (!isDeckFinished)
        {
            recheck.SetActive(true);
            confirmBtn.SetActive(false);
        }
        if (isDeckFinished)
        {
            isDeckFinished = false;
            Text1.SetActive(false);Text2.SetActive(false);Text3.SetActive(false);
            SaveCard();
            SaveManager.Instance.IsPoolFinshed = true;
            SaveManager.Instance.SaveLevel();
            SaveManager.Instance.LoadLevel();
            TransManager.Instance.ChangeScene("LevelOne");
        }
    }
    public void SaveCard()
    {
        string path = Application.persistentDataPath + "/playerData.csv";
        List<string> data = new List<string>();
        for (int i = 0; i < libraryPanel.childCount; i++)
        {
            data.Add("card," + libraryPanel.GetChild(i).GetComponent<CardDisplay>().id.ToString() + "," +
                libraryPanel.GetChild(i).GetComponent<CardDisplay>().card.level.ToString() + "," + ((int)CardState.Library).ToString());
        }
        for (int i = 0; i < deckPanel.childCount; i++)
        {
            data.Add("card," + deckPanel.GetChild(i).GetComponent<CardDisplay>().id.ToString() + "," +
                deckPanel.GetChild(i).GetComponent<CardDisplay>().card.level.ToString() + "," + ((int)CardState.Deck).ToString());
        }
        //保存数据
        File.WriteAllLines(path, data);
    }
}
