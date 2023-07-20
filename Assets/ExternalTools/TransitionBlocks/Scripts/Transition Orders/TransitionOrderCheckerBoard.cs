using UnityEngine;
using System.Collections.Generic;
using System;

public class TransitionOrderCheckerBoard : TransitionOrderBase
{
    public override void OnSetup()
    {
        List<TransitionBlock> odds = new List<TransitionBlock>();
        List<TransitionBlock> evens = new List<TransitionBlock>();
        for (int i = 0; i < _columns; ++i)
        {
            for(int j = 0; j < _rows; ++j)
            {
                if (i % 2 == 0)
                {
                    if(j % 2 == 0)
                    {
                        evens.Add(_transitionBlocks[j, i]);
                    }
                    else
                    {
                        odds.Add(_transitionBlocks[j, i]);
                    }
                }
                else
                {
                    if (j % 2 == 0)
                    {
                        odds.Add(_transitionBlocks[j, i]);
                    }
                    else
                    {
                        evens.Add(_transitionBlocks[j, i]);
                    }
                }
            }
        }
        AddTransitionBlocks(odds);
        AddTransitionBlocks(evens);
    }
}
