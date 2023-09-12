using System;
using UnityEngine;
using UnityEngine.UI;
public class Node : MonoBehaviour
{
    [SerializeField] private DataController dataController;
    [SerializeField] private float purchaseCoins;
    [SerializeField] private GameObject buyPanel;
    [SerializeField] private GameObject attackRangeSprite;
    [SerializeField] private Turret turret;
    [SerializeField] private float _rangeSize;
    [SerializeField] private Vector3 _rangeOriginalSize;
    public bool isTurretOn;
    private void Start()
    {
        isTurretOn = false;
        buyPanel.SetActive(false);
        turret.gameObject.SetActive(false);
        attackRangeSprite.SetActive(false);
        _rangeSize = attackRangeSprite.GetComponent<SpriteRenderer>().bounds.size.y;
        _rangeOriginalSize = attackRangeSprite.transform.localScale;
    }
    public void OnMouseEnter()
    {
        attackRangeSprite.SetActive(true);
        if (!isTurretOn)
        {
            buyPanel.SetActive(true);
        }
    }
    public void OnMouseDown()
    {
        //if (!isTurretOn)
        //{
        //    int nowcoins = int.Parse(dataController.coinText.text);
        //    if (nowcoins >= purchaseCoins)
        //    {
        //        CursorManager.Instance.AddCoins((int)-purchaseCoins);
        //        SetTurret();
        //    }
        //    buyPanel.SetActive(false);
        //}

    }
    public void OnMouseExit()
    {
        buyPanel.SetActive(false);
        attackRangeSprite.SetActive(false);
    }
    public void SetTurret()
    {
        isTurretOn = true;
        turret.gameObject.SetActive(true);
    }
}
