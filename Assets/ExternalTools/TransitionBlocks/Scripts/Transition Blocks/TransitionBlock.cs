using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class TransitionBlock : MonoBehaviour
{
    [Tooltip("A multiplyer for the x-scale of the game object. You want this to end at 1.0 for the transition to look good.")]
    public AnimationCurve _xScaleOverTime;
    [Tooltip("A multiplyer for the y-scale of the game object. You want this to end at 1.0 for the transition to look good.")]
    public AnimationCurve _yScaleOverTime;
    [Tooltip("A translation for the x-position of the game object. You want this to end at 0.0 for the transition to look good.")]
    public AnimationCurve _xPositionOverTime;
    [Tooltip("A translation for the y-position of the game object. You want this to end at 0.0 for the transition to look good.")]
    public AnimationCurve _yPositionOverTime;
    [Tooltip("The rotation of the game object. A full rotation happens every 1.0 (1.0 is 360 degrees). You want this to end at any whole number (0.0, -1.0, 1.0, 25.0, etc...) to look good.")]
    public AnimationCurve _rotationOverTime;
    [Tooltip("The alpha of the sprite between 0.0 and 1.0 (0.0 being transparent and 1.0 being fully visible). You want this to end at 1.0 for the transition to look good.")]
    public AnimationCurve _alphaOverTime;

    [HideInInspector]
    public float _animationTime;

    [HideInInspector]
    public TransitionType _transitionType;

    [HideInInspector]
    public int _skipEveryXBlockUpdates;

    [HideInInspector]
    public bool _useScale = true;
    [HideInInspector]
    public bool _usePosition = true;
    [HideInInspector]
    public bool _useRotation = true;
    [HideInInspector]
    public bool _useAlpha = true;

    private Transform _transform;
    private SpriteRenderer _spriteRenderer;

    private Vector3 _originalScale;
    private Vector3 _originalPosition;
    private float _originalRotation;

    private float _previousXScale;
    private float _previousYScale;
    private float _previousXPosition;
    private float _previousYPosition;
    private float _previousRotation;
    private float _previousAlpha;

    void Start()
    {
        _transform = transform;
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _originalScale = transform.localScale;
        _originalPosition = transform.localPosition;
        _originalRotation = transform.localEulerAngles.z;

        _previousXScale = float.MaxValue;
        _previousYScale = float.MaxValue;
        _previousXPosition = float.MaxValue;
        _previousYPosition = float.MaxValue;
        _previousRotation = float.MaxValue;
        _previousAlpha = float.MaxValue;

        AnimationUpdate(_transitionType == TransitionType.In ? 1.0f : 0.0f);
    }

    public void StartTransition()
    {
        if(_skipEveryXBlockUpdates == 1)
        {
            StartCoroutine(Transition());
        }
        else
        {
            StartCoroutine(SkippingTransition());
        }
    }

    IEnumerator Transition()
    {
        float timer = 0.0f;
        float animationPercent = 0.0f;

        while (timer < _animationTime)
        {
            animationPercent = _transitionType == TransitionType.Out ? timer / _animationTime : 1.0f - (timer / _animationTime);
            AnimationUpdate(animationPercent);

            timer += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }

        //Make sure they finish completely
        animationPercent = _transitionType == TransitionType.Out ? 1.0f : 0.0f;
        AnimationUpdate(animationPercent);
    }

    IEnumerator SkippingTransition()
    {
        float timer = 0.0f;
        float animationPercent = 0.0f;

        int skip = Random.Range(0, _skipEveryXBlockUpdates);

        while (timer < _animationTime)
        {
            animationPercent = _transitionType == TransitionType.Out ? timer / _animationTime : 1.0f - (timer / _animationTime);

            if(skip < _skipEveryXBlockUpdates)
            {
                AnimationUpdate(animationPercent);
                skip++;
            }
            else
            {
                skip = 1;
            }

            timer += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }

        //Make sure they finish completely
        animationPercent = _transitionType == TransitionType.Out ? 1.0f : 0.0f;
        AnimationUpdate(animationPercent);
    }

    private void AnimationUpdate(float percent)
    {
        if(_useScale)
        {
            UpdateScale(percent);
        }
        if(_usePosition)
        {
            UpdatePosition(percent);
        }
        if(_useRotation)
        {
            UpdateRotation(percent);
        }
        if(_useAlpha)
        {
            UpdateAlpha(percent);
        }
    }

    bool CheckFloatEquality(float left, float right)
    {
        if(Mathf.Abs(left - right) < Mathf.Epsilon)
        {
            return true;
        }
        return false;
    }

    private void UpdateAlpha(float percent)
    {
        float alpha = _alphaOverTime.Evaluate(percent);
        if(CheckFloatEquality(alpha, _previousAlpha))
        {
            return;
        }

        Color color = _spriteRenderer.color;
        color.a = alpha;
        _spriteRenderer.color = color;

        _previousAlpha = alpha;
    }

    private void UpdateScale(float percent)
    {
        Vector3 scale = _originalScale;
        scale.x *= _xScaleOverTime.Evaluate(percent);
        scale.y *= _yScaleOverTime.Evaluate(percent);

        if(CheckFloatEquality(scale.x, _previousXScale) && CheckFloatEquality(scale.y, _previousYScale))
        {
            return;
        }

        _transform.localScale = scale;

        _previousXScale = scale.x;
        _previousYScale = scale.y;
    }

    private void UpdatePosition(float percent)
    {
        Vector3 position = _originalPosition;
        position.x += _xPositionOverTime.Evaluate(percent) * _originalScale.x;
        position.y += _yPositionOverTime.Evaluate(percent) * _originalScale.y;

        if(CheckFloatEquality(position.x, _previousXPosition) && CheckFloatEquality(position.y, _previousYPosition))
        {
            return;
        }

        _transform.localPosition = position;

        _previousXPosition = position.x;
        _previousYPosition = position.y;
    }

    private void UpdateRotation(float percent)
    {
        float rotation = _originalRotation;
        rotation += _rotationOverTime.Evaluate(percent) * 360.0f;

        if(CheckFloatEquality(rotation, _previousRotation))
        {
            return;
        }

        transform.localEulerAngles = new Vector3(0.0f, 0.0f, rotation);

        _previousRotation = rotation;
    }
}
