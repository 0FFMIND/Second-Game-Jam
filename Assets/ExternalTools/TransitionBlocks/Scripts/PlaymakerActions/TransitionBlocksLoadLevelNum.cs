#if PLAYMAKER
namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Level)]
    [Tooltip("Transitions between scenes using the scene number. All you need is a transitioner in the scene and this action will find it. No need to hook it up at all.")]
    public class TransitionBlocksLoadLevelNum : FsmStateAction
    {
        [RequiredField]
        public FsmInt levelNumber;

        public override void Reset()
        {
            levelNumber = 0;
        }

        public override void OnEnter()
        {
            Transitioner.Instance.TransitionToScene(levelNumber.Value, true);
        }
    }
}
#endif