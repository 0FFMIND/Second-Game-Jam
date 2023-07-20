using UnityEngine;
using System.Collections.Generic;

public class TransitionOrderSideWipe : TransitionOrderBase
{
    public TransitionStartSide _startSide;

    public override void OnSetup()
    {
        TransitionEnumRandomizer.SetIfRandomTransitionStartSide(ref _startSide);

        switch(_startSide)
        {
            case TransitionStartSide.Top:
                AddRows(_rows - 1, -1, -1);
                break;
            case TransitionStartSide.Left:
                AddColumns(0, _columns, 1);
                break;
            case TransitionStartSide.Bottom:
                AddRows(0, _rows, 1);
                break;
            case TransitionStartSide.Right:
                AddColumns(_columns - 1, -1, -1);
                break;
            default:
                Debug.LogWarning("Start side is unknown, this can happen if you've added more start sides");
                break;
        }
    }

    private void AddRows(int rowStart, int rowEnd, int rowDirection)
    {
        for (int row = rowStart; row != rowEnd; row += rowDirection)
        {
            List<TransitionBlock> blocksToAdd = new List<TransitionBlock>();
            for (int col = 0; col < _columns; ++col)
            {
                blocksToAdd.Add(_transitionBlocks[row, col]);
            }
            AddTransitionBlocks(blocksToAdd);
        }
    }

    private void AddColumns(int colStart, int colEnd, int colDirection)
    {
        for (int col = colStart; col != colEnd; col += colDirection)
        {
            List<TransitionBlock> blocksToAdd = new List<TransitionBlock>();
            for (int row = 0; row < _rows; ++row)
            {
                blocksToAdd.Add(_transitionBlocks[row, col]);
            }
            AddTransitionBlocks(blocksToAdd);
        }
    }
}
