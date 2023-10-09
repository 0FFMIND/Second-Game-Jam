using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cardstr : MonoBehaviour
{
    public TextAsset CNcardData;
    public TextAsset ENcardData;
    public Texture[] textureList;
    public List<Card> AcardList = new List<Card>();
    public List<Card> DcardList = new List<Card>();
    private void Start()
    {
        LoadCardData();
    }
    // ´æ¿¨
    public void LoadCardData()
    {
        SaveManager.Instance.LoadLevel();
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
                CardState state = CardState.Library;
                string cardType = rowArray[1];
                string cardName = rowArray[2];
                int id = int.Parse(rowArray[3]);
                string cardEffet = rowArray[4];
                Texture texture = textureList[id - 1];
                Color cardPanelColor = Color.white;
                Color cardNameColor = Color.cyan;
                AngelCard angelCard = new AngelCard(id, level, state, cardName, cardType, cardEffet, texture, cardPanelColor, cardNameColor);
                AcardList.Add(angelCard);
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
            
                Color cardPanelColor = Color.yellow;
                Color cardNameColor = Color.yellow;
                DemonCard demonCard = new DemonCard(id, level, state, cardName, cardType, cardEffet, texture, cardPanelColor, cardNameColor);
                DcardList.Add(demonCard);
            }
        }
    }
    // ·Å¿¨
    public Card RandomCard()
    {
        int num = Random.Range(0, 100);
        SaveManager.Instance.LoadLevel();
        if (SaveManager.Instance.IsAngel == 1 && num > 35)
        {
            int random = Random.Range(0, AcardList.Count);
            Card card = AcardList[random];
            AcardList.RemoveAt(random);
            return card;
        }
        else if (SaveManager.Instance.IsAngel == 1 && num < 35)
        {
            int random = Random.Range(0, DcardList.Count);
            Card card = DcardList[random];
            DcardList.RemoveAt(random);
            return card;
        }
        else if (SaveManager.Instance.IsAngel == 2 && num > 35)
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
        return null;
    }
}