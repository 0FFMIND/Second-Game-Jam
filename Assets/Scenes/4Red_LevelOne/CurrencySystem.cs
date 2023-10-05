using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencySystem : MonoBehaviour
{
    public int powers;
    public void InitCoins(int num)
    {
        powers = num;
    }
    public void AddCoins(int num)
    {
        powers += num;
    }
    public void DeleteCoins(int num)
    {
        if(powers >= num)
        {
            powers -= num;
        }
    }
    public int GetCoins()
    {
        return powers;
    }

}
