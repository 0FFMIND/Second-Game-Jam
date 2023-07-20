using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyLO : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Waypoint waypoint;
    public float nowHealth = 20;
    public Slider slider;
    public Animator animator;
    public bool onMouseEnter;
    public bool onMouseExit;
    private int _currentWaypointIndex;
    public bool isInit;
    public bool onEnemy;
    public float prehealth;
    public Vector3 CurrentPointPosition => waypoint.GetWaypointPosition(_currentWaypointIndex);
    private void Start()
    {
        onEnemy = false;
        isInit = false;
        animator = GetComponent<Animator>();
        nowHealth = 20f;
        prehealth = nowHealth;
        _currentWaypointIndex = 0;
        slider = GetComponentInChildren<Slider>();
    }
    private void Update()
    {
        if (!onEnemy)
        {
            if(prehealth!= nowHealth)
            {
                prehealth = nowHealth;
                slider.value = nowHealth;
            }
            if (onMouseEnter && onMouseExit)
            {
                onMouseExit = !onMouseExit;
                onMouseEnter = !onMouseEnter;
                SetDamage(Random.Range(1, 3));
            }
            Move();
            if (CurrentPointPositionReached())
            {
                UpdateCurrentIndex();
            }
            if (nowHealth <= 0 && !isInit)
            {
                animator.SetTrigger("DIE");
                CursorManager.Instance.AddCoins(1);
                Invoke("SetInvisible", 0.2f);
                isInit = true;
            }
        }

    }
    public void SetInvisible()
    {
        gameObject.SetActive(false);

    }
    public void SetDamage(int damage)
    {
        nowHealth = nowHealth - damage;
        slider.value = nowHealth;
    }
    private void OnMouseEnter()
    {
        if (Input.GetMouseButton(0))
        {
            Material mat = GetComponent<Renderer>().material;
            mat.SetFloat("_OutlineAlpha", 1f);
            AudioManager.Instance.PlaySFX(SoundEffect.Mousehit);
            onMouseEnter = true;
            animator.SetTrigger("HIT");
            CursorManager.Instance.Damage(1);
        }
    }
    private void OnMouseExit()
    {
        if (Input.GetMouseButton(0))
        {
            Material mat = GetComponent<Renderer>().material;
            mat.SetFloat("_OutlineAlpha", 0f);
            AudioManager.Instance.PlaySFX(SoundEffect.Mousehit);
            onMouseExit = true;
            animator.SetTrigger("HIT");
            CursorManager.Instance.Damage(1);
        }
    }
    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, CurrentPointPosition, moveSpeed * Time.deltaTime);
    }
    private bool CurrentPointPositionReached()
    {
        float distanceToNextPointPosition = (transform.position - CurrentPointPosition).magnitude;
        if (distanceToNextPointPosition < 0.1f)
        {
            return true;
        }
        return false;
    }
    private void UpdateCurrentIndex()
    {
        int lastWaypointIndex = waypoint.Points.Length - 1;
        if (_currentWaypointIndex < lastWaypointIndex)
        {
            _currentWaypointIndex++;
        }
        else
        {
            if (!onEnemy)
            {
                onEnemy = true;
                animator.SetTrigger("DIE");
                CursorManager.Instance.AddCoins(30);
                CursorManager.Instance.ChangeFinger();
                gameObject.SetActive(false);
            }
        }
    }
}
