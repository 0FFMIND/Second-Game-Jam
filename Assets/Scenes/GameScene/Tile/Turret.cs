using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Turret : MonoBehaviour
{
    public EnemyLO currentEnemyLO;
    public List<EnemyLO> _enemies;
    public Vector3 direction;
    private float lastangle;
    private void Start()
    {
        _enemies = new List<EnemyLO>();
    }
    private void Update()
    {
        GetCurrentEnemyTarget();
        RotateTowardsTarget();
    }
    public void GetCurrentEnemyTarget()
    {
        if (_enemies.Count <= 0)
        {
            currentEnemyLO = null;
            return;
        }
        currentEnemyLO = _enemies[0];
    }
    public void RotateTowardsTarget()
    {
        if (currentEnemyLO == null)
        {
            return;
        }
        Vector3 targetPos = currentEnemyLO.transform.position - transform.position;
        if(SceneManager.GetActiveScene().name == "LevelOne")
        {
            float angle = Vector3.SignedAngle(transform.up, Vector3.Lerp(transform.up, targetPos, 3f * Time.deltaTime), transform.forward);
            transform.Rotate(0f, 0f, angle);

        }
        if (SceneManager.GetActiveScene().name == "LevelTwo")
        {
            float angle = Vector3.SignedAngle(transform.up, targetPos, transform.forward);
            angle = Mathf.Lerp(0, angle, Time.deltaTime);
            transform.Rotate(0f, 0f, angle);
        }
        direction = targetPos.normalized;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            EnemyLO newEnemy = other.GetComponent<EnemyLO>();
            _enemies.Add(newEnemy);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyLO enemy = other.GetComponent<EnemyLO>();
            if (_enemies.Contains(enemy))
            {
                _enemies.Remove(enemy);
            }
        }
    }
}
