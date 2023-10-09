using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleCollision : MonoBehaviour
{
    public GameObject sellPanel;
    public Building building;
    private void OnMouseEnter()
    {
        sellPanel.gameObject.SetActive(true);
    }
    private void OnMouseExit()
    {
        sellPanel.gameObject.SetActive(false);
    }
    private void OnMouseDown()
    {
        if (sellPanel.activeSelf)
        {
                BuildingTypeSO buildingType = building.GetComponent<BuildingTypeHolder>().buildingType;
                ResourceAmonut resourceAmonut = buildingType.constructResourceArray;
                ResourceManager.Instance.AddResource(resourceAmonut.resourceType, Mathf.FloorToInt(resourceAmonut.amonut * .1f));
                Destroy(building.gameObject);
        }
    }
}
