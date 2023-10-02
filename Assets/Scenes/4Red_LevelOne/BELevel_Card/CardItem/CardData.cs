using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class CardData : MonoBehaviour
{
    private string path;
    // 要储存的数据
    public CardStorePref cardStorePref;
    public int playerCoins;
    public List<CardDisplay> playerCards;
    public GameObject playerPool;

    private void Start()
    {
        path = Application.persistentDataPath + "/playerData.csv";
        cardStorePref.LoadCardData();
    }
    public void SavePlayerData()
    {
        for (int i = 0; i < playerPool.transform.childCount; i++)
        {
            playerCards.Add(playerPool.transform.GetChild(i).GetComponent<CardDisplay>());
        }
        List<string> data = new List<string>();
        data.Add("coins," + playerCoins.ToString());
        if(playerCards.Count != 0)
        {
            
            for (int i = 0; i < playerCards.Count; i++)
            {
                int state = (int)playerCards[i].state;
                data.Add("card," + playerCards[i].id.ToString() + "," + playerCards[i].cardLevel.text + "," + ((int)CardState.Library).ToString());
            }
        }
        //保存数据
        File.WriteAllLines(path, data);
    }
}
