using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public Vector2 startPoint;
    public Vector2 endPoint;

    private Transform arrowTransform;
    private float arrowLength;
    private float arrowTheta;
    private Vector2 arrowPosition;
    private void Start()
    {
        arrowTransform = this.transform;
    }
    private void Update()
    {
        //endPoint = InputManager.Instance.cursorPos;
    }

}
