using System.Collections.Generic;

public class TransitionOrderTeeth : TransitionOrderBase
{
    public TransitionDirection _direction;

    public override void OnSetup()
    {
        TransitionEnumRandomizer.SetIfRandomTransitionDirection(ref _direction);

        if (_direction == TransitionDirection.Vertical)
        {
            int rowMarker = 0;
            for (int row = 0; row < _rows; ++row)
            {
                List<TransitionBlock> transitionBlocks = new List<TransitionBlock>();
                for (int col = 0; col < _columns; ++col)
                {
                    if (col % 2 == 0)
                    {
                        transitionBlocks.Add(_transitionBlocks[rowMarker, col]);
                    }
                    else
                    {
                        transitionBlocks.Add(_transitionBlocks[_rows - 1 - rowMarker, col]);
                    }
                }
                AddTransitionBlocks(transitionBlocks);
                rowMarker++;
            }
        }
        else
        {
            int colMarker = 0;
            for (int col = 0; col < _columns; ++col)
            {
                List<TransitionBlock> transitionBlocks = new List<TransitionBlock>();
                for (int row = 0; row < _rows; ++row)
                {
                    if (row % 2 == 0)
                    {
                        transitionBlocks.Add(_transitionBlocks[row, colMarker]);
                    }
                    else
                    {
                        transitionBlocks.Add(_transitionBlocks[row, _columns - 1 - colMarker]);
                    }
                }
                AddTransitionBlocks(transitionBlocks);
                colMarker++;
            }
        }
    }
}
