using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTwo : MonoBehaviour
{
    public GameObject BGpic;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Waypoint waypoint;
    public Slider slider;
    public Animator animator;
    public bool onMouseEnter;
    public bool onMouseExit;
    public bool onEnemy;
    private int _currentWaypointIndex;
    public Vector3 CurrentPointPosition => waypoint.GetWaypointPosition(_currentWaypointIndex);
    private void Start()
    {
        onEnemy = false;
        animator = GetComponent<Animator>();
        _currentWaypointIndex = 0;
        slider = GetComponentInChildren<Slider>();
    }
    private void Update()
    {
        if (!onEnemy)
        {
            Move();
            if (CurrentPointPositionReached())
            {
                UpdateCurrentIndex();
            }
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
            _currentWaypointIndex = lastWaypointIndex;
            onEnemy = true;
            animator.SetTrigger("DIE");
            CursorManager.Instance.AddCoins(30);
            Material mat = BGpic.GetComponent<Image>().material;
            mat.SetFloat("_OverlayGlow", 0f);
            AudioManager.Instance.PlaySFX(SoundEffect.Mousehit);
            mat.SetFloat("_OverlayGlow", 0.4f);
            mat.SetFloat("_ShakeUvSpeed",1f);
            Invoke(nameof(DestroyWhole), 0.2f);
        }
    }
    public void DestroyWhole()
    {
        CursorManager.Instance.duality = 40f;
        CursorManager.Instance.fingers = 4;
        CursorManager.Instance.ChangeSprite(FingerOption.four);
        AudioManager.Instance.PlaySFX(SoundEffect.Stronghit);
        Material mat = BGpic.GetComponent<Image>().material;
        mat.SetFloat("_OverlayGlow", 0.8f);
        gameObject.SetActive(false);
        mat.SetFloat("_OverlayGlow", 0f);
        mat.SetFloat("_ShakeUvSpeed", 0f);
    }
}
