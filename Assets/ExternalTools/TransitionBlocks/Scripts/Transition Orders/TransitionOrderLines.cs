using UnityEngine;
using System.Collections;
using System;

public class TransitionOrderLines : TransitionOrderBase
{
    public TransitionStartCorner _startCorner;
    public TransitionDirection _direction;

    public override void OnSetup()
    {
        TransitionEnumRandomizer.SetIfRandomTransitionStartCorner(ref _startCorner);
        TransitionEnumRandomizer.SetIfRandomTransitionDirection(ref _direction);

        int rowStart = 0;
        int rowEnd = 0;
        int rowDirection = 0;
        int colStart = 0;
        int colEnd = 0;
        int colDirection = 0;

        SetDirectionVariables(ref rowStart, ref rowEnd, ref rowDirection, ref colStart, ref colEnd, ref colDirection);

        if (_direction == TransitionDirection.Horizontal)
        {
            for (int i = rowStart; i != rowEnd; i += rowDirection)
            {
                for (int j = colStart; j != colEnd; j += colDirection)
                {
                    AddTransitionBlock(_transitionBlocks[i, j]);
                }
            }
        }
        else
        {
            for (int j = colStart; j != colEnd; j += colDirection)
            {
                for (int i = rowStart; i != rowEnd; i += rowDirection)
                {
                    AddTransitionBlock(_transitionBlocks[i, j]);
                }
            }
        }

    }

    private void SetDirectionVariables(ref int rowStart, ref int rowEnd, ref int rowDirection, ref int colStart, ref int colEnd, ref int colDirection)
    {
        switch (_startCorner)
        {
            case TransitionStartCorner.TopLeft:
                rowStart = _rows - 1;
                rowEnd = -1;
                rowDirection = -1;
                colStart = 0;
                colEnd = _columns;
                colDirection = 1;
                break;
            case TransitionStartCorner.TopRight:
                rowStart = _rows - 1;
                rowEnd = -1;
                rowDirection = -1;
                colStart = _columns - 1;
                colEnd = -1;
                colDirection = -1;
                break;
            case TransitionStartCorner.BottomLeft:
                rowStart = 0;
                rowEnd = _rows;
                rowDirection = 1;
                colStart = 0;
                colEnd = _columns;
                colDirection = 1;
                break;
            case TransitionStartCorner.BottomRight:
                rowStart = 0;
                rowEnd = _rows;
                rowDirection = 1;
                colStart = _columns - 1;
                colEnd = -1;
                colDirection = -1;
                break;
            default:
                Debug.Log("Unknown start corner for this transition");
                break;
        }
    }
}
