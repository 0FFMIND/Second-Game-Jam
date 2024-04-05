using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public event EventHandler OnDamaged;
    public event EventHandler OnDied;
    private bool onlyOnce = false;
    // Damage 和 Died 用于给外界注册
    public Action<Enemy> OnEnemyKilled;
    public Action<Enemy> OnEnemyHit;
    // 给Enemy的
    [SerializeField] private int healthAmountMax;
    private int healthAmount;
    private void Awake()
    {
        healthAmount = healthAmountMax;
    }
    private void Update()
    {
        if(GameObject.FindGameObjectWithTag("defenseholder") != null && !onlyOnce)
        {
            onlyOnce = true;
            Damage(-20);
        }
    }
    public void Damage(int damageAmonut)
    {
        if (damageAmonut > 0)
        {
            AudioManager.Instance.StopSFX();
            AudioManager.Instance.PlaySFX(SoundEffect.Hit);
        }
        healthAmount -= damageAmonut;
        healthAmount = Mathf.Clamp(healthAmount, 0, healthAmountMax);
        OnDamaged?.Invoke(this, EventArgs.Empty);
        if (IsDead())
        {
            OnDied?.Invoke(this, EventArgs.Empty);
        }
    }
    public bool IsDead()
    {
        return healthAmount == 0;
    }
    public int GetHealthAmount()
    {
        return healthAmount;
    }
    public float GetHealthAmountNormalized()
    {
        return (float)healthAmount / healthAmountMax;
    }
    public void SetHealthAmountMax(int healthAmountMax, bool updateHealthAmount)
    {
        this.healthAmountMax = healthAmountMax;
        if (updateHealthAmount)
        {
            healthAmount = healthAmountMax;
        }
    }
}
