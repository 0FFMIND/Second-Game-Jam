using System;

public static class EventHandler
{
    //��ͣ��Ϸ���÷���
    public static event Action OnGameStop;
    public static void CallOnGameStop()
    {
        OnGameStop?.Invoke();
    }
}
