
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
        // �س�ǰ��տ���
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
    // ����ȷ�ϰ�ť��
    public void OnConfirm()
    {
        foreach (var card in cardTempStore)
        {
            int id = card.GetComponent<CardDisplay>().card.id;
            cardData.playerCards.Add(id);
        }
    }
}
