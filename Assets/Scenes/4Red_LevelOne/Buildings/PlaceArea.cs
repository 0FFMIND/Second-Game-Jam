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
        BuildingManager.Instance.InPlaceSpace(isOK);
    }
    private void OnMouseExit()
    {
        isOK = false;
        BuildingManager.Instance.InPlaceSpace(isOK);
    }
}
