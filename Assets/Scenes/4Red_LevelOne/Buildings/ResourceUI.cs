using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ResourceUI : MonoBehaviour
{
    private ResourceTypeListSO resourceTypeList;
    private Dictionary<ResourceTypeSO, TextMeshProUGUI> resourceTypeTMPDictionary;
    private void Awake()
    {
        resourceTypeList = Resources.Load<ResourceTypeListSO>("ResourceTypeList");
        resourceTypeTMPDictionary = new Dictionary<ResourceTypeSO, TextMeshProUGUI>();
    }
    private void Start()
    {
        ResourceManager.Instance.OnResourceAmountChanged += ResourceManager_OnResourceAmountChanged;
        UpdateResourceAmount();
    }
    private void Update()
    {
        UpdateResourceAmount();
    }

    private void ResourceManager_OnResourceAmountChanged(object sender, System.EventArgs e)
    {
        UpdateResourceAmount();
    }

    private void UpdateResourceAmount()
    {
        foreach (ResourceTypeSO resourceType in resourceTypeList.list)
        {
            if(resourceType.name == "Power")
            {
                int resourceAmount = ResourceManager.Instance.GetResourceAmount(resourceType);
                GameObject powerUGUI = GameObject.FindWithTag("POWER");
                if (!resourceTypeTMPDictionary.ContainsKey(resourceType))
                {
                    resourceTypeTMPDictionary.Add(resourceType, powerUGUI.GetComponent<TextMeshProUGUI>());
                }
                resourceTypeTMPDictionary[resourceType].text = resourceAmount.ToString();
            }
        }
    }
}
