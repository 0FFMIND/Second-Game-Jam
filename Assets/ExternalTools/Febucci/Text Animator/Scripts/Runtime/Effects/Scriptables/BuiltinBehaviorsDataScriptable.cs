using UnityEngine;

namespace Febucci.UI.Core
{
    /// <summary>
    /// Scriptable Object that contains Behaviors data that can be shared among multiple TextAnimator components.
    /// - Manual: <see href="https://www.febucci.com/text-animator-unity/docs/how-to-add-effects-to-your-texts/#shared-built-in-values">Shared built-in values.</see><br/>
    /// </summary>
    [System.Serializable]
    [CreateAssetMenu(fileName = "Built-in Behaviors values", menuName = "TextAnimator/Create Built-in Behaviors values")]
    public class BuiltinBehaviorsDataScriptable : BuiltinDataScriptableBase<BehaviorDefaultValues.Defaults>
    {

    }
}