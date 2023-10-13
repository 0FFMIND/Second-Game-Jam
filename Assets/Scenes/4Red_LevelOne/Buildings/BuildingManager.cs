using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { get; private set; }
    private BuildingTypeListSO buildingTypeList;
    public BuildingTypeSO activeBuildingType;
    public event EventHandler<OnActiveBuildingTypeChangedEventArgs> OnActiveBuildingTypeChanged;
    public class OnActiveBuildingTypeChangedEventArgs : EventArgs
    {
        public BuildingTypeSO activeBuildingType;
    }
    private bool canPlace;
    private bool canWaterPlace;
    private void Start()
    {
        Instance = this;
        buildingTypeList = Resources.Load<BuildingTypeListSO>("BuildingTypeList");
    }
    private void Update()
    {

        if (Input.GetMouseButtonDown(0) && canWaterPlace && (activeBuildingType == buildingTypeList.list[3] || activeBuildingType == buildingTypeList.list[9]))
        {
            if (CanSpawnBuilding(activeBuildingType, GetMouseWorldPosition()))
            {
                if (ResourceManager.Instance.CanAfford(activeBuildingType.constructResourceArray))
                {
                    AudioManager.Instance.PlaySFX(SoundEffect.UISelect);
                    ResourceManager.Instance.SpendResource(activeBuildingType.constructResourceArray);
                    Instantiate(activeBuildingType.prefab, GetMouseWorldPosition(), Quaternion.identity);
                }
            }


        }
        if (Input.GetMouseButtonDown(0) && canPlace && (activeBuildingType != buildingTypeList.list[3] && activeBuildingType != buildingTypeList.list[9]))
        {
            if (activeBuildingType != null && CanSpawnBuilding(activeBuildingType, GetMouseWorldPosition()))
            {
                if (ResourceManager.Instance.CanAfford(activeBuildingType.constructResourceArray))
                {
                    ResourceManager.Instance.SpendResource(activeBuildingType.constructResourceArray);
                    if(activeBuildingType == buildingTypeList.list[5])
                    {
                        if(GameObject.FindGameObjectWithTag("defenseholder") == null && GameObject.FindGameObjectsWithTag("defenseholder").Length == 0)
                        {
                            AudioManager.Instance.PlaySFX(SoundEffect.UISelect);
                            Instantiate(activeBuildingType.prefab, GetMouseWorldPosition(), Quaternion.identity);
                            return;
                        }
                        else
                        {
                            return;
                        }
                    }
                    if(activeBuildingType == buildingTypeList.list[6])
                    {
                        if (GameObject.FindGameObjectWithTag("shootcenter") == null && GameObject.FindGameObjectsWithTag("shootcenter").Length == 0)
                        {
                            AudioManager.Instance.PlaySFX(SoundEffect.UISelect);
                            Instantiate(activeBuildingType.prefab, GetMouseWorldPosition(), Quaternion.identity);
                            return;
                        }
                        else
                        {
                            return;
                        }
                    }
                    AudioManager.Instance.PlaySFX(SoundEffect.UISelect);
                    Instantiate(activeBuildingType.prefab, GetMouseWorldPosition(), Quaternion.identity);
                    return;
                }
            }
        }
    }
    private Vector3 GetMouseWorldPosition()
    {
        Vector3 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return new Vector3(cursorPos.x, cursorPos.y, 1f);
    }
    public void SetActiveBuildingType(BuildingTypeSO buildingType)
    {
        activeBuildingType = buildingType;
        OnActiveBuildingTypeChanged?.Invoke(this, new OnActiveBuildingTypeChangedEventArgs { activeBuildingType = buildingType});
    }

    public void InPlaceSpace(bool _isOK)
    {
        canPlace = _isOK;
    }
    public void InWaterSpace(bool _isWater)
    {
        canWaterPlace = _isWater;
    }
    private bool CanSpawnBuilding(BuildingTypeSO buildingType, Vector3 position)
    {
        List<Collider2D> collider2DList = new List<Collider2D>();
        CircleCollider2D circleCollider2D = buildingType.prefab.GetComponent<CircleCollider2D>();
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(position + (Vector3)circleCollider2D.offset, circleCollider2D.radius * 4);
        foreach (var collider in collider2DArray)
        {    
            if(collider.tag != "PLACE" && collider.tag != "water" && collider.tag != "ignore" && collider.tag != "water2")
            {
                collider2DList.Add(collider);
            }
        }
        if(collider2DList.Count != 0) return false;
        foreach (var collider in collider2DList)
        {
            BuildingTypeHolder buildingTypeHolder = collider.GetComponent<BuildingTypeHolder>();
            if(buildingTypeHolder != null)
            {
                if(buildingTypeHolder.buildingType == buildingType)
                {
                    return false;
                }
            }
        }
        return true;
    }
}
