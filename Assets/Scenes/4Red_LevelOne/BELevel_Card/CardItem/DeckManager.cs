using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public Transform deckPanel;
    public Transform libraryPanel;
    // ����
    public GameObject deckPrefab;
    public GameObject cardPrefab;
    public GameObject dataManager;
    // ��
    public CardStorePref storePref;
    public CardData cardData;
    public void UpdateLibrary()
    {
        for (int i = 0; i < cardData.loadPlayerCards.Count; i++)
        {

        }
    }
    public void UpdateDeck()
    {

    }
}
