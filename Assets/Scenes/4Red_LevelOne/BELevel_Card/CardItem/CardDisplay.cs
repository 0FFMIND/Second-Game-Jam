using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public Text cardType;
    public Text cardLevel;
    public RawImage cardImage;
    public Text cardName;
    public Text cardDiscription;
    public Image panel;
    public Card card;
    private void Start()
    {
        ShowCard();
    }
    public void ShowCard()
    {
        cardName.text = card.cardName;
        // ������ų�����ϵͳ
        cardLevel.text = "1";
        cardImage.texture = card.tImage;
        cardType.text = card.cardType;
        cardDiscription.text = card.cardEffect;
        if(card is AngelCard)
        {
            var angel = card as AngelCard;
            panel.color = angel.cardPanelColor;
            cardName.color = angel.cardNameColor;
        }
        else if(card is DemonCard)
        {
            var demon = card as DemonCard;
            panel.color = demon.cardPanelColor;
            cardName.color = demon.cardNameColor;
        }
    }
}
