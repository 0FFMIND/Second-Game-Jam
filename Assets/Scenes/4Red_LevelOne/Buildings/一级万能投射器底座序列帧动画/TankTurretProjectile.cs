using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankTurretProjectile : TurretProjectile
{
    protected override void Update()
    {
        if (Time.time > _nextAttackTime)
        {
            if (_turret.CurrentEnemyTarget != null 
                && _turret.CurrentEnemyTarget._enemyHealth.GetHealthAmount() > 0)
            {
                FireProjectile(_turret.CurrentEnemyTarget);
            }
            
            _nextAttackTime = Time.time + delayBtwAttacks;
        }
    }

    protected override void LoadProjectile() { }

    private void FireProjectile(Enemy enemy)
    {
        GameObject instance = _pooler.GetInstanceFromPool();
        instance.transform.position = projectileSpawnPosition.position;

        Projectile projectile = instance.GetComponent<Projectile>();
        _currentProjectileLoaded = projectile;
        _currentProjectileLoaded.TurretOwner = this;
        _currentProjectileLoaded.ResetProjectile();
        _currentProjectileLoaded.SetEnemy(enemy);
        _currentProjectileLoaded.Damage = Damage;
        instance.SetActive(true);
        if(gameObject.GetComponentInParent<Building>().gameObject.tag == "shootcenter")
        {
            AudioManager.Instance.PlaySFX(SoundEffect.StrongShoot);
        }
        else
        {
            AudioManager.Instance.PlaySFX(SoundEffect.Shoot);
        }
    }
}
