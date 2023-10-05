using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }
    private Dictionary<ResourceTypeSO, int> resourceAmountDictionary;
    // �������������¼�����1��60֡���¼������Ż���������������ĸ�����Ǵ�д��
    public event EventHandler OnResourceAmountChanged;
    private void Awake()
    {
        Instance = this;
        resourceAmountDictionary = new Dictionary<ResourceTypeSO, int>();
        ResourceTypeListSO resourceTypeList = Resources.Load<ResourceTypeListSO>("ResourceTypeList");
        foreach (ResourceTypeSO resourceType in resourceTypeList.list)
        {
            // ��Ϊֻ��power��һ��
            resourceAmountDictionary[resourceType] = 40;
        }
    }
    public void AddResource(ResourceTypeSO resourceType, int amount)
    {
        OnResourceAmountChanged?.Invoke(this, EventArgs.Empty);
        resourceAmountDictionary[resourceType] += amount;
        if(resourceAmountDictionary[resourceType] >= 800)
        {
            resourceAmountDictionary[resourceType] = 800;
        }
    }
    public int GetResourceAmount(ResourceTypeSO resourceType)
    {
        return resourceAmountDictionary[resourceType];
    }
}
