using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class WaterArea : MonoBehaviour,IPointerEnterHandler
{
    public bool isWater;
    public Collider2D colliderD;
    public void Start()
    {
        colliderD = this.gameObject.GetComponent<Collider2D>();
    }
    private void Update()
    {
        if (BuildingManager.Instance != null)
        {
            BuildingManager.Instance.InWaterSpace(isWater);
        }
    }
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
        isWater = true;
        if (BuildingManager.Instance != null)
        {
            BuildingManager.Instance.InWaterSpace(isWater);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isWater = true;
    }

}
