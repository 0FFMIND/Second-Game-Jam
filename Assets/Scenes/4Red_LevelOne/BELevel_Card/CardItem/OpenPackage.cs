
using System.Collections.Generic;
using UnityEngine;

public class OpenPackage : MonoBehaviour
{
    public GameObject cardPrefab;
    public CardStore cardStore;
    public Transform cardPool;
    public CardData cardData;
    public List<GameObject> cardTempStore;
    public void OnClickOpen()
    {
        cardTempStore.Clear();
        AudioManager.Instance.PlaySFX(SoundEffect.CardPlace);
        // 重抽前清空卡牌
        if (cardPool.childCount != 0)
        {
            CardDisplay[] all = cardPool.GetComponentsInChildren<CardDisplay>();
            foreach (var single in all)
            {
                Destroy(single.gameObject);
            }
        }
        cardStore.LoadCardData();
        for (int i = 0; i < 5; i++)
        {
            GameObject newCard = GameObject.Instantiate(cardPrefab, cardPool);
            cardTempStore.Add(newCard);
            newCard.GetComponent<CardDisplay>().card = cardStore.RandomCard();
        }
    }
    // 按下确认按钮后
    public void OnConfirm()
    {
        foreach (var card in cardTempStore)
        {
            int id = card.GetComponent<CardDisplay>().card.id;
            cardData.playerCards.Add(id);
        }
    }
}
