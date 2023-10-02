using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public enum CardState
{
    Store, Library, Deck,
}

public class ZoomUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    private CardDisplay card;
    private int id;
    public CardState state;
    Vector3 localScale;
    private void Start()
    {
        card = GetComponent<CardDisplay>();
        id = card.id;
        state = card.state;
        localScale = transform.localScale;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if(state == CardState.Library)
        {
            card.state = CardState.Deck;
            return;
        }
        if(state == CardState.Deck)
        {
            card.state = CardState.Library;
            return;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.Instance.PlaySFX(SoundEffect.CardPlace);
        transform.localScale = transform.localScale * 1.1f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = localScale;
    }
}
