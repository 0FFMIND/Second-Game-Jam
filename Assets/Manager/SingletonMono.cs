using UnityEngine;

public class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    // �������Ա���˽�б���
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                // ͨ��Resource��Դ�ļ����أ���������Ϸ�ļ������ȥ��ֻ���ļ��У������޸ģ�ֻ�ɶ�ȡ
                string name = typeof(T).ToString();
                GameObject obj = Instantiate(Resources.Load<GameObject>("Prefabs/" + name));
                obj.name = name;
                instance = obj.GetComponent<T>();
                DontDestroyOnLoad(instance);
            }
            return instance;
        }
    }
}