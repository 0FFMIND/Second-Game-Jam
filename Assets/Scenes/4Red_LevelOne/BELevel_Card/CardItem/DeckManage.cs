using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DeckManage : MonoBehaviour
{
    public Transform deckPanel;
    public Transform libraryPanel;
    public Transform generatePanel;
    // 数据
    public GameObject cardPrefab;
    // 
    public bool isDeckFinished = false;
    public void LoadLibrary()
    {
        for (int i = 0; i < libraryPanel.childCount; i++)
        {
            Destroy(libraryPanel.GetChild(i).gameObject);
        }
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

    private void Start()
    {
        LoadLibrary();
    }
    private void Update()
    {
    }
    public void DeckFinish()
    {
        if (isDeckFinished)
        {
            isDeckFinished = false;
            SaveCard();
            SaveManager.Instance.IsPoolFinshed = true;
            SaveManager.Instance.SaveLevel();
            SaveManager.Instance.LoadLevel();
        }
    }
    public void SaveCard()
    {
        string path = Application.persistentDataPath + "/playerData.csv";
        File.Delete(path);
        List<string> data = new List<string>();
        for (int i = 0; i < generatePanel.childCount; i++)
        {
            data.Add("card," + generatePanel.GetChild(i).GetComponent<CardDisplay>().id.ToString() + "," +
                generatePanel.GetChild(i).GetComponent<CardDisplay>().card.level.ToString() + "," + ((int)CardState.Library).ToString());
        }
        //保存数据
        File.WriteAllLines(path, data);
        LoadLibrary();

    }
}