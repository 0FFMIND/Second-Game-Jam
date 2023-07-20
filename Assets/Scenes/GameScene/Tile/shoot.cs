using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{

    /// �ڵ�����λ��
    [SerializeField] protected Transform turrentTrans;
    [SerializeField] protected float delayBtwAttacks = 1f;
    [SerializeField] protected float moveSpeed = 10f;
    /// �ڵ��˺�
    [SerializeField] protected float damage = 4f;

    public float Damage { get; set; }
    /// ÿ��������ӳ�
    public float DelayPerShot { get; set; }

    protected float _nextAttackTime;
    protected Turret _turret;
    public GameObject go;
    public GameObject _go;

    private void Start()
    {
        _turret = GetComponent<Turret>();
        turrentTrans = _turret.gameObject.transform;
        Damage = damage;
        DelayPerShot = delayBtwAttacks;
        LoadProjectile();
    }

    protected virtual void Update()
    {
        if (Time.time > _nextAttackTime)
        {
            if (_turret.currentEnemyLO != null && 
                _turret.currentEnemyLO.nowHealth > 0f)
            {
                MoveShoot(LoadProjectile());
            }
            _nextAttackTime = Time.time + DelayPerShot;
        }
    }

    /// <summary>
    /// �����ڵ�
    /// </summary>
    public GameObject LoadProjectile()
    {
        //ʵ�����ڵ�
        GameObject newInstance = Instantiate(go);
        newInstance.transform.localPosition = turrentTrans.position;
        newInstance.transform.SetParent(_turret.transform);
        newInstance.SetActive(true);
        return newInstance;
    }
    private void MoveShoot(GameObject go)
    {
        //Vector3 movement = _turret.direction * moveSpeed * Time.deltaTime;
        //go.transform.position += movement;
    }



}
