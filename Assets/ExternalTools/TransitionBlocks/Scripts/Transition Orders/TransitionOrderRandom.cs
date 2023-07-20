using UnityEngine;
using System.Collections.Generic;

public class TransitionOrderRandom : TransitionOrderBase
{
    [Range(1, 50)]
    public int _blocksPerGroup = 1;

    public override void OnSetup()
    {
        List<TransitionBlock> transitionBlocks = new List<TransitionBlock>();
        foreach (TransitionBlock transitionBlock in _transitionBlocks)
        {
            transitionBlocks.Add(transitionBlock);
        }
        while(transitionBlocks.Count > 0)
        {
            List<TransitionBlock> randomBlocks = new List<TransitionBlock>();
            for(int i = 0; i < _blocksPerGroup; ++i)
            {
                if(transitionBlocks.Count > 0)
                {
                    int randomBlockLocation = Random.Range(0, transitionBlocks.Count);
                    randomBlocks.Add(transitionBlocks[randomBlockLocation]);
                    transitionBlocks.RemoveAt(randomBlockLocation);
                }
            }
            AddTransitionBlocks(randomBlocks);
        }
    }
}
