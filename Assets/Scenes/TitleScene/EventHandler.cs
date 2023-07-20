using System;

public static class EventHandler
{
    //暂停游戏调用方法
    public static event Action OnGameStop;
    public static void CallOnGameStop()
    {
        OnGameStop?.Invoke();
    }
}
