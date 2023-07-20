#if UNITY_EDITOR
#define CHECK_ERRORS //used to check text errors 
#endif

#if TA_Naninovel
#define INTEGRATE_NANINOVEL
#endif

using Febucci.UI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

namespace Febucci.UI
{
    /// <summary>
    /// The main TextAnimator component. Add this near to a TextMeshPro component in order to enable effects. It can also be used in combination with a TextAnimatorPlayer in order to display letters dynamically (like a typewriter).<br/>
    /// - See also: <seealso cref="TextAnimatorPlayer"/><br/>
    /// - Manual: <see href="https://www.febucci.com/text-animator-unity/docs/how-to-add-effects-to-your-texts/">How to add effects to your texts</see><br/>
    /// </summary>
    [HelpURL("https://www.febucci.com/text-animator-unity/docs/how-to-add-effects-to-your-texts/")]
    [AddComponentMenu("Febucci/TextAnimator/TextAnimator")]
    [RequireComponent(typeof(TMP_Text)), DisallowMultipleComponent]
    public class TextAnimator : MonoBehaviour
    {

        #region Types (Structs + Enums)

        /// <summary>
        /// Contains TextAnimator's current time values.
        /// </summary>
        [System.Serializable]
        public struct TimeData
        {
            /// <summary>
            /// Time passed since the textAnimator started showing the very first letter
            /// </summary>
            public float timeSinceStart { get; private set; }

            /// <summary>
            /// TextAnimator's Component delta time, could be Scaled or Unscaled
            /// </summary>
            public float deltaTime { get; private set; }

            internal void ResetData()
            {
                timeSinceStart = 0;
            }

            internal void IncreaseTime()
            {
                timeSinceStart += deltaTime;
            }

            internal void UpdateDeltaTime(TimeScale timeScale)
            {
                deltaTime = timeScale == TimeScale.Unscaled ? Time.unscaledDeltaTime : Time.deltaTime;

                //To avoid possible desync errors etc., effects can't be played backwards. 
                if (deltaTime < 0)
                    deltaTime = 0;
            }
        }

        [System.Serializable]
        class AppearancesContainer
        {
            [SerializeField, UnityEngine.Serialization.FormerlySerializedAs("tags")]
            public string[] tagsFallback_Appearances = new string[] { TAnimTags.ap_Size };  //starts with a size effect by default

            public string[] tagsFallback_Disappearances = new string[] { TAnimTags.ap_Size };  //starts with a size effect by default

            public AppearanceDefaultValues values = new AppearanceDefaultValues();
        }

        internal struct InternalAction
        {
            public TypewriterAction action;

            public int charIndex;
            public bool triggered;
            public int internalOrder;
        }


        enum ShowTextMode : byte
        {
            Hidden = 0,
            Shown = 1,
            UserTyping = 2
        }

        /// <summary>
        /// TextAnimator's effects time scale, which could match unity's Time.deltaTime or Time.unscaledDeltaTime
        /// </summary>
        public enum TimeScale
        {
            Scaled,
            Unscaled,
        }

        #endregion

        private void Awake()
        {
            Canvas[] canvases = new Canvas[0];
            canvases = gameObject.GetComponentsInParent<Canvas>(true);

            //-----
            //TMPro UI references a canvas, but if it's null [in its case, the object is inactive] it doesn't generate the mesh and it throws error(s).
            //These variables manages a canvas also if its' disabled.
            //-----

            if (canvases.Length > 0)
            {
                parentCanvas = canvases[0];
                hasParentCanvas = parentCanvas != null;
            }

#if INTEGRATE_NANINOVEL
            reveablelText = GetComponent<Naninovel.UI.IRevealableText>();
            isNaninovelPresent = reveablelText != null;
#endif

            //If we're checking text from TMPro, prevents its very first set text to appear for one frame and then disappear
            if (triggerAnimPlayerOnChange)
            {
                tmproText.renderMode = TextRenderFlags.DontRender;
            }

            m_time.UpdateDeltaTime(timeScale);
        }

        #region Variables

        TAnimPlayerBase _tAnimPlayer;
        /// <summary>
        /// Linked TAnimPlayer to this component
        /// </summary>
        TAnimPlayerBase tAnimPlayer
        {
            get
            {
                if (_tAnimPlayer != null)
                    return _tAnimPlayer;

#if UNITY_2019_2_OR_NEWER
                if(!TryGetComponent(out _tAnimPlayer))
                {
                    Debug.LogError($"Text Animator component is null on GameObject {gameObject.name}");
                }
#else
                _tAnimPlayer = GetComponent<TAnimPlayerBase>();
                Assert.IsNotNull(_tAnimPlayer, $"Text Animator Player component is null on GameObject {gameObject.name}");
#endif

                return _tAnimPlayer;
            }
        }

        #region Inspector

        [SerializeField, Tooltip("If true, the typewriter is triggered automatically once the TMPro text changes (requires a TextAnimatorPlayer component). Otherwise, it shows the entire text instantly.")]
        bool triggerAnimPlayerOnChange = false;

        [SerializeField]
        float effectIntensityMultiplier = 50;

        [UnityEngine.Serialization.FormerlySerializedAs("defaultAppearance"), SerializeField, Header("Text Appearance")]
        AppearancesContainer appearancesContainer = new AppearancesContainer();

        [SerializeField] string[] tags_fallbackBehaviors = new string[0];
        [SerializeField] BehaviorDefaultValues behaviorValues = new BehaviorDefaultValues();

        //Global effect values
#pragma warning disable 0649
        [SerializeField] BuiltinBehaviorsDataScriptable scriptable_globalBehaviorsValues;
        [SerializeField] BuiltinAppearancesDataScriptable scriptable_globalAppearancesValues;
#pragma warning restore 0649

        [SerializeField, Tooltip("True if you want effects to have the same intensities even if text is larger/smaller than default (example: when TMPro's AutoSize changes the size based on screen size)")]
        bool useDynamicScaling = false;
        [SerializeField, Tooltip("Used for scaling, represents the text's size where/when effects intensity behave like intended.")]
        float referenceFontSize = -1;

        #endregion

        #region Public Variables

        TMP_Text _tmproText;

        /// <summary>
        /// The TextMeshPro component linked to this TextAnimator
        /// </summary>
        public TMP_Text tmproText
        {
            get
            {
                if (_tmproText != null)
                    return _tmproText;

#if UNITY_2019_2_OR_NEWER
                if(!TryGetComponent(out _tmproText))
                {
                    Debug.LogError("TextAnimator: TMproText component is null.");
                }
#else
                _tmproText = GetComponent<TMP_Text>();
                Assert.IsNotNull(tmproText, $"TextMeshPro component is null on Object {gameObject.name}");
#endif

                return _tmproText;
            }

            private set
            {
                _tmproText = value;
            }
        }

        #region Time

        /// <summary>
        /// Effects timescale, you can set it to scaled or unscaled.
        /// It also affects the TextAnimatorPlayer, if there is one linked to this TextAnimator.
        /// </summary>
        public TimeScale timeScale = TimeScale.Scaled;

        #endregion

        #region Events
        /// <summary>
        /// Delegate used for TextAnimator's events. Listeners can subscribe to: <see cref="onEvent"/>. <br/>
        /// - Manual: <see href="https://www.febucci.com/text-animator-unity/docs/triggering-events-while-typing/">Triggering Events while typing</see>
        /// </summary>
        /// <param name="message"></param>
        public delegate void MessageEvent(string message);

        /// <summary>
        /// Invoked by the typewriter once it reaches a message tag while showing letters.<br/>
        /// - Manual: <see href="https://www.febucci.com/text-animator-unity/docs/triggering-events-while-typing/">Triggering Events while typing</see>
        /// </summary>
        public event MessageEvent onEvent;
        #endregion

        string latestText;
        /// <summary>
        /// The text stored in the TextAnimator component, without TextAnimator's tags.
        /// </summary>
        public string text { get => latestText; private set => latestText = value; }

        /// <summary>
        /// <c>true</c> if the text is entirely visible.
        /// </summary>
        /// <remarks>
        /// You can use this to check if all the letters have been shown.
        /// </remarks>
        public bool allLettersShown => _maxVisibleCharacters >= tmproText.textInfo.characterCount;

        /// <summary>
        /// The latest TextMeshPro character shown by the typewriter.
        /// </summary>
        public TMP_CharacterInfo latestCharacterShown { get; private set; } //TODO rename in "latestCharacterVisible" for better clarity, since users can now "decrease" the max visible character visible as well

        #endregion

        #region Managament variables

        /// <summary>
        /// Contains TextAnimator's current time values.
        /// </summary>
        public TimeData time => m_time;
        TimeData m_time;

#if INTEGRATE_NANINOVEL //Naninovel integration
        bool isNaninovelPresent;
        Naninovel.UI.IRevealableText reveablelText;
#endif

        bool forceMeshRefresh;
        bool skipAppearanceEffects;

        //----- TMPro workaround -----
        bool hasParentCanvas;
        Canvas parentCanvas;
        //-----

        //----- TMPro values cache -----
        bool autoSize;
        Rect sourceRect;
        Color sourceColor;
        //-----
        int _maxVisibleCharacters = 0;
        public int maxVisibleCharacters
        {
            get => _maxVisibleCharacters;
            set
            {
                if (_maxVisibleCharacters == value) return;

                _maxVisibleCharacters = value;

                //clamps value
                if (_maxVisibleCharacters < 0)
                    _maxVisibleCharacters = 0;

                //stores the latest character visible
                if (hasText && _maxVisibleCharacters <= textInfo.characterCount && _maxVisibleCharacters > 0)
                {
                    latestCharacterShown = textInfo.characterInfo[_maxVisibleCharacters - 1];
                }

                AssertCharacterTimes();
            }
        }

        bool IsCharacterVisible(int i)
        {
            return i <= textInfo.characterCount
                && i >= _firstVisibleCharacter
                && i < _maxVisibleCharacters;
        }

        void AssertCharacterTimes()
        {
            for (int i = 0; i < characters.Length; i++)
            {
                //P.S. Do not change characters' passed time here, since this method might be called by the user while the Update is already applying effects, and it could cause some glitches for that frame

                characters[i].wantsToDisappear = !IsCharacterVisible(i);
            }
        }

        int _firstVisibleCharacter;
        public int firstVisibleCharacter
        {
            get => _firstVisibleCharacter;
            set
            {
                if (_firstVisibleCharacter == value) return;

                _firstVisibleCharacter = value;
                AssertCharacterTimes();
            }
        }

        bool hasText = false;
        internal bool hasActions { get; private set; }


        int latestTriggeredEvent = 0;
        int latestTriggeredAction = 0;

        #endregion

        #region Text Elements

        TMP_TextInfo textInfo;

        Character[] characters = new Character[0];


        List<BehaviorBase> behaviorEffects = new List<BehaviorBase>();
        List<AppearanceBase> appearanceEffects = new List<AppearanceBase>();
        List<AppearanceBase> disappearanceEffects = new List<AppearanceBase>();
        AppearanceBase[] fallbackAppearanceEffects;
        AppearanceBase[] fallbackDisappearanceEffects;
        BehaviorBase[] fallbackBehaviorEffects;

        List<InternalAction> typewriterActions = new List<InternalAction>();
        List<EventMarker> eventMarkers = new List<EventMarker>();

        #endregion

        #endregion

        #region Public Component Methods

        #region For setting the Text
        /// <summary>
        /// Method to set the TextAnimator's text and apply its tags (effects/actions/tmpro/...).
        /// </summary>
        /// <param name="text">Source text, including rich text tags</param>
        /// <param name="hideText"><c>true</c> = sets the text but hides it (visible characters = 0). Mostly used to let the typewriter show letters after setting the text</param>
        public void SetText(string text, bool hideText)
        {
            _SetText(text, hideText ? ShowTextMode.Hidden : ShowTextMode.Shown);
        }

        /// <summary>
        /// Appends the given text to the already existing TMPro's one, applying its tags etc.
        /// </summary>
        /// <param name="text">Text to append, including rich text tags</param>
        /// <param name="hideText"><c>true</c> = appends the text but hides it. Mostly used to let the typewriter show the remaining letters.</param>
        /// <remarks>
        /// If you're using the typewriter, you must manually start it from the code (after appending the text). See: <see cref="TAnimPlayerBase.StartShowingText(bool)"/>
        /// </remarks>
        public void AppendText(string text, bool hideText)
        {
            //Prevents appending an empty text
            if (string.IsNullOrEmpty(text))
                return;

            //The user is appending to an empty text
            //so we set it instead
            if (!hasText)
            {
                SetText(text, hideText);
                return;
            }

            _ApplyTextToCharacters(this.text + _FormatText(text, this.text.Length));

#if TA_DEBUG
            DebugText();
#endif
        }
        #endregion

        #region For the typewriter
        /// <summary>
        /// Tries to return the next character in the text.
        /// </summary>
        /// <example>
        /// <code>
        /// if (textAnimatorComponent.TryGetNextCharacter(out TMP_CharacterInfo nextChar))
        /// {
        ///     ///[...]
        /// }
        /// </code>
        /// </example>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool TryGetNextCharacter(out TMP_CharacterInfo result)
        {
            if (_maxVisibleCharacters < textInfo.characterCount)
            {
                result = textInfo.characterInfo[_maxVisibleCharacters];
                return true;
            }

            result = default;
            return false;
        }

        [System.Obsolete("Please use 'maxVisibleCharacter++' instead.")]
        public char IncreaseVisibleChars()
        {
            maxVisibleCharacters++;
            return latestCharacterShown.character;
        }

        /// <summary>
        /// Turns all characters visible at the end of the frame (i.e. "a typewriter skip")
        /// </summary>
        /// <param name="skipAppearanceEffects">Set this to true if you want all letters to appear instantly (without any appearance effect)</param>
        public void ShowAllCharacters(bool skipAppearanceEffects)
        {
            maxVisibleCharacters = textInfo.characterCount;
            this.skipAppearanceEffects = skipAppearanceEffects;
        }

        /// <summary>
        /// Triggers all the remaining TextAnimator's events.
        /// </summary>
        public void TriggerRemainingEvents()
        {
            if (eventMarkers.Count <= 0)
                return;

            for (int i = latestTriggeredEvent; i < eventMarkers.Count; i++)
            {
                if (!eventMarkers[i].triggered)
                {
                    var _event = eventMarkers[i];
                    _event.triggered = true;
                    onEvent?.Invoke(eventMarkers[i].eventMessage);
                }
            }

            latestTriggeredEvent = eventMarkers.Count - 1;
        }
        #endregion

        /// <summary>
        /// Forces refreshing the mesh at the end of the frame
        /// </summary>
        public void ForceMeshRefresh()
        {
            forceMeshRefresh = true;
        }

        /// <summary>
        /// Triggers events that are currently visible
        /// </summary>
        public void TriggerVisibleEvents()
        {
            TryTriggeringEvent(int.MaxValue); //Invokes all events that are after the current letter (but on the same TMPro index)
        }

        /// <summary>
        /// Resets the effects time, making them start from the start.
        /// </summary>
        /// <remarks>P.S. If you want to restart the typewriter, see <see cref="TAnimPlayerBase.StartShowingText(bool)"/> </remarks>
        /// <param name="skipAppearances"><code>true</code> if you want the characters to reset mostly their behavior effects, staying on screen. <code>false</code> if you want all characters to disappear and play back from their appearance (all together).</param>
        public void ResetEffectsTime(bool skipAppearances)
        {
            if (skipAppearances)
            {
                for (int i = firstVisibleCharacter; i < maxVisibleCharacters; i++)
                {
                    characters[i].isDisappearing = false;
                    characters[i].data.passedTime = characters[i].appearancesMaxDuration;
                }
            }
            else
            {
                for (int i = firstVisibleCharacter; i < maxVisibleCharacters; i++)
                {
                    characters[i].isDisappearing = false;
                    characters[i].data.passedTime = 0;
                }
            }

            //resets text animator's internal time
            m_time.ResetData();

        }

        #endregion

        #region Public Static Methods

        /// <summary>
        /// <c>true</c> if behavior effects are enabled globally (in all TextAnimators).
        /// </summary>
        /// <remarks>
        /// To modify this value, invoke: <see cref="EnableBehaviors(bool)"/>
        /// </remarks>
        public static bool effectsBehaviorsEnabled => enabled_globalBehaviors;

        /// <summary>
        /// <c>true</c> if appearance effects are enabled globally (in all TextAnimators).
        /// </summary>
        /// <remarks>
        /// To modify this value, invoke: <see cref="EnableAppearances(bool)(bool)"/>
        /// </remarks>
        public static bool effectsAppearancesEnabled => enabled_globalAppearances;

        static bool enabled_globalAppearances = true;
        static bool enabled_globalBehaviors = true;

        /// <summary>
        /// Enables/Disables all effects for all TextAnimators.
        /// </summary>
        public static void EnableAllEffects(bool enabled)
        {
            EnableAppearances(enabled);
            EnableBehaviors(enabled);
        }

        /// <summary>
        /// Enables/Disables Appearances effects globally (for all TextAnimators)
        /// </summary>
        /// <param name="enabled"></param>
        /// /// <remarks>To check if behaviors are enabled, refer to <see cref="effectsAppearancesEnabled"/></remarks>
        public static void EnableAppearances(bool enabled)
        {
            enabled_globalAppearances = enabled;
        }

        /// <summary>
        /// Enables/Disables Behavior effects globally (for all TextAnimators)
        /// </summary>
        /// <param name="enabled"></param>
        /// <remarks>To check if behaviors are enabled, refer to <see cref="effectsBehaviorsEnabled"/></remarks>
        public static void EnableBehaviors(bool enabled)
        {
            enabled_globalBehaviors = enabled;
        }


        bool enabled_localBehaviors = true;
        bool enabled_localAppearances = true;


        /// <summary>
        /// Enables/disables Behavior effects for this specific TextAnimator component.
        /// </summary>
        /// <remarks>
        /// To disable effects on all TextAnimators, please see <see cref="EnableAppearances(bool)(bool)"></see>
        /// </remarks>
        /// <param name="value"></param>
        public void EnableBehaviorsLocally(bool value)
        {
            enabled_localBehaviors = value;
        }

        /// <summary>
        /// Enables/disables Appearance effects for this specific TextAnimator component.
        /// </summary>
        /// <remarks>
        /// To disable effects on all TextAnimators, please see <see cref="EnableAppearances(bool)(bool)"></see>
        /// </remarks>
        /// <param name="value"></param>
        public void EnableAppearancesLocally(bool value)
        {
            enabled_localAppearances = value;
        }

        #endregion

        #region Effects Database
        bool databaseBuilt = false;
        Dictionary<string, Type> localBehaviors = new Dictionary<string, Type>();
        Dictionary<string, Type> localAppearances = new Dictionary<string, Type>();

        void BuildTagsDatabase()
        {

            if (databaseBuilt)
                return;

            TAnimBuilder.InitializeGlobalDatabase();

            databaseBuilt = true;

            #region Global built-in effects values

            //replaces local appearances data with global scriptable data
            if (scriptable_globalAppearancesValues)
            {
                appearancesContainer.values.defaults = scriptable_globalAppearancesValues.effectValues;
            }

            //replaces local behavior data with global scriptable data
            if (scriptable_globalBehaviorsValues)
            {
                behaviorValues.defaults = scriptable_globalBehaviorsValues.effectValues;
            }
            #endregion

            //adds local behavior presets
            for (int i = 0; i < behaviorValues.presets.Length; i++)
            {
                TAnimBuilder.TryAddingPresetToDictionary(ref localBehaviors, behaviorValues.presets[i].effectTag, typeof(PresetBehavior));
            }

            //Adds local appearance presets
            for (int i = 0; i < appearancesContainer.values.presets.Length; i++)
            {
                TAnimBuilder.TryAddingPresetToDictionary(ref localAppearances, appearancesContainer.values.presets[i].effectTag, typeof(PresetAppearance));
            }


            #region Fallback appearing effects

            //TODO make a generic method for both

            AppearanceBase[] GetFallbackAppearancesFromTag(string[] tagsToConvert)
            {
                var temp_fallbackEffects = new List<AppearanceBase>();

                //Default effects
                for (int i = 0; i < tagsToConvert.Length; i++)
                {
                    if (tagsToConvert[i].Length <= 0) continue; //tag is empty

                    var tags = tagsToConvert[i].Split(' ');
                    string actualEffect = tags[0]; //removes probable modifiers

                    foreach(var effect in temp_fallbackEffects)
                    {
                        if (effect.regionManager.entireRichTextTag.Equals(tagsToConvert[i])) continue; //same effect has already been added
                    }

                    if (TryGetAppearingClassFromTag(actualEffect, tagsToConvert[i], 0, out AppearanceBase effectBase))
                    {
                        effectBase.SetDefaultValues(appearancesContainer.values);
                        TryProcessingModifier(tags, ref effectBase);

                        effectBase.regionManager.AddRegion(0);
                        temp_fallbackEffects.Add(effectBase);
                    }
                    else
                    {
                        Debug.LogError($"TextAnimator: Effect Tag '{tagsToConvert[i]}' is not recognized.", this.gameObject);
                    }
                }

                return temp_fallbackEffects.ToArray();
            }

            this.fallbackAppearanceEffects = GetFallbackAppearancesFromTag(appearancesContainer.tagsFallback_Appearances);
            this.fallbackDisappearanceEffects = GetFallbackAppearancesFromTag(appearancesContainer.tagsFallback_Disappearances);

            var temp_fallbackBehaviorEffects = new List<BehaviorBase>();
            //Default behavior effects
            for (int i = 0; i < tags_fallbackBehaviors.Length; i++)
            {
                if (tags_fallbackBehaviors[i].Length <= 0)
                {
                    continue;
                }

                var tags = tags_fallbackBehaviors[i].Split(' ');
                string actualEffect = tags[0]; //removes probable modifiers

                foreach (var effect in temp_fallbackBehaviorEffects)
                {
                    if (effect.regionManager.entireRichTextTag.Equals(tags_fallbackBehaviors[i])) continue; //same effect has already been added
                }

                if (TryGetBehaviorClassFromTag(actualEffect, tags_fallbackBehaviors[i], 0, out BehaviorBase effectBase))
                {
                    effectBase.regionManager.AddRegion(0);
                    TryProcessingModifier(tags, ref effectBase);
                    temp_fallbackBehaviorEffects.Add(effectBase);
                }
                else
                {
                    Debug.LogError($"TextAnimator: Behavior Tag '{tags_fallbackBehaviors[i]}' is not recognized.", this.gameObject);
                }
            }

            this.fallbackBehaviorEffects = temp_fallbackBehaviorEffects.ToArray();

            #endregion


        }
        #endregion

        #region Effects Creation/Instancing

