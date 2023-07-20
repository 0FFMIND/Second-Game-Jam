using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{

    /// 炮弹出生位置
    [SerializeField] protected Transform turrentTrans;
    [SerializeField] protected float delayBtwAttacks = 1f;
    [SerializeField] protected float moveSpeed = 10f;
    /// 炮弹伤害
    [SerializeField] protected float damage = 4f;

    public float Damage { get; set; }
    /// 每次射击的延迟
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
    /// 加载炮弹
    /// </summary>
    public GameObject LoadProjectile()
    {
        //实例化炮弹
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
