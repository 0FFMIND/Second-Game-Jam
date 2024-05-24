using UnityEngine;
[CreateAssetMenu(fileName = "Quest", menuName = "ScriptableObjects/New Quest")]
[System.Serializable]
public class QuestInfo : ScriptableObject
{
    public string questName;
    public string questInfo;
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
