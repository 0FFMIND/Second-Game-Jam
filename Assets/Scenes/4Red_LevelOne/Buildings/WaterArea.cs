using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterArea : MonoBehaviour
{
    public bool isWater;
    private void OnMouseEnter()
    {
        isWater = true;
        if (BuildingManager.Instance != null)
        {
            BuildingManager.Instance.InWaterSpace(isWater);
        }
    }
    private void OnMouseExit()
    {
        isWater = false;
        if (BuildingManager.Instance != null)
        {
            BuildingManager.Instance.InWaterSpace(isWater);
        }
    }
}
