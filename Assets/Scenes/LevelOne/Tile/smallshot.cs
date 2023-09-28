using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class smallshot : MonoBehaviour
{
    public Turret _turrent;
    private void Start()
    {
        _turrent = gameObject.GetComponentInParent<Turret>();
    }
    private void Update()
    {
        if(SceneManager.GetActiveScene().name == "LevelTwo")
        {
            Vector3 movement = _turrent.direction * 100f * Time.deltaTime;
            this.transform.position += movement;
        }
        else
        {
            Vector3 movement = _turrent.direction * 10f * Time.deltaTime;
            this.transform.position += movement;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (other.GetComponent<EnemyLO>().nowHealth > 0)
            {
                //AudioManager.Instance.PlaySFX(SoundEffect.Stronghit);
                other.GetComponent<EnemyLO>().nowHealth = other.GetComponent<EnemyLO>().nowHealth - 4;
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}
