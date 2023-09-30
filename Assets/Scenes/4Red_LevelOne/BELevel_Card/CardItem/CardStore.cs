using System.Collections.Generic;
using UnityEngine;

public class CardStore : MonoBehaviour
{
    public TextAsset CNcardData;
    public TextAsset ENcardData;
    public Texture[] textureList;
    public GameObject HintCN;
    public GameObject HintEN;
    public List<Card> AcardList = new List<Card>();
    public List<Card> DcardList = new List<Card>();
    private void Start()
    {
        if(LanguageManager.Instance.CurrentLanguage == LanguageOption.Chinese)
        {
            HintCN.SetActive(true);HintEN.SetActive(false);
        }else if (LanguageManager.Instance.CurrentLanguage == LanguageOption.English)
        {
            HintEN.SetActive(true);HintCN.SetActive(false);
        }
    }
    // ´æ¿¨
    public void LoadCardData()
    {
        AcardList.Clear();
        DcardList.Clear();
        string[] dataRow;
        if(LanguageManager.Instance.CurrentLanguage == LanguageOption.Chinese)
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
            if(rowArray[0] == "#")
            {
                continue;
            }else if(rowArray[0] == "Angel")
            {
                string cardType = rowArray[1];
                string cardName = rowArray[2];
                int id = int.Parse(rowArray[3]);
                string cardEffet = rowArray[4];
                Texture texture = textureList[id - 1];
                Color cardPanelColor = Color.white;
                Color cardNameColor = Color.cyan;
                AngelCard angelCard = new AngelCard(id, cardName, cardType, cardEffet, texture, cardPanelColor, cardNameColor);
                AcardList.Add(angelCard);
            }else if(rowArray[0] == "Demon")
            {
                string cardType = rowArray[1];
                string cardName = rowArray[2];
                int id = int.Parse(rowArray[3]);
                string cardEffet = rowArray[4];
                Texture texture = textureList[id - 1];
                Color cardPanelColor = Color.yellow;
                Color cardNameColor = Color.yellow;
                DemonCard demonCard = new DemonCard(id, cardName, cardType, cardEffet, texture, cardPanelColor, cardNameColor);
                DcardList.Add(demonCard);
            }
        }
    }
    // ·Å¿¨
    public Card RandomCard()
    {
        int num = Random.Range(0,100);
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
