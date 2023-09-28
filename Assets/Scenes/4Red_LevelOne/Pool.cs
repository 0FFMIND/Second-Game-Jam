using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : Singleton<Pool>
{
    private class SinglePool
    {
        public HashSet<GameObject> Active; // 已经显示的
        public Queue<GameObject> Available;// 可用的
        public SinglePool()
        {
            Available = new Queue<GameObject>();
        }
    }
    public int poolObjsNum = 10;
    public GameObject[] PooledObjects; // 这里给对象池的物体都需要打上tag

    private Dictionary<string, GameObject> pooledObjectsDic = new Dictionary<string, GameObject>();
    private Dictionary<string, Queue<GameObject>> pool = new Dictionary<string, Queue<GameObject>>();

    private void Start()
    {
        foreach (var item in PooledObjects)
        {
            RegisterObject(item); // 先注册每个物体，给上默认的10并将它们deactive
        }
    }
    private void RegisterObject(GameObject prototype)
    {
        if (pooledObjectsDic.ContainsKey(prototype.tag)) return;
        var singlePool = new Queue<GameObject>();
        for (int i = 0; i < poolObjsNum; i++)
        {
            var newItem = Instantiate(prototype, transform);
            newItem.SetActive(false);
            singlePool.Enqueue(newItem);
        }
        pooledObjectsDic.Add(prototype.tag, prototype);
        pool.Add(prototype.tag, singlePool);
    }
    //工具方法
    public GameObject ActivateObject(string tag)
    {
        if (!pooledObjectsDic.ContainsKey(tag)) return null;
        var singlePool = pool[tag];
        if(singlePool.Count == 0)
        {
            var newItem = Instantiate(pooledObjectsDic[tag], transform);
            return newItem;
        }
        return pool[tag].Dequeue();
    }
    public void DeactivateObject(GameObject item)
    {
        if (!pooledObjectsDic.ContainsKey(tag)) return;
        item.SetActive(false);
        pool[tag].Enqueue(item);
    }
}
