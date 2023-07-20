namespace Febucci.UI.Core
{
    /// <summary>
    /// Base class for TextAnimator effects' categories<br/>
    /// Please do not inherit from this class directly, but do from <see cref="AppearanceBase"/> or <see cref="BehaviorBase"/>
    /// </summary>
    public abstract class EffectsBase
    {
        /// <summary>
        /// Effect's tag without symbols, eg. "shake"
        /// </summary>
        public string effectTag { get; private set; }


        /// <summary>
        /// Intensity used to uniform effects that behave differently based on screen or font sizes.
        /// </summary>
        /// <remarks>
        /// Multiply this by your effect values only if they behave differently with different screen resolutions, font sizes or similar. (e.g. adding or subtracting vectors)
        /// </remarks>
        public float uniformIntensity = 1;
        
        [System.Obsolete("This value will be removed from next versions. Please use 'uniformIntensity' instead")]  public float effectIntensity => uniformIntensity;

        #region Internal/Core
        //---Methods used only by TextAnimator's internal workflow---//

        internal class RegionManager
        {
            public string entireRichTextTag;
            System.Collections.Generic.List<TextRegion> textRegions = new System.Collections.Generic.List<TextRegion>();

            struct TextRegion
            {
                public int startIndex;
                public int endIndex;

                public TextRegion(int startIndex)
                {
                    this.startIndex = startIndex;
                    this.endIndex = int.MaxValue;
                }
            }

            internal bool IsLastRegionClosed()
            {
                return textRegions.Count > 0 && textRegions[textRegions.Count - 1].endIndex != int.MaxValue;
            }

            internal void AddRegion(int startIndex)
            {
                textRegions.Add(new TextRegion(startIndex));
            }

            internal bool TryReutilizingWithTag(string richTextTag, int indexNewRegionStart)
            {
                if (!entireRichTextTag.Equals(richTextTag))
                    return false;

                if (!IsLastRegionClosed())
                    return true;

                AddRegion(indexNewRegionStart);
                return true;
            }


            internal void CloseEffect(int index)
            {
                var region = textRegions[textRegions.Count - 1];
                region.endIndex = index;
                textRegions[textRegions.Count - 1] = region;
            }

            internal bool IsCharInsideRegion(int charIndex)
            {
                foreach (var region in textRegions)
                {
                    if (charIndex >= region.startIndex && charIndex < region.endIndex)
                        return true;
                }

                return false;
            }

            public override string ToString()
            {
                string text = $"{entireRichTextTag} - {textRegions.Count} region(s): ";
                for (int i = 0; i < textRegions.Count; i++)
                {
                    if(textRegions[i].endIndex == int.MaxValue)
                    {
                        text += $"[{textRegions[i].startIndex}; Infinity] ";
                    }
                    else
                    {
                        text += $"[{textRegions[i].startIndex}; {textRegions[i].endIndex}] ";
                    }
                }

                return text;
            }
        }

        internal RegionManager regionManager;

        /// <summary>
        /// For internal use only. Sets the effect settings such as tags, instead of a constructor.
        /// </summary>
        /// <param name="effectTag"></param>
        internal void _Initialize(string effectTag, string entireRichTextTag)
        {
            this.effectTag = effectTag;
            this.regionManager = new RegionManager();
            this.regionManager.entireRichTextTag = entireRichTextTag;
        }
        #endregion

        #region Utilities
        //---Methods you can use in your classes---//

        /// <summary>
        /// Applies the modifier by performing a multiplication to the given value.
        /// </summary>
        /// <param name="value">The effect's value you want to modify</param>
        /// <param name="modifierValue">The modifier value. eg. "0.5"</param>
        /// <example>
        /// <code>
        /// string modifier = "0.5";
        /// float amplitude = 1;
        /// ApplyModifierTo(ref amplitude, modifier);
        /// //amplitude becomes 0.5
        /// </code>
        /// </example>
        protected void ApplyModifierTo(ref float value, string modifierValue)
        {
            if (FormatUtils.ParseFloat(modifierValue, out float multiplier))
            {
                value *= multiplier;
            }
        }
        #endregion

        #region Effect Methods
        //---Methods you can override in your classes---//

        /// <summary>
        /// Invoked upon effect creation
        /// </summary>
        /// <param name="charactersCount"></param>
        public virtual void Initialize(int charactersCount) { }

        /// <summary>
        /// Called once per frame, before applying the effect to letters.
        /// Example: You could use this to calculate the effect variables that are indiependant from specific letters
        /// </summary>
        public virtual void Calculate() { }

        /// <summary>
        /// Called once for each letter, per each frame.<br/>
        /// Use this to apply the effect to a letter/character, by modifying its <see cref="CharacterData"/> values.
        /// </summary>
        /// <param name="data">Letters' values like position and colors. It might have been already modified by previous effects.</param>
        /// <param name="charIndex">Letter index/position in the text.</param>
        public abstract void ApplyEffect(ref CharacterData data, int charIndex);


        /// <summary>
        /// Invoked when there is a modifier in your rich text tag, eg. &#60;shake a=3&#62;
        /// </summary>
        /// <remarks>You can also use the following helper methods:
        /// - <see cref="EffectsBase.ApplyModifierTo"/>
        /// - <see cref="FormatUtils.ParseFloat"/>
        /// </remarks>
        /// <param name="modifierName">modifier name. eg. in &#60;shake a=3&#62; this string is "a"</param>
        /// <param name="modifierValue">modifier value. eg. in &#60;shake a=3&#62; this string is "3"</param>
        /// <example>
        /// <code>
        /// float amplitude = 2;
        /// //[...]
        /// public override void SetModifier(string modifierName, string modifierValue){
        ///     switch(modifierName){
        ///         //changes the 'amplitude' variable based on the modifier written in the tag
        ///         //eg. when you write a tag like &#60;shake a=3&#62;
        ///         case "a": ApplyModifierTo(ref amplitude, modifierValue); return;
        ///     }
        /// }
        /// </code>
        /// </example>
        public abstract void SetModifier(string modifierName, string modifierValue);


#if UNITY_EDITOR
        //Used only in the editor to set again modifiers if we change values in the inspector

        System.Collections.Generic.List<Modifier> modifiers { get; set; } = new System.Collections.Generic.List<Modifier>();

        internal void EDITOR_RecordModifier(string name, string value)
        {
            modifiers.Add(new Modifier
            {
                name = name,
                value = value,
            });
        }

        internal void EDITOR_ApplyModifiers()
        {
            for (int i = 0; i < modifiers.Count; i++)
            {
                SetModifier(modifiers[i].name, modifiers[i].value);
            }
        }
#endif
        #endregion
    }
}