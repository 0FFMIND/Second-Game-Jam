using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyFX : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI damageText;
    private Enemy _enemy;
    private void Start()
    {
        _enemy = GetComponent<Enemy>();
    }

    public void EnemyHit(Enemy enemy, float damage)
    {
        if (_enemy == enemy)
        {
            damageText.text = damage.ToString();
        }
    }
}

