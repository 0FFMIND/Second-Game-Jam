using UnityEngine;
[CreateAssetMenu(fileName = "Quest", menuName = "ScriptableObjects/New Quest")]
[System.Serializable]
public class QuestInfo : ScriptableObject
{
    public string questName;
    public string questInfo;
    // 仅在U3D编辑模式中调用，不属于脚本生命周期函数
    private void OnValidate()
    {
#if UNITY_EDITOR
        questName = this.name;
        // 在编辑器模式下进行保存
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }
}
