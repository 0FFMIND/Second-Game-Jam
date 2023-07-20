using System.Collections.Generic;

public class TransitionOrderCornerWipe : TransitionOrderBase
{
    public TransitionStartCorner _transitionStartCorner;

    public override void OnSetup()
    {
        TransitionEnumRandomizer.SetIfRandomTransitionStartCorner(ref _transitionStartCorner);

        if (_transitionStartCorner == TransitionStartCorner.TopLeft)
        {
            AddBlocksFromCorner(-(_rows + _columns), _rows + _columns, 1, 0, _rows, 1);
        }
        else if (_transitionStartCorner == TransitionStartCorner.TopRight)
        {
            AddBlocksFromCorner(_rows + _columns, -1, -1, 0, _rows, 1);
        }
        else if (_transitionStartCorner == TransitionStartCorner.BottomLeft)
        {
            AddBlocksFromCorner(0, _rows + _columns, 1, 0, _rows, 1);
        }
        else //TransitionStartCorner.BottomRight
        {
            AddBlocksFromCorner(_rows + _columns, -(_rows + _columns), -1, 0, _rows, 1);
        }
    }

    void AddBlocksFromCorner(int colStart, int colEnd, int colDirection, int rowStart, int rowEnd, int rowDirection)
    {
        for (int col = colStart; col != colEnd; col += colDirection)
        {
            List<TransitionBlock> transitionBlocks = new List<TransitionBlock>();
            for (int row = rowStart; row != rowEnd; row += rowDirection)
            {
                if(_transitionStartCorner == TransitionStartCorner.BottomLeft || _transitionStartCorner == TransitionStartCorner.TopRight)
                {
                    if (IsPositionInGrid(col - row, row))
                    {
                        transitionBlocks.Add(_transitionBlocks[row, col - row]);
                    }
                }
                else //_transitionStartCorner == TransitionStartCorner.BottomRight || _transitionStartCorner == TransitionStartCorner.TopLeft
                {
                    if (IsPositionInGrid(col + row, row))
                    {
                        transitionBlocks.Add(_transitionBlocks[row, col + row]);
                    }
                }
            }
            AddTransitionBlocks(transitionBlocks);
        }
    }
}
