using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : Singleton<CardManager>
{
    public List<string> cardList;
    public List<string> usedCardList;
    public void Init()
    {
        cardList = new List<string>();
        usedCardList = new List<string>();
        //������ʱ���ƿ�
        List<string> tempList = new List<string>();
        //tempList.AddRange()��������
        while(tempList.Count > 0)
        {
            int tempIndex = Random.Range(0, tempList.Count); // ����±�
        }
    }
}
