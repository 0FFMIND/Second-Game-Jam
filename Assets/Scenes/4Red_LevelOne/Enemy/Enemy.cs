using System;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public HealthSystem _enemyHealth;
    private SpriteRenderer _spriteRenderer;
    private Animator animator;
    private bool isFirst = false;
    Vector3 lastPosition;
    public static Action<Enemy> OnEnemyKilled;
    public static Action<Enemy> OnEnemyHit;

    public float CurrentHealth;
    private EnemyFX _enemyFX;
    public GameObject deathParticles;

    private void PlayHurtAnimation()
    {
        animator.SetTrigger("Hit");
        Instantiate(deathParticles, transform.position, Quaternion.identity);
    }
    private IEnumerator PlayHurt()
    {
        yield return null;
        PlayHurtAnimation();
    }
    private IEnumerator PlayDead()
    {
        animator.SetTrigger("Die");
        Instantiate(deathParticles, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }

    public void DealDamage(float damageReceived)
    {
        AudioManager.Instance.PlaySFX(SoundEffect.Hit);
        CurrentHealth = _enemyHealth.GetHealthAmount();
        CurrentHealth -= damageReceived;
        if (CurrentHealth <= 0)
        {
            StartCoroutine(PlayDead());
        }
        else
        {
            if (SkillGhost.isCardThree)
            {
                this.gameObject.GetComponent<PathFollower>().Speed = 0.3f;
            }
            StartCoroutine(PlayHurt());
        }
    }

    private void Start()
    {
        _enemyFX = GetComponent<EnemyFX>();
        animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _enemyHealth = GetComponent<HealthSystem>();
        if (GameObject.FindGameObjectWithTag("defenseholder"))
        {
            CurrentHealth = _enemyHealth.GetHealthAmount() - 10f;
        }
        else {

            CurrentHealth = _enemyHealth.GetHealthAmount() - 10f;
        }
    }

    private void Update()
    {
        Rotate();
    }
    private void Rotate()
    {
        if (!isFirst) {
            isFirst = true;
            lastPosition = transform.position;
        }
        if (transform.position.x > lastPosition.x + 0.05f)
        {
            _spriteRenderer.flipX = true;
        }
        else
        {
            _spriteRenderer.flipX = false ;
        }
        lastPosition = transform.position;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "base")
        {
            Vector3 EndPos = transform.position;
            EndPointReached(EndPos);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.gameObject.tag == "base")
        {
            Vector3 EndPos = transform.position;
            EndPointReached(EndPos);
        }
    }
    private void EndPointReached(Vector3 endpos)
    {
        transform.position = endpos;
        StartCoroutine(PlayDead());
        //遍历场上装了building的物体
        if(GameObject.FindGameObjectsWithTag("solar") != null && GameObject.FindGameObjectsWithTag("solar").Length != 0)
        {
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("solar").Length; i++)
            {
                GameObject.FindGameObjectsWithTag("solar")[i].GetComponent<Building>().healthSystem.Damage(10);
            }
        }if(GameObject.FindGameObjectsWithTag("windstation") != null && GameObject.FindGameObjectsWithTag("windstation").Length != 0)
        {
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("windstation").Length; i++)
            {
                GameObject.FindGameObjectsWithTag("windstation")[i].GetComponent<Building>().healthSystem.Damage(10);
            }
        }
        if (GameObject.FindGameObjectsWithTag("collector") != null && GameObject.FindGameObjectsWithTag("collector").Length != 0)
        {
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("collector").Length; i++)
            {
                GameObject.FindGameObjectsWithTag("collector")[i].GetComponent<Building>().healthSystem.Damage(10);
            }
        }
        if (GameObject.FindGameObjectsWithTag("Regulator") != null && GameObject.FindGameObjectsWithTag("Regulator").Length != 0)
        {
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("Regulator").Length; i++)
            {
                GameObject.FindGameObjectsWithTag("Regulator")[i].GetComponent<Building>().healthSystem.Damage(10);
            }
        }
        if(GameObject.FindGameObjectsWithTag("turret")!= null && GameObject.FindGameObjectsWithTag("turret").Length != 0)
        {
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("turret").Length; i++)
            {
                GameObject.FindGameObjectsWithTag("turret")[i].GetComponent<Building>().healthSystem.Damage(10);
            }
        }
        if (GameObject.FindGameObjectsWithTag("defenseholder") != null && GameObject.FindGameObjectsWithTag("defenseholder").Length != 0)
        {
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("defenseholder").Length; i++)
            {
                GameObject.FindGameObjectsWithTag("defenseholder")[i].GetComponent<Building>().healthSystem.Damage(10);
            }
        }
        if (GameObject.FindGameObjectsWithTag("shootcenter") != null && GameObject.FindGameObjectsWithTag("shootcenter").Length != 0)
        {
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("shootcenter").Length; i++)
            {
                GameObject.FindGameObjectsWithTag("shootcenter")[i].GetComponent<Building>().healthSystem.Damage(10);
            }
        }
        if(SceneManager.GetActiveScene().name == "LevelThree" || SceneManager.GetActiveScene().name == "LevelFour" || SceneManager.GetActiveScene().name == "LevelFive" || SceneManager.GetActiveScene().name == "LevelSix")
        {
            if (GameObject.FindGameObjectsWithTag("base") != null)
            {
                for (int i = 0; i < GameObject.FindGameObjectsWithTag("base").Length; i++)
                {
                    GameObject.FindGameObjectsWithTag("base")[i].GetComponent<Building>().healthSystem.Damage(10);
                }
            }
        }

    }

}
