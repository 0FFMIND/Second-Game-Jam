#if PLAYMAKER
namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Level)]
    [Tooltip("Transitions between scenes using the scene name. All you need is a transitioner in the scene and this action will find it. No need to hook it up at all.")]
    public class TransitionBlocksLoadLevel : FsmStateAction
    {
        [RequiredField]
        public FsmString levelName;

        public override void Reset()
        {
            levelName = "";
        }

        public override void OnEnter()
        {
            Transitioner.Instance.TransitionToScene(levelName.Value, true);
        }
    }
}
#endif