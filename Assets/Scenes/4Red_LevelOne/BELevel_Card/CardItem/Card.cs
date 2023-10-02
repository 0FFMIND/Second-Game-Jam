using UnityEngine;
using UnityEngine.UI;

public class Card
{
    public int id;
    public int level;
    public CardState state;
    public string cardName;
    public string cardType;
    public string cardEffect;
    public Texture tImage;
    //每个都写上构造函数
    public Card(int _id,int _level, CardState _state, string _cardName, string _cardType, string _cardEffect, Texture _tImage)
    {
        this.id = _id;
        this.level = _level;
        this.state = _state;
        this.cardName = _cardName;
        this.cardType = _cardType;
        this.cardEffect = _cardEffect;
        this.tImage = _tImage;
    }
}
public class AngelCard : Card
{
    public Color cardPanelColor;
    public Color cardNameColor;
    public AngelCard(int _id, int _level,CardState _state, string _cardName, string _cardType, string _cardEffect, Texture _tImage, Color _cardPanelColor, Color _cardNameColor) : base(_id, _level, _state, _cardName, _cardType, _cardEffect,_tImage)
    {
        this.cardNameColor = _cardNameColor;
        this.cardPanelColor = _cardPanelColor;
    }
}
public class DemonCard : Card
{
    public Color cardPanelColor;
    public Color cardNameColor;
    public DemonCard(int _id, int _level, CardState _state, string _cardName, string _cardType, string _cardEffect, Texture _tImage, Color _cardPanelColor, Color _cardNameColor) : base(_id, _level, _state, _cardName, _cardType, _cardEffect, _tImage)
    {
        this.cardNameColor = _cardNameColor;
        this.cardPanelColor = _cardPanelColor;
    }
}