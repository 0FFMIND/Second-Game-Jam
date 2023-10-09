using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlaceArea : MonoBehaviour
{
    public bool isOK;

    private void OnMouseEnter()
    {
        isOK = true;
        if (BuildingManager.Instance != null)
        {
            BuildingManager.Instance.InPlaceSpace(isOK);
        }
    }
    private void OnMouseExit()
    {
        isOK = false;
        if (BuildingManager.Instance != null)
        {
            BuildingManager.Instance.InPlaceSpace(isOK);
        }
    }
}
