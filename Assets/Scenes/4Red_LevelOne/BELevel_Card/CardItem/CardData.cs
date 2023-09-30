using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CardData : MonoBehaviour
{
    private string path;
    // 要储存的数据
    public CardStorePref cardStorePref;
    public int playerCoins;
    public List<int> playerCards;
    // 要读取的数据
    public List<int> loadPlayerCards;
    public int loadPlayerCoins;
    private void Start()
    {
        path = Application.dataPath + "/playerData.csv";
        cardStorePref.LoadCardData();
        LoadPlayerData();
    }
    public void LoadPlayerData()
    {
        playerCards = new List<int>();
        string[] dataRow = File.ReadAllLines(path);
        if (dataRow.Length == 0) return;
        foreach (var row in dataRow)
        {
            string[] rowArray = row.Split(',');
            if(rowArray[0] == "coins")
            {
                loadPlayerCoins = int.Parse(rowArray[1]);
            }
            if(rowArray[0] == "card")
            {
                loadPlayerCards.Add(int.Parse(rowArray[1]));
            }
        }
    }
    public void SavePlayerData()
    {
        List<string> data = new List<string>();
        data.Add("coins," + playerCoins.ToString());
        if(playerCards.Count != 0)
        {
            for (int i = 0; i < playerCards.Count; i++)
            {
                data.Add("card," + playerCards[i].ToString());
            }
        }
        //保存数据
        File.WriteAllLines(path, data);
    }
}
