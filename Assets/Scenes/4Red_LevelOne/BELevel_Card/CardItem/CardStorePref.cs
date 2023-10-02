using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardStorePref : Singleton<CardStorePref>
{
    public TextAsset CNcardData;
    public TextAsset ENcardData;
    public Texture[] textureList;
    public List<Card> AcardList = new List<Card>();
    public List<Card> DcardList = new List<Card>();
    public Dictionary<int, Card> CardLibrary = new Dictionary<int, Card>();
    public Dictionary<int, CardState> stateCard = new Dictionary<int, CardState>();
    // 用来取走存档的卡牌
    public Dictionary<int, CardState> loadStateCard;
    public Dictionary<int, int> loadPlayerCards;
    //取卡
    public void GetCard()
    {
        if(CardLibrary.Count == 0)
        {
            LoadCardData();
        }
        if(loadPlayerCards.Count == 0 && loadStateCard == null)
        {
            string path = Application.persistentDataPath + "/playerData.csv";
            string[] storeDataRow = File.ReadAllLines(path);
            loadStateCard = new Dictionary<int, CardState>();
            loadPlayerCards = new Dictionary<int, int>();
            foreach (var data in storeDataRow)
            {
                string[] array = data.Split(',');
                if (array[0] == "card")
                {
                    loadPlayerCards.Add(int.Parse(array[1]), int.Parse(array[2]));
                    stateCard.Add(int.Parse(array[1]), (CardState)int.Parse(array[3]));
                }
            }
        }
    }
    // 存12张卡
    public void LoadCardData()
    {
        AcardList.Clear();
        DcardList.Clear();
        string[] dataRow;
        if (LanguageManager.Instance.CurrentLanguage == LanguageOption.Chinese)
        {
            dataRow = CNcardData.text.Split('\n');
        }
        else
        {
            dataRow = ENcardData.text.Split('\n');
        }
        foreach (var row in dataRow)
        {
            string[] rowArray = row.Split(',');
            if (rowArray[0] == "#")
            {
                continue;
            }
            else if (rowArray[0] == "Angel")
            {
                int level = 1;
                CardState state = CardState.Store;
                string cardType = rowArray[1];
                string cardName = rowArray[2];
                int id = int.Parse(rowArray[3]);
                string cardEffet = rowArray[4];
                Texture texture = textureList[id - 1];
                //决定level
                try
                {
                    string path = Application.persistentDataPath + "/playerData.csv";
                    loadPlayerCards = new Dictionary<int, int>();
                    string[] storeDataRow = File.ReadAllLines(path);
                    if (storeDataRow.Length == 0)
                    {
                        level = 1;
                        state = CardState.Store;
                    }
                    else
                    {
                        foreach (var data in storeDataRow)
                        {
                            string[] array = data.Split(',');
                            if (array[0] == "card")
                            {
                                loadPlayerCards.Add(int.Parse(rowArray[1]), int.Parse(rowArray[2]));
                                stateCard.Add(int.Parse(rowArray[1]), (CardState)int.Parse(rowArray[3]));
                            }
                        }
                        level = loadPlayerCards[id];
                        state = stateCard[id];
                    }
                }
                catch(Exception e)
                {

                }

                Color cardPanelColor = Color.white;
                Color cardNameColor = Color.cyan;
                AngelCard angelCard = new AngelCard(id, level, state, cardName, cardType, cardEffet, texture, cardPanelColor, cardNameColor);
                AcardList.Add(angelCard);
                CardLibrary.Add(id, angelCard);
            }
            else if (rowArray[0] == "Demon")
            {
                int level = 1;
                CardState state = CardState.Library;
                string cardType = rowArray[1];
                string cardName = rowArray[2];
                int id = int.Parse(rowArray[3]);
                string cardEffet = rowArray[4];
                Texture texture = textureList[id - 1];
                //决定level
                try
                {
                    string path = Application.persistentDataPath + "/playerData.csv";
                    string[] storeDataRow = File.ReadAllLines(path);
                    if (storeDataRow.Length == 0)
                    {
                        level = 1;
                        state = CardState.Store;
                    }
                    else
                    {
                        foreach (var data in storeDataRow)
                        {
                            string[] array = data.Split(',');
                            if (array[0] == "card")
                            {
                                loadPlayerCards.Add(int.Parse(rowArray[1]), int.Parse(rowArray[2]));
                                stateCard.Add(int.Parse(rowArray[1]), (CardState)int.Parse(rowArray[3]));
                            }
                        }
                        level = loadPlayerCards[id];
                        state = stateCard[id];
                    }
                }
                catch(Exception e)
                {

                }

                Color cardPanelColor = Color.yellow;
                Color cardNameColor = Color.yellow;
                DemonCard demonCard = new DemonCard(id, level, state, cardName, cardType, cardEffet, texture, cardPanelColor, cardNameColor);
                DcardList.Add(demonCard);
                CardLibrary.Add(id, demonCard);
            }
        }
    }
    // 放卡
    public Card RandomCard()
    {
        int num = Random.Range(0, 100);
        SaveManager.Instance.LoadLevel();
        if (SaveManager.Instance.IsAngel == 2 && num > 35)
        {
            int random = Random.Range(0, AcardList.Count);
            Card card = AcardList[random];
            AcardList.RemoveAt(random);
            return card;
        }
        else if (SaveManager.Instance.IsAngel == 2 && num < 35)
        {
            int random = Random.Range(0, DcardList.Count);
            Card card = DcardList[random];
            DcardList.RemoveAt(random);
            return card;
        }
        else if (SaveManager.Instance.IsAngel == 1 && num > 35)
        {
            int random = Random.Range(0, DcardList.Count);
            Card card = DcardList[random];
            DcardList.RemoveAt(random);
            return card;
        }
        else if (SaveManager.Instance.IsAngel == 1 && num < 35)
        {
            int random = Random.Range(0, AcardList.Count);
            Card card = AcardList[random];
            AcardList.RemoveAt(random);
            return card;
        }
        return null;
    }
}
