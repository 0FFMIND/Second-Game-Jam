using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingDemolish : MonoBehaviour
{
    [SerializeField] private Building building;
    public void Destroy()
    {
        BuildingTypeSO buildingType = building.GetComponent<BuildingTypeHolder>().buildingType;
        ResourceAmonut resourceAmonut = buildingType.constructResourceArray;
        ResourceManager.Instance.AddResource(resourceAmonut.resourceType, Mathf.FloorToInt(resourceAmonut.amonut * .1f));
        Destroy(building.gameObject);
    }
}
