using UnityEngine;

namespace Febucci.UI.Core
{
    /// <summary>
    /// Scriptable Object that contains Appearances data that can be shared among multiple TextAnimator components.
    /// - Manual: <see href="https://www.febucci.com/text-animator-unity/docs/how-to-add-effects-to-your-texts/#shared-built-in-values">Shared built-in values.</see><br/>
    /// </summary>
    [System.Serializable]
    [CreateAssetMenu(fileName = "Built-in Appearances values", menuName = "TextAnimator/Create Built-in Appearances values")]
    public class BuiltinAppearancesDataScriptable : BuiltinDataScriptableBase<AppearanceDefaultValues.Defaults>
    {

    }
}