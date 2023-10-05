using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingTypeSelectUI : MonoBehaviour
{
    public void Click(int id)
    {
        BuildingTypeListSO buildingTypeList = Resources.Load<BuildingTypeListSO>("BuildingTypeList");
        BuildingManager.Instance.SetActiveBuildingType(buildingTypeList.list[id]);
    }
}
