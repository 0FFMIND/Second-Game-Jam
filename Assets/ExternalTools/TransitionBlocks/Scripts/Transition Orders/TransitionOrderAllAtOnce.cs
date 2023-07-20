using UnityEngine;
using System.Collections.Generic;
using System;

public class TransitionOrderAllAtOnce : TransitionOrderBase
{
    public override void OnSetup()
    {
        List<TransitionBlock> transitionBlocks = new List<TransitionBlock>();
        foreach (TransitionBlock transitionBlock in _transitionBlocks)
        {
            transitionBlocks.Add(transitionBlock);
        }
        AddTransitionBlocks(transitionBlocks);
    }
}
