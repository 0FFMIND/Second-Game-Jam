using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private HealthSystem healthSystem;
    private Transform barTransform;
    private void Start()
    {
        barTransform = transform.Find("HealthBar");
        healthSystem.OnDamaged += HealthSystem_OnDamaged;
        UpdateBar();
    }
    private void HealthSystem_OnDamaged(object sender, System.EventArgs e)
    {
        AudioManager.Instance.PlaySFX(SoundEffect.Hit);
        UpdateBar();
    }
    private void UpdateBar()
    {
        barTransform.localScale = new Vector3(1.37076f * healthSystem.GetHealthAmountNormalized(), 2.037019f * 1f, 1f);
    }
}
