using UnityEngine;

public class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    // 创建属性保护私有变量
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                // 通过Resource资源文件加载，它是随游戏文件打包出去的只读文件夹，不可修改，只可读取
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