using UnityEngine;
// 抽象类只能在派生类(子类)中被实例化
public abstract class QuestStep : MonoBehaviour
{
    // private成员在继承类是不可见的，派生类(子类)不可以访问或者修改此类成员
    private bool isFinished = false;
    // protected意味着该成员可以被派生类(子类)修改与访问
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