        bool TryGetBehaviorClassFromTag(string tag, string entireRichTextTag, int regionStartIndex, out BehaviorBase effectBase)
        {
            //Global Tags
            if (TAnimBuilder.TryGetGlobalBehaviorFromTag(tag, entireRichTextTag, out effectBase))
            {
                effectBase.SetDefaultValues(behaviorValues); //<-- add this
                effectBase.regionManager.AddRegion(regionStartIndex);
                return true;
            }

            //Local tags
            if (TAnimBuilder.TryGetEffectClassFromTag(localBehaviors, tag, entireRichTextTag, out effectBase))
            {
                effectBase.SetDefaultValues(behaviorValues); //<-- add this
                effectBase.regionManager.AddRegion(regionStartIndex);
                return true;
            }

            effectBase = default;
            return false;

        }

        bool TryGetAppearingClassFromTag(string tag, string entireRichTextTag, int startIndex, out AppearanceBase effectBase)
        {
            //Global Tags
            if (TAnimBuilder.TryGetGlobalAppearanceFromTag(tag, entireRichTextTag, out effectBase))
            {
                effectBase.regionManager.AddRegion(startIndex);
                return true;
            }

            //Local tags
            if (TAnimBuilder.TryGetEffectClassFromTag(localAppearances, tag, entireRichTextTag, out effectBase))
            {
                effectBase.regionManager.AddRegion(startIndex);
                return true;
            }

            effectBase = default;
            return false;
        }

        #endregion

        #region Management Methods

        #region Tags Processing
        const char m_closureSymbol = '/';
        const char m_eventSymbol = '?';
        const char m_disappearanceSymbol = '#';

        bool TryProcessingAppearanceTag(string richTextTag, int realTextIndex)
        {
            //Closure tag, eg. '/'
            if (richTextTag[0] == m_closureSymbol)
            {
                //Tries closing effect
                if (richTextTag.Length > 1 && richTextTag[1] == m_disappearanceSymbol)
                {
                    //disappearances, tag starts later (accounts for / and #)
                    return disappearanceEffects.CloseSingleOrAllEffects(richTextTag.Substring(2, richTextTag.Length - 2), realTextIndex);
                }
                else
                {
                    //appearances
                    return appearanceEffects.CloseSingleOrAllEffects(richTextTag.Substring(1, richTextTag.Length - 1), realTextIndex);
                }

            }
            else
            {
                bool ProcessOpeningTag(List<AppearanceBase> effectsList)
                {
                    //Avoids creating a new class if the same effect has already been instanced
                    for (int i = 0; i < effectsList.Count; i++)
                    {
                        if (effectsList[i].regionManager.TryReutilizingWithTag(richTextTag, realTextIndex))
                        {
                            return true;
                        }
                    }


                    //All the tags inside the { } region (without the opening and ending chars, '{' and '}') separated by a space
                    string[] tags = richTextTag.Split(' ');

                    #region Tries adding effect
                    if (TryGetAppearingClassFromTag(tags[0], richTextTag, realTextIndex, out AppearanceBase effectBase))
                    {
                        effectBase.SetDefaultValues(appearancesContainer.values);

                        TryProcessingModifier(tags, ref effectBase);

                        effectsList.TryAddingNewRegion(effectBase);

                        return true;
                    }

                    #endregion

                    return false;
                }

                if (richTextTag[0] == m_disappearanceSymbol) //disappearances
                {
                    richTextTag = richTextTag.Substring(1, richTextTag.Length - 1); //removes the disappearance symbol, e.g. #fade becomes fade
                    return ProcessOpeningTag(disappearanceEffects);
                }
                else //appearances
                {
                    return ProcessOpeningTag(appearanceEffects);
                }
            }
        }

        void TryProcessingModifier<T>(string [] tags, ref T effect) where T : EffectsBase
        {
            int equalsIndex;
            //Searches for modifiers inside the effect region (after the first tag, which we used to check the type of effect to add)
            for (int tagIndex = 1; tagIndex < tags.Length; tagIndex++)
            {
                equalsIndex = tags[tagIndex].IndexOf('=');

                //we've found an "=" symbol, so we're setting the modifier
                if (equalsIndex >= 0)
                {
                    //modifier name, from start to the equals symbol
                    string modifierName = tags[tagIndex].Substring(0, equalsIndex);

                    //Numeric value of the modifier (the part after the equal symbol)
                    string modifierValueName = tags[tagIndex].Substring(equalsIndex + 1);
                    //modifierValueName = modifierValueName.Replace('.', ','); //replaces dots with commas

                    effect.SetModifier(modifierName, modifierValueName);

#if UNITY_EDITOR
                    effect.EDITOR_RecordModifier(modifierName, modifierValueName);
#endif
                }
            }
        }

        bool TryProcessingBehaviorTag(string richTextTag, string loweredRichTextTag, int realTextIndex, ref int internalEventActionIndex)
        {
            if (loweredRichTextTag[0] == m_eventSymbol)
            {
                richTextTag = richTextTag.Substring(1, richTextTag.Length - 1);

                #region Tries firing event

                if (richTextTag.Length == 0) //prevents from adding an empty callback
                    return false;

                eventMarkers.Add(new EventMarker
                {
                    charIndex = realTextIndex,
                    eventMessage = richTextTag,
                    internalOrder = internalEventActionIndex,
                });

                internalEventActionIndex++; //increases internal events and features order
                return true;

                #endregion
            }
            else if (loweredRichTextTag[0] == m_closureSymbol)
            {
                loweredRichTextTag = loweredRichTextTag.Substring(1, loweredRichTextTag.Length - 1);

                #region Tries closing effect

                bool closedRegion = false;

                //Closes all the regions
                if (loweredRichTextTag.Length <= 0) //tag is </>
                {
                    //Closes ALL the region opened until now
                    for (int k = 0; k < behaviorEffects.Count; k++)
                    {
                        closedRegion = behaviorEffects.CloseElement(k, realTextIndex);
                    }
                }
                //Closes the current region
                else
                {
                    closedRegion = behaviorEffects.CloseRegionNamed(loweredRichTextTag, realTextIndex);
                }

                return closedRegion;

                #endregion
            }
            else
            {
                #region Tries adding effect

                //Avoids creating a new effect if the same one has already been instanced
                for (int i = 0; i < behaviorEffects.Count; i++)
                {
                    if (behaviorEffects[i].regionManager.TryReutilizingWithTag(loweredRichTextTag, realTextIndex))
                        return true;
                }

                //All the tags inside the "< >" region (without the opening and ending chars, '<' and '>') separated by a space
                string[] tags = loweredRichTextTag.Split(' ');

                //Creates a behavior effect
                if (TryGetBehaviorClassFromTag(tags[0], loweredRichTextTag, realTextIndex, out BehaviorBase behaviorEffect))
                {
                    behaviorEffect.SetDefaultValues(behaviorValues);

                    TryProcessingModifier(tags, ref behaviorEffect);

                    behaviorEffects.TryAddingNewRegion(behaviorEffect);
                    return true;
                }

                //No effect found
                return false;

                #endregion
            }

        }

        bool TryProcessingActionTag(string entireTag, int realTextIndex, ref int internalEventActionIndex)
        {
            //First part of the tag, "<ciao>" becomes "ciao"
            string firstPartTag = entireTag.Substring(1, entireTag.Length - 2);


            //Trims from the equal symbol. If it's "<ciao=3>" it becomes "ciao"
            int trimmeredIndex = entireTag.IndexOf('=');
            if (trimmeredIndex >= 0)
            {
                firstPartTag = firstPartTag.Substring(0, trimmeredIndex - 1);
            }

            //Checks if the tag is a recognized action
            if (TAnimBuilder.IsDefaultAction(firstPartTag) || TAnimBuilder.IsCustomAction(firstPartTag))
            {
                hasActions = true;

                InternalAction m_action = default;
                m_action.action = new TypewriterAction();

                m_action.action.actionID = firstPartTag;
                m_action.charIndex = realTextIndex;
                m_action.action.parameters = new List<string>();

                //the tag has also a part after the equal
                if (trimmeredIndex >= 0)
                {
                    //creates its parameters

                    string finalPartTag = entireTag.Substring(firstPartTag.Length + 2);

                    finalPartTag = finalPartTag
                        .Substring(0, finalPartTag.Length - 1);

                    //Splits parameters
                    m_action.action.parameters = finalPartTag.Split(',').ToList();
                }

                m_action.internalOrder = internalEventActionIndex;
                typewriterActions.Add(m_action);
                internalEventActionIndex++;

                return true;
            }

            return false;

        }

        #endregion

        bool noparseEnabled = false;
        int internalEventActionIndex = 0;

        List<int> temp_effectsToApply = new List<int>(); //temporary


        void _SetText(string text, ShowTextMode showTextMode)
        {
            //Prevents to calculate everything for an empty text
            if (text.Length <= 0)
            {
                hasText = false;
                text = string.Empty;
                tmproText.text = string.Empty;
                tmproText.ClearMesh();
                return;
            }

            BuildTagsDatabase();

            #region Resets text variables

            skipAppearanceEffects = false;
            hasActions = false;
            noparseEnabled = false;
            m_time.ResetData(); //resets time

            behaviorEffects.Clear();
            appearanceEffects.Clear();
            disappearanceEffects.Clear();
            eventMarkers.Clear();
            typewriterActions.Clear();
            latestTriggeredEvent = 0;
            latestTriggeredAction = 0;
            internalEventActionIndex = 0;

            #endregion

            #region Adds Fallback Effects

            //fallback effects are added at the end of the list
            for (int i = 0; i < fallbackAppearanceEffects.Length; i++)
            {
                appearanceEffects.Add(fallbackAppearanceEffects[i]);
            }

            for (int i = 0; i < fallbackDisappearanceEffects.Length; i++)
            {
                disappearanceEffects.Add(fallbackDisappearanceEffects[i]);
            }

            for (int i = 0; i < fallbackBehaviorEffects.Length; i++)
            {
                behaviorEffects.Add(fallbackBehaviorEffects[i]);
            }

            #endregion

            _ApplyTextToCharacters(_FormatText(text, 0));

            //--------
            //Decides how many characters to show
            //--------

            void HideCharacter(int i)
            {
                characters[i].data.passedTime = 0;
                characters[i].isDisappearing = true;
                characters[i].wantsToDisappear = true;
                characters[i].Hide();
            }

            void HideAllCharacters()
            {
                _maxVisibleCharacters = 0;

                for (int i = 0; i < textInfo.characterCount; i++)
                {
                    HideCharacter(i);
                }

                if (_maxVisibleCharacters <= 0 && characters.Length > 0)
                {
                    HideCharacter(0);
                }

            }

            void ShowAllCharacters()
            {
                _maxVisibleCharacters = textInfo.characterCount;

                //resets letters time
                for (int i = 0; i < textInfo.characterCount; i++)
                {
                    characters[i].data.passedTime = 0;
                    characters[i].isDisappearing = false;
                    characters[i].wantsToDisappear = false;
                }
            }

            switch (showTextMode)
            {
                case ShowTextMode.Hidden:
                    HideAllCharacters();
                    break;
                case ShowTextMode.Shown:
                    ShowAllCharacters();
                    break;
                case ShowTextMode.UserTyping:
                    maxVisibleCharacters = textInfo.characterCount + 1;
#if INTEGRATE_NANINOVEL
                    //Hides characters based on naninovel's progress 
                    for (int i = 0; i < characters.Length; i++)
                    {
                        if (i >= Mathf.CeilToInt(Mathf.Clamp01(reveablelText.RevealProgress) * textInfo.characterCount))
                        {
                            HideCharacter(i);
                        }
                    }
#endif

                    if (_maxVisibleCharacters - 1 < characters.Length && _maxVisibleCharacters - 1 >= 0)
                        HideCharacter(_maxVisibleCharacters - 1); //user is typing, the latest letter has time reset

                    break;
            }

#if TA_DEBUG
            DebugText();
#endif
        }


        private string _FormatText(string text, int startCharacterIndex)
        {
            System.Text.StringBuilder temp_realText = new System.Text.StringBuilder();

#if CHECK_ERRORS
            EDITOR_CompatibilityCheck(text);
#endif

            temp_realText.Clear();

            //Temporary variables
            string entireTag;
            string loweredRichTextTag;
            string richTextTag;

            int indexOfClosing;
            int indexOfNextOpening;

            for (int i = 0, realTextIndex = startCharacterIndex; i < text.Length; i++)
            {
                #region Local Methods
                void AppendCurrentCharacterToText()
                {
                    temp_realText.Append(text[i]);
                    realTextIndex++;
                }

                bool TryGetClosingCharacter(out char _closingCharacter)
                {
                    if (text[i] == TAnimBuilder.tag_behaviors.charOpeningTag)
                    {
                        _closingCharacter = TAnimBuilder.tag_behaviors.charClosingTag;
                        return true;
                    }
                    else if (text[i] == TAnimBuilder.tag_appearances.charOpeningTag)
                    {
                        _closingCharacter = TAnimBuilder.tag_appearances.charClosingTag;
                        return true;
                    }

                    _closingCharacter = default;
                    return false;
                }

                //Pastes the entire tag (eg. <ciao>) to the text
                void AppendCurrentTagToText()
                {
                    temp_realText.Append(entireTag);
                    realTextIndex += entireTag.Length;
                }

                #endregion

                if (TryGetClosingCharacter(out char closingCharacter))
                {
                    indexOfNextOpening = text.IndexOf(text[i], i + 1);
                    indexOfClosing = text.IndexOf(closingCharacter, i + 1);

                    //Checks if the tag is closed correctly and valid
                    if (
                        indexOfClosing >= 0  //the tag ends somewhere
                            && (
                                indexOfNextOpening > indexOfClosing || //next opening char is further from the closing (example, at first pos "<hello> <" is ok, "<<hello>" is wrong)
                                indexOfNextOpening < 0 //there isn't a next opening char
                            )
                        )
                    {
                        //entire tag found, including < and >
                        entireTag = (text.Substring(i, indexOfClosing - i + 1));
                        richTextTag = entireTag.Substring(1, entireTag.Length - 2);
                        loweredRichTextTag = richTextTag.ToLower();

                        #region Processes Tags
                        if (loweredRichTextTag.Length < 1) //avoids an empty tag
                        {
                            AppendCurrentTagToText();
                        }
                        else
                        {
                            if (closingCharacter == TAnimBuilder.tag_appearances.charClosingTag)
                            {
                                if (noparseEnabled || !TryProcessingAppearanceTag(loweredRichTextTag, realTextIndex))
                                {
                                    AppendCurrentTagToText();
                                }
                            }
                            else //behavior effects
                            {
                                switch (loweredRichTextTag)
                                {
                                    //<noparse>
                                    case "noparse": noparseEnabled = true; AppendCurrentTagToText(); break;
                                    case "/noparse": noparseEnabled = false; AppendCurrentTagToText(); break;


                                    default:

                                        if (noparseEnabled)
                                        {
                                            AppendCurrentTagToText();
                                        }
                                        else
                                        {
                                            if (!TryProcessingBehaviorTag(richTextTag, loweredRichTextTag, realTextIndex, ref internalEventActionIndex))
                                            {
                                                if (!TryProcessingActionTag(entireTag, realTextIndex, ref internalEventActionIndex))
                                                {
                                                    AppendCurrentTagToText();
                                                }
                                            }
                                        }
                                        break;
                                }


                            }
                        }
                        #endregion

                        //"skips" all the characters inside the tag, so we'll go back adding letters again
                        i = indexOfClosing;

                    }
                    else //tag is not closed correctly - pastes the tag opening/closing character (eg. '<')
                    {
                        AppendCurrentCharacterToText();
                    }
                }
                else
                {
                    AppendCurrentCharacterToText();
                }
            }

            return temp_realText.ToString();
        }

        void _ApplyTextToCharacters(string text)
        {
            //Applies the formatted to the component in order to get the proper TextInfo
            {
                //Avoids rendering the text for half a frame
                tmproText.renderMode = TextRenderFlags.DontRender;

                //--generates mesh and text info--
                tmproText.text = text; //<-- sets the text
                tmproText.ForceMeshUpdate(true);

                textInfo = tmproText.GetTextInfo(tmproText.text);
            }

            //Resizes characters array
            if (characters.Length < textInfo.characterCount)
                Array.Resize(ref characters, textInfo.characterCount);


            #region Effects and Features Initialization

            SetupEffectsIntensity();

            foreach (var effect in this.appearanceEffects)
            {
                effect.Initialize(characters.Length);
            }

            foreach (var effect in this.disappearanceEffects)
            {
                effect.Initialize(characters.Length);
            }

            foreach (var effect in this.behaviorEffects)
            {
                effect.Initialize(characters.Length);
            }

            #endregion

            #region Characters Setup
            for (int i = 0; i < textInfo.characterCount; i++)
            {
                characters[i].data.tmp_CharInfo = textInfo.characterInfo[i];

                //Calculates which effects are applied to this character

                #region Sources and data

                //Creates sources and data arrays only the first time
                if (!characters[i].initialized)
                {
                    characters[i].sources.vertices = new Vector3[TextUtilities.verticesPerChar];
                    characters[i].sources.colors = new Color32[TextUtilities.verticesPerChar];

                    characters[i].data.vertices = new Vector3[TextUtilities.verticesPerChar];
                    characters[i].data.colors = new Color32[TextUtilities.verticesPerChar];
                }

                #endregion

                void SetEffectsDependency<T>(ref int[] indexes, List<T> effects, int fallbackEffectsCount) where T : EffectsBase
                {
                    temp_effectsToApply.Clear();

                    //Checks if the character is inside a region of any effect, if yes we add a pointer to it
                    for (int l = fallbackEffectsCount; l < effects.Count; l++)
                    {
                        if (effects[l].regionManager.IsCharInsideRegion(textInfo.characterInfo[i].index))
                        {
                            temp_effectsToApply.Add(l);
                        }
                    }

                    indexes = new int[temp_effectsToApply.Count];
                    for (int x = 0; x < temp_effectsToApply.Count; x++)
                    {
                        indexes[x] = temp_effectsToApply[x];
                    }
                }

                //Assigns effects
                SetEffectsDependency(ref characters[i].indexBehaviorEffects, behaviorEffects, fallbackBehaviorEffects.Length);
                SetEffectsDependency(ref characters[i].indexAppearanceEffects, appearanceEffects, fallbackAppearanceEffects.Length);
                SetEffectsDependency(ref characters[i].indexDisappearanceEffects, disappearanceEffects, fallbackDisappearanceEffects.Length);

                #region Fallback Effects

                void AssignFallbackEffect<T>(T[] effect, ref int[] indexes) where T : EffectsBase
                {

                    //Assigns fallbacks appearances if there are no effects on the current characters
                    if (effect.Length > 0 && indexes.Length <= 0)
                    {
                        indexes = new int[effect.Length];
                        for (int x = 0; x < effect.Length; x++)
                        {
                            indexes[x] = x; //fallback effects are added at the start of the array
                        }
                    }
                }

                AssignFallbackEffect(fallbackAppearanceEffects, ref characters[i].indexAppearanceEffects);
                AssignFallbackEffect(fallbackBehaviorEffects, ref characters[i].indexBehaviorEffects);
                AssignFallbackEffect(fallbackDisappearanceEffects, ref characters[i].indexDisappearanceEffects);

                #endregion

                //Assigns duration
                float CalculateAppearanceDuration(int[] effectsIndex, List<AppearanceBase> effects)
                {
                    float duration = 0;
                    //calculates disappearance duration
                    foreach (var index in effectsIndex)
                    {
                        if (effects[index].effectDuration > duration) duration = effects[index].effectDuration;
                    }

                    return duration;
                }

                characters[i].disappearancesMaxDuration = CalculateAppearanceDuration(characters[i].indexDisappearanceEffects, disappearanceEffects);
                characters[i].appearancesMaxDuration = CalculateAppearanceDuration(characters[i].indexAppearanceEffects, appearanceEffects);



                //Copies source data from the mesh info only if the character is valid, otherwise its vertices array will be null and tAnim will start throw errors
                if (textInfo.characterInfo[i].isVisible)
                {
                    for (byte k = 0; k < TextUtilities.verticesPerChar; k++)
                    {
                        //vertices
                        characters[i].sources.vertices[k] = textInfo.meshInfo[textInfo.characterInfo[i].materialReferenceIndex].vertices[textInfo.characterInfo[i].vertexIndex + k];

                        //colors
                        characters[i].sources.colors[k] = textInfo.meshInfo[textInfo.characterInfo[i].materialReferenceIndex].colors32[textInfo.characterInfo[i].vertexIndex + k];
                    }
                }
            }

            //makes sure the new characters start invisible, and do not have a disappearance effect applied
            for (int i = maxVisibleCharacters; i < characters.Length; i++)
            {
                characters[i].isDisappearing = true;
                characters[i].data.passedTime = 0;
            }

            #endregion

            #region Updates variables
            hasText = text.Length > 0;
            autoSize = tmproText.enableAutoSizing;
            this.text = tmproText.text;
            #endregion


            AssertCharacterTimes();

            //Avoids the next text to be rendered for half a frame
            tmproText.renderMode = TextRenderFlags.DontRender;
            CopyMeshSources();
        }

        void TryTriggeringEvent(int maxInternalOrder)
        {
            //Calls all events markers until the current shown visible character
            for (int i = latestTriggeredEvent; i < eventMarkers.Count; i++)
            {
                if (!eventMarkers[i].triggered && //current event must not be triggered already
                    eventMarkers[i].charIndex <= textInfo.characterInfo[_maxVisibleCharacters].index && //triggers any event until the current character
                    eventMarkers[i].internalOrder < maxInternalOrder
                    )
                {
                    var _event = eventMarkers[i];
                    _event.triggered = true;
                    eventMarkers[i] = _event;

                    latestTriggeredEvent = i;
                    onEvent?.Invoke(eventMarkers[i].eventMessage);
                }
            }
        }

        /// <summary>
        /// Tries to get an action in the current position of the text
        /// </summary>
        /// <param name="action">Initialized feature</param>
        /// <returns>True if we have found one action  in the current text position</returns>
        internal bool TryGetAction(out TypewriterAction action)
        {
            if (_maxVisibleCharacters >= textInfo.characterCount) //avoids searching if text has ended
            {
                action = default;
                return false;
            }

            for (int i = latestTriggeredAction; i < typewriterActions.Count; i++)
            {
                if (typewriterActions[i].charIndex == textInfo.characterInfo[_maxVisibleCharacters].index &&
                    !typewriterActions[i].triggered)
                {
                    //tries triggering event, if it's written before function
                    TryTriggeringEvent(typewriterActions[i].internalOrder);

                    var typAction = typewriterActions[i];
                    typAction.triggered = true;
                    typewriterActions[i] = typAction;

                    action = typAction.action;

                    latestTriggeredAction = i;
                    return true;
                }
            }

            action = default;
            return false;
        }

        /// <summary>
        /// Assigns intensity multiplier and effect values/parameters to effects
        /// </summary>
        void SetupEffectsIntensity()
        {
            float intensity = effectIntensityMultiplier;

            if (useDynamicScaling)
            {
                //multiplies by font size
                intensity *= tmproText.fontSize / referenceFontSize;
            }

            void SetEffectsIntensity<T>(List<T> effects) where T : EffectsBase
            {
                foreach (var effect in effects)
                {
                    effect.uniformIntensity = intensity;
                }
            }

            SetEffectsIntensity(behaviorEffects);
            SetEffectsIntensity(appearanceEffects);
            SetEffectsIntensity(disappearanceEffects);
        }

        #endregion

        #region Mesh

        int tmpFirstVisibleCharacter;
        void CopyMeshSources()
        {
            forceMeshRefresh = false;
            autoSize = tmproText.enableAutoSizing;
            sourceRect = tmproText.rectTransform.rect;
            sourceColor = tmproText.color;
            tmpFirstVisibleCharacter = tmproText.firstVisibleCharacter;

            SetupEffectsIntensity();
            //Updates the characters sources
            for (int i = 0; i < textInfo.characterCount; i++)
            {
                //Updates TMP char info
                characters[i].data.tmp_CharInfo = textInfo.characterInfo[i];

                if (!textInfo.characterInfo[i].isVisible) continue;

                //Updates vertices
                for (byte k = 0; k < TextUtilities.verticesPerChar; k++)
                {
                    characters[i].sources.vertices[k] = textInfo.meshInfo[textInfo.characterInfo[i].materialReferenceIndex].vertices[textInfo.characterInfo[i].vertexIndex + k];
                }

                //Updates colors
                for (byte k = 0; k < TextUtilities.verticesPerChar; k++)
                {
                    characters[i].sources.colors[k] = textInfo.meshInfo[textInfo.characterInfo[i].materialReferenceIndex].colors32[textInfo.characterInfo[i].vertexIndex + k];
                }
            }
        }

        /// <summary>
        /// Applies the changes to the text component
        /// </summary>
        void UpdateMesh()
        {
            //Updates the mesh
            for (int i = 0; i < textInfo.characterCount; i++)
            {
                //Avoids updating if we're on an invisible character, like a spacebar
                //Do not switch this with "i<visibleCharacters", since the plugin has to update not yet visible characters
                if (!textInfo.characterInfo[i].isVisible) continue;

                //Updates TMP char info
                textInfo.characterInfo[i] = characters[i].data.tmp_CharInfo;

                //Updates vertices
                for (byte k = 0; k < TextUtilities.verticesPerChar; k++)
                {
                    textInfo.meshInfo[textInfo.characterInfo[i].materialReferenceIndex].vertices[textInfo.characterInfo[i].vertexIndex + k] = characters[i].data.vertices[k];
                }

                //Updates colors
                for (byte k = 0; k < TextUtilities.verticesPerChar; k++)
                {
                    textInfo.meshInfo[textInfo.characterInfo[i].materialReferenceIndex].colors32[textInfo.characterInfo[i].vertexIndex + k] = characters[i].data.colors[k];
                }
            }

            tmproText.UpdateVertexData();
        }

        #endregion

#if TA_DEBUG
        void DebugText()
        {
            System.Text.StringBuilder debugBuilder = new System.Text.StringBuilder();
            debugBuilder.Append($"TextAnimator Debug: Applying Text to TANIM component.\n");

            //Common Info
            debugBuilder.Append($"Frame number: {Time.frameCount} - Time: {Time.time}\n");
            debugBuilder.Append($"Visible characters: {_maxVisibleCharacters} out of {textInfo.characterCount}\n");


            //Prints effects
            debugBuilder.Append($"\nEffects: {behaviorEffects.Count + appearanceEffects.Count} - Appearances are {appearanceEffects.Count}, Behaviors are {behaviorEffects.Count}, Disappearances are {disappearanceEffects.Count}\n");

            //appearances

            debugBuilder.Append("\n--APPEARANCES--\n");
            for (int i = 0; i < appearanceEffects.Count; i++)
            {
                debugBuilder.Append($"[A #{i}] {appearanceEffects[i].regionManager}, duration: {appearanceEffects[i].effectDuration}s\n");
            }

            //disappearances
            debugBuilder.Append("\n--DISAPPEARANCES--\n");
            for (int i = 0; i < disappearanceEffects.Count; i++)
            {
                debugBuilder.Append($"[D #{i}] {disappearanceEffects[i].regionManager}, duration: {disappearanceEffects[i].effectDuration}s\n");
            }

            debugBuilder.Append("\n--BEHAVIORS--\n");
            //behaviors
            for (int i = 0; i < behaviorEffects.Count; i++)
            {
                debugBuilder.Append($"[B #{i}] {behaviorEffects[i].regionManager}\n");
            }


            debugBuilder.Append("\n--TANIM CHARACTERS--\n");
            //Characters
            for (int i = 0; i < textInfo.characterCount; i++)
            {
                var character = characters[i];
                if (!character.data.tmp_CharInfo.isVisible)
                {
                    debugBuilder.Append($"[CH #{i}] '{character.data.tmp_CharInfo.character}' - tmp_index: {character.data.tmp_CharInfo.index} - ");
                    continue;
                }

                debugBuilder.Append($"[C #{i}] '{character.data.tmp_CharInfo.character}' - tmp_index: {character.data.tmp_CharInfo.index} - ");

                if (character.indexAppearanceEffects.Length > 0)
                {
                    debugBuilder.Append($"appearances: ");
                    for (int j = 0; j < character.indexAppearanceEffects.Length; j++)
                    {
                        debugBuilder.Append($"[{character.indexAppearanceEffects[j]}]");
                    }

                    //adds a '-' in case there are other effects too
                    if (character.indexDisappearanceEffects.Length > 0 || character.indexBehaviorEffects.Length > 0)
                    {
                        debugBuilder.Append($" - ");
                    }

                }


                if (character.indexDisappearanceEffects.Length > 0)
                {
                    debugBuilder.Append($"disappearances: ");
                    for (int j = 0; j < character.indexDisappearanceEffects.Length; j++)
                    {
                        debugBuilder.Append($"[{character.indexDisappearanceEffects[j]}]");
                    }

                    debugBuilder.Append($", {character.disappearancesMaxDuration}s");

                    //adds a '-' in case there are other effects too
                    if (character.indexBehaviorEffects.Length > 0)
                    {
                        debugBuilder.Append($" - ");
                    }
                }


                if (character.indexBehaviorEffects.Length > 0)
                {
                    debugBuilder.Append($"behaviors: ");
                    for (int j = 0; j < character.indexBehaviorEffects.Length; j++)
                    {
                        debugBuilder.Append($"[{character.indexBehaviorEffects[j]}]");
                    }
                }

                debugBuilder.Append($"\n");


            }

            debugBuilder.Append("\n--TMP CHARACTERS--\n");
            //Characters
            for (int i = 0; i < textInfo.characterCount; i++)
            {
                var character = textInfo.characterInfo[i];
                debugBuilder.Append($"[C #{i}] '{character.character}' - visible: {character.isVisible} - index: {character.index}\n");
            }


            Debug.Log(debugBuilder.ToString(), this.gameObject);
        }
#endif

        private void Update()
        {
            //TMPRO's text changed, setting the text again
            if (!tmproText.text.Equals(text))
            {
                if (hasParentCanvas && !parentCanvas.isActiveAndEnabled)
                    return;

                //trigers anim player
                if (triggerAnimPlayerOnChange && tAnimPlayer != null)
                {

#if TA_NoTempFix
                    tAnimPlayer.ShowText(tmproText.text);
#else

                    //temp fix, opening and closing this TMPro tag (which won't be showed in the text, acting like they aren't there) because otherwise
                    //there isn't any way to trigger that the text has changed, if it's actually the same as the previous one.

                    if (tmproText.text.Length <= 0) //forces clearing the mesh during the tempFix, without the <noparse> tags
                        tAnimPlayer.ShowText("");
                    else
                        tAnimPlayer.ShowText($"<noparse></noparse>{tmproText.text}");
#endif

                }
                else //user is typing from TMPro
                {
                    _SetText(tmproText.text, ShowTextMode.UserTyping);
                }

                return;
            }

            if (!hasText)
                return;


            m_time.UpdateDeltaTime(timeScale);
            m_time.IncreaseTime();

            #region Effects Calculation

            for (int i = 0; i < behaviorEffects.Count; i++)
            {
                behaviorEffects[i].SetAnimatorData(m_time);
                behaviorEffects[i].Calculate();
            }

            for (int i = 0; i < appearanceEffects.Count; i++)
            {
                appearanceEffects[i].Calculate();
            }

            for (int i = 0; i < disappearanceEffects.Count; i++)
            {
                disappearanceEffects[i].Calculate();
            }
            #endregion

            for (int i = 0; i < textInfo.characterCount; i++)
            {

#if INTEGRATE_NANINOVEL
                //If we're integrating naninovels, shows characters based on its reveal component
                if (isNaninovelPresent)
                {
                    if (reveablelText.RevealProgress < (float)i / textInfo.characterCount)
                        continue;
                }
#endif

                //applies effects only if the character is visible in TMPro
                //otherwise the UVs etc. are all distorted and we don't want that
                if (!textInfo.characterInfo[i].isVisible)
                {
                    characters[i].data.passedTime = 0;
                    characters[i].Hide();
                    continue;
                }


                void TryApplyingBehaviors()
                {
                    if (enabled_globalBehaviors && enabled_localBehaviors)
                    {
                        foreach (int behaviorIndex in characters[i].indexBehaviorEffects)
                        {
                            behaviorEffects[behaviorIndex].ApplyEffect(ref characters[i].data, i);
                        }
                    }
                }


                //Makes the disappearance or appearance effect start instantly, in the correct order (to prevent graphic glitches when the user changes maxVisibleCharacters etc. in the middle of a frame or similar)
                if (characters[i].isDisappearing != characters[i].wantsToDisappear)
                {
                    characters[i].isDisappearing = characters[i].wantsToDisappear;
                    characters[i].data.passedTime = characters[i].isDisappearing ? characters[i].disappearancesMaxDuration : 0;
                }

                characters[i].ResetColors();
                characters[i].ResetVertices();

                //character is appearing
                if (!characters[i].isDisappearing)
                {
                    //behaviors
                    TryApplyingBehaviors();

                    //appearances
                    if (enabled_globalAppearances && enabled_localAppearances && !skipAppearanceEffects)
                    {
                        foreach (int appearanceIndex in characters[i].indexAppearanceEffects)
                        {
                            if (appearanceEffects[appearanceIndex].CanShowAppearanceOn(characters[i].data.passedTime))
                            {
                                appearanceEffects[appearanceIndex].ApplyEffect(ref characters[i].data, i);
                            }
                        }
                    }

                    characters[i].data.passedTime += m_time.deltaTime;

                }
                else //tries to apply disappearance effects
                {
                    //hides the character entirely
                    if (characters[i].data.passedTime <= 0)
                    {
                        characters[i].data.passedTime = 0;
                        characters[i].Hide();
                        continue;
                    }

                    //still applies behavior effects (if present) in order to not cut them while a letter disappears
                    TryApplyingBehaviors();

                    //disappearances
                    if (enabled_globalAppearances && enabled_localAppearances)
                    {
                        foreach (int disappearanceIndex in characters[i].indexDisappearanceEffects)
                        {
                            if (disappearanceEffects[disappearanceIndex].CanShowAppearanceOn(characters[i].data.passedTime))
                            {
                                disappearanceEffects[disappearanceIndex].ApplyEffect(ref characters[i].data, i);
                            }
                        }
                    }

                    characters[i].data.passedTime -= m_time.deltaTime;

                }
            }


            UpdateMesh();

            //TMPro's component changed, recalculating mesh
            //P.S. Must be placed after everything else.
            if (tmproText.havePropertiesChanged
                || forceMeshRefresh
                //changing the properties below doesn't seem to trigger 'havePropertiesChanged', so we're checking them manually
                || tmproText.enableAutoSizing != autoSize
                || tmproText.rectTransform.rect != sourceRect
                || tmproText.color != sourceColor
                || tmproText.firstVisibleCharacter != tmpFirstVisibleCharacter
                )
            {
                tmproText.ForceMeshUpdate();
                CopyMeshSources();
            }

        }

        private void OnEnable()
        {
            //The mesh might have changed when the gameObject was disabled (eg. change of "autoSize")
            forceMeshRefresh = true;
            Update();
            
#if UNITY_EDITOR
            TAnim_EditorHelper.onChangesApplied += EDITORONLY_ResetEffects;
#endif
        }

#if UNITY_EDITOR
        #region Editor
#if CHECK_ERRORS
        void EDITOR_CompatibilityCheck(string text)
        {
            #region Text
            string textLower = text.ToLower();
            string errorsLog = "";

            //page
            if ((textLower.Contains("<page=")))
            {
                errorsLog += "- Tag <page> is not compatible\n";
            }

            if (errorsLog.Length > 0)
            {
                Debug.LogError($"TextAnimator: Given text not accepted [expand for more details]\n\nText:'{text}'\n\nErrors:\n{errorsLog}", this.gameObject);
            }
            #endregion
        }
#endif

        [ContextMenu("Toggle Appearances (all scripts)")]
        void EDITORONLY_ToggleAppearances()
        {
            if (!Application.isPlaying)
                return;

            EnableAppearances(!enabled_globalAppearances);
        }

        [ContextMenu("Toggle Behaviors (all scripts)")]
        void EDITORONLY_ToggleBehaviors()
        {
            if (!Application.isPlaying)
                return;

            EnableBehaviors(!enabled_globalBehaviors);
        }

        private void OnDisable()
        {
            TAnim_EditorHelper.onChangesApplied -= EDITORONLY_ResetEffects;
        }

        void EDITORONLY_ResetEffects()
        {
            if (!Application.isPlaying)
                return;

            if (behaviorEffects != null && appearanceEffects != null && disappearanceEffects != null) 
            {
                //---sets intensity---
                for (int i = 0; i < behaviorEffects.Count; i++)
                {
                    behaviorEffects[i].SetDefaultValues(behaviorValues);
                }

                for (int i = 0; i < appearanceEffects.Count; i++)
                {
                    appearanceEffects[i].SetDefaultValues(appearancesContainer.values);
                }

                for (int i = 0; i < disappearanceEffects.Count; i++)
                {
                    disappearanceEffects[i].SetDefaultValues(appearancesContainer.values);
                }

                SetupEffectsIntensity();

                for (int i = 0; i < behaviorEffects.Count; i++)
                {
                    behaviorEffects[i].EDITOR_ApplyModifiers();
                }

                for (int i = 0; i < appearanceEffects.Count; i++)
                {
                    appearanceEffects[i].EDITOR_ApplyModifiers();
                }

                for (int i = 0; i < disappearanceEffects.Count; i++)
                {
                    disappearanceEffects[i].EDITOR_ApplyModifiers();
                }

            }
        }
        #endregion
#endif
    }
}