using UnityEngine;
[CreateAssetMenu(fileName = "Quest", menuName = "ScriptableObjects/New Quest")]
[System.Serializable]
public class QuestInfo : ScriptableObject
{
    public string questName;
    public string questInfo;
    [Header("Prerequisite")]
    public GameObject[] requirements;
    [Header("Steps")]
    public GameObject[] steps;
    [Header("Rewards")]
    public GameObject[] rewards;
    // ����U3D�༭ģʽ�е��ã������ڽű��������ں���
    private void OnValidate()
    {
#if UNITY_EDITOR
        questName = this.name;
        // �ڱ༭��ģʽ�½��б���
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }
}
