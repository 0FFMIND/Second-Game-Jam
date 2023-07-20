namespace Febucci.UI.Core
{
    /// <summary>
    /// Base class for all behavior effects.<br/>
    /// Inherit from this class if you want to create your own Behavior Effect in C#.
    /// </summary>
    public abstract class BehaviorBase : EffectsBase
    {
        public abstract void SetDefaultValues(BehaviorDefaultValues data);

        [System.Obsolete("This variable will be removed from next versions. Please use 'time.timeSinceStart' instead")]
        public float animatorTime => time.timeSinceStart;
        [System.Obsolete("This variable will be removed from next versions. Please use 'time.deltaTime' instead")]
        public float animatorDeltaTime => time.deltaTime;
        
        /// <summary>
        /// Contains data/settings from the TextAnimator component that is linked to (and managing) this effect.
        /// </summary>
        public TextAnimator.TimeData time { get; private set; }

        internal void SetAnimatorData(in TextAnimator.TimeData time)
        {
            this.time = time;
        }
        
    }
}