using UnityEngine;
// ������ֻ����������(����)�б�ʵ����
public abstract class QuestStep : MonoBehaviour
{
    // private��Ա�ڼ̳����ǲ��ɼ��ģ�������(����)�����Է��ʻ����޸Ĵ����Ա
    private bool isFinished = false;
    // protected��ζ�Ÿó�Ա���Ա�������(����)�޸������
    protected void FinishedQuestStep()
    {
        if(isFinished == false)
        {
            isFinished = true;
            // TODO
            Destroy(this.gameObject);
        }
    }
}
