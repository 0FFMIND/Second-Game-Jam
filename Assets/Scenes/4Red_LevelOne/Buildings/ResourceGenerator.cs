using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    private BuildingTypeSO buildingType;
    private float timer = 0;
    private float timerMax;
    private void Awake()
    {
        buildingType = GetComponent<BuildingTypeHolder>().buildingType;
        timerMax = buildingType.resourceGeneratorData.timerMax;
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > timerMax)
        {
            timer = 0;
            if(buildingType.resourceGeneratorData.resourceType.name == "Power")
            {

                ResourceManager.Instance.AddResource(buildingType.resourceGeneratorData.resourceType, 20);
            }
        }
    }
}
