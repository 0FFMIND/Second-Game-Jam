using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineProjectile : Projectile
{
    public Vector2 Direction { get; set; }

    protected override void Update()
    {
        MoveProjectile();
    }

    protected override void MoveProjectile()
    {
        Vector2 movement = Direction.normalized * moveSpeed * Time.deltaTime;
        transform.Translate(movement);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy._enemyHealth.GetHealthAmount() > 0f)
            {
                OnEnemyHit?.Invoke(enemy, Damage);
                enemy._enemyHealth.Damage(Damage);
            }

            ObjectPooler.ReturnToPool(gameObject);
        }
    }

    private void OnEnable()
    {
        StartCoroutine(ObjectPooler.ReturnToPoolWithDelay(gameObject, 5f));
    }
}
