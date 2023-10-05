using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(menuName = "BuildingType")]
public class BuildingTypeSO : ScriptableObject
{
    public string nameString;
    public Transform prefab;
    public ResourceGeneratorData resourceGeneratorData;
}
