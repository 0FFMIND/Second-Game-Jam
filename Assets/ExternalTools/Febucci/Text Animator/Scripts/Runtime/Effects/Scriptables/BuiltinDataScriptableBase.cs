using UnityEngine;

namespace Febucci.UI.Core
{
    public class BuiltinDataScriptableBase<T> : ScriptableObject where T : new()
    {
        [SerializeField] public T effectValues = new T();
    }
}