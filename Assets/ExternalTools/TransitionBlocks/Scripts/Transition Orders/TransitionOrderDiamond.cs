using UnityEngine;
using System.Collections.Generic;

public class TransitionOrderDiamond : TransitionOrderBase
{
    public DiamondDirection _direction;

    public override void OnSetup()
    {
        TransitionEnumRandomizer.SetIfRandomDiamondDirection(ref _direction);

        int largerSide = _columns > _rows ? _columns : _rows;
        for (int i = 0; i < largerSide; ++i)
        {
            AddTransitionBlocks(MakeDiamond( _direction == DiamondDirection.Outward ? i : largerSide - i - 1 ));
        }
    }

    protected List<TransitionBlock> MakeDiamond(int size)
    {
        List<TransitionBlock> transitionBlocks = new List<TransitionBlock>();

        if (size == 0)
        {
            transitionBlocks.Add(_transitionBlocks[_centerYPosition, _centerXPosition]);
            return transitionBlocks;
        }

        int xOffset = size;
        int yOffset = 0;
        while (xOffset >= 0)
        {
            if (IsPositionInGrid(_centerXPosition + xOffset, _centerYPosition + yOffset))
            {
                transitionBlocks.Add(_transitionBlocks[_centerYPosition + yOffset, _centerXPosition + xOffset]);
            }
            if (IsPositionInGrid(_centerXPosition - xOffset, _centerYPosition + yOffset))
            {
                transitionBlocks.Add(_transitionBlocks[_centerYPosition + yOffset, _centerXPosition - xOffset]);
            }
            if (IsPositionInGrid(_centerXPosition - xOffset, _centerYPosition - yOffset))
            {
                transitionBlocks.Add(_transitionBlocks[_centerYPosition - yOffset, _centerXPosition - xOffset]);
            }
            if (IsPositionInGrid(_centerXPosition + xOffset, _centerYPosition - yOffset))
            {
                transitionBlocks.Add(_transitionBlocks[_centerYPosition - yOffset, _centerXPosition + xOffset]);
            }

            xOffset--;
            yOffset++;
        }

        return transitionBlocks;
    }
}
