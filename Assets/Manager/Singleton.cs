using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T m_Instance;
    public static T Instance
    {
        get
        {
            m_Instance = (T)FindObjectOfType(typeof(T));
            return m_Instance;
        }
    }
}
