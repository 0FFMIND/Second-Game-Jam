using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class OpenPackage : MonoBehaviour
{
    public GameObject cardPrefab;
    public Cardstr cardStore;
    public CardStorePref cardStorePref;
    public Transform cardPool;
    public List<GameObject> cardTempStore;
    public void OnClickOpen()
    {
        cardTempStore.Clear();
        if(cardStore!= null)
        {
            cardStore.LoadCardData();
        }
        if(cardStorePref != null)
        {
            cardStorePref.LoadCardData();
        }
        AudioManager.Instance.PlaySFX(SoundEffect.CardPlace);
        // ÷ÿ≥È«∞«Âø’ø®≈∆
        if (cardPool.childCount != 0)
        {
            CardDisplay[] all = cardPool.GetComponentsInChildren<CardDisplay>();
            foreach (var single in all)
            {
                Destroy(single.gameObject);
            }
        }
        for (int i = 0; i < 5; i++)
        {            GameObject newCard = GameObject.Instantiate(cardPrefab, cardPool);
            cardTempStore.Add(newCard);
            if(cardStorePref != null)
            {
                newCard.GetComponent<CardDisplay>().card = cardStorePref.RandomCard();
            }else if(cardStore != null)
            {
                newCard.GetComponent<CardDisplay>().card = cardStore.RandomCard();
            }
        }
    }
}
