using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Enemy CurrentEnemyTarget { get; set; }
    private List<Enemy> _enemies;
    public GameObject targetArea;

    private void Start()
    {
        _enemies = new List<Enemy>();
    }

    private void Update()
    {
        GetCurrentEnemyTarget();
        RotateTowardsTarget();
    }

    private void GetCurrentEnemyTarget()
    {
        if (_enemies.Count <= 0)
        {
            CurrentEnemyTarget = null;
            return;
        }
        CurrentEnemyTarget = _enemies[0];
    }

    private void RotateTowardsTarget()
    {
        if (CurrentEnemyTarget == null)
        {
            return;
        }
        Vector3 targetPos = CurrentEnemyTarget.transform.position - transform.position;
        float angle = Vector3.SignedAngle(transform.up, Vector3.Lerp(transform.up, targetPos, 3f * Time.deltaTime), transform.forward);
        transform.Rotate(0f, 0f, angle);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy newEnemy = other.GetComponent<Enemy>();
            _enemies.Add(newEnemy);
            targetArea.SetActive(true);
        }
        SaveManager.Instance.LoadLevel();
        if (SaveManager.Instance.isBuffFour)
        {
            for (int i = 0; i < _enemies.Count; i++)
            {
                _enemies[i]._enemyHealth.Damage(10);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (_enemies.Contains(enemy))
            {
                _enemies.Remove(enemy);
                targetArea.SetActive(false);
            }
        }
    }
}