using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public interface IEventInfo // 定义接口
{

}
public class EventInfo<T> : IEventInfo
{
    public UnityAction<T> actions;
    public EventInfo(UnityAction<T> action)
    {
        actions += action;
    }
}
public class EventInfo : IEventInfo
{
    public UnityAction actions;
    public EventInfo(UnityAction action)
    {
        actions += action;
    }
}
// 事件中心使用例子
// 在一个函数最后通知这个怪物死亡EventManager.Instance.EventTrigger("MonsterDead",this)
// 在需要监听的脚本中首先加EventManger.Instance.AddEventListener<Monster>("MonsterDead",(o) => MyFunction(o))
// 在死亡的时候就会唤醒监听脚本中的MyFunction(object o){}
public class EventManager : Singleton<EventManager>
{
    // string是事件名字，接口是监听事件的委托函数
    private Dictionary<string, IEventInfo> eventDic = new Dictionary<string, IEventInfo>();
    public void AddEventListener<T>(string name, UnityAction<T> action)
    {
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo<T>).actions += action;
        }
        else
        {
            eventDic.Add(name, new EventInfo<T>(action));
        }
    }
    public void AddEventListener(string name, UnityAction action)
    {
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo).actions += action;
        }
        else
        {
            eventDic.Add(name, new EventInfo(action));
        }
    }
    public void RemoveEventListener<T>(string name, UnityAction<T> action)
    {
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo<T>).actions -= action;
        }
    }
    public void RemoveEventListener(string name, UnityAction action)
    {
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo).actions -= action;
        }
    }
    public void EventTrigger<T>(string name,T info)
    {
        if (eventDic.ContainsKey(name))
        {
            if((eventDic[name] as EventInfo<T>).actions != null)
            {
                (eventDic[name] as EventInfo<T>).actions.Invoke(info);
            }
        }
    }
    public void EventTrigger(string name)
    {
        if (eventDic.ContainsKey(name))
        {
            if((eventDic[name] as EventInfo).actions != null)
            {
                (eventDic[name] as EventInfo).actions.Invoke();
            }
        }
    }
    public void Clear()
    {
        eventDic.Clear();
    }
}
