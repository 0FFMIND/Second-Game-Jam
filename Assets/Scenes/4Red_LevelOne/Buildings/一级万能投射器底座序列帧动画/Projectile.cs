using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public static Action<Enemy, float> OnEnemyHit;
    // 基类
    [SerializeField] protected float moveSpeed = 10f;
    [SerializeField] private float minDistanceToDealDamage = 0.1f;
    public ParticleSystem particle;
    public TurretProjectile TurretOwner { get; set; }
    public int Damage { get; set; }
    protected Enemy _enemyTarget;
    private float timer;

    protected virtual void Update()
    {
        if (_enemyTarget != null)
        {
            MoveProjectile();
            RotateProjectile();
        }
    }

    protected virtual void MoveProjectile()
    {
        timer += Time.deltaTime;
        transform.position = Vector3.MoveTowards(new Vector3(transform.position.x,transform.position.y,1f),
            new Vector3(_enemyTarget.transform.position.x,_enemyTarget.transform.position.y,1f), moveSpeed * Time.deltaTime);
        float distanceToTarget = (_enemyTarget.transform.position - transform.position).magnitude;
        if (distanceToTarget < minDistanceToDealDamage)
        {
            _enemyTarget.DealDamage(Damage);
            _enemyTarget._enemyHealth.Damage(Damage);
            TurretOwner.ResetTurretProjectile();
            particle.Play();
            ObjectPooler.ReturnToPool(gameObject);
        }else if(timer > 5f)
        {
            timer = 0;
            ObjectPooler.ReturnToPool(gameObject);
        }
    }

    private void RotateProjectile()
    {
        Vector3 enemyPos = _enemyTarget.transform.position - transform.position;
        float angle = Vector3.SignedAngle(transform.up, enemyPos, transform.forward);
        transform.Rotate(0f, 0f, angle);
    }

    public void SetEnemy(Enemy enemy)
    {
        _enemyTarget = enemy;
    }

    public void ResetProjectile()
    {
        _enemyTarget = null;
        transform.localRotation = Quaternion.identity;
    }
}
