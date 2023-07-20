using UnityEngine;

public static class TransitionEnumRandomizer
{
    public static void SetIfRandomTransitionStartCorner(ref TransitionStartCorner transitionStartCorner)
    {
        if(transitionStartCorner != TransitionStartCorner.Random)
        {
            return;
        }

        int random = Random.Range(0, 4);
        switch (random)
        {
            case 0:
                transitionStartCorner = TransitionStartCorner.TopLeft;
                break;
            case 1:
                transitionStartCorner = TransitionStartCorner.TopRight;
                break;
            case 2:
                transitionStartCorner = TransitionStartCorner.BottomLeft;
                break;
            case 3:
                transitionStartCorner = TransitionStartCorner.BottomRight;
                break;
            default:
                Debug.Log("Random number out of bounds " + random.ToString());
                transitionStartCorner = TransitionStartCorner.TopLeft;
                break;
        }
    }

    public static void SetIfRandomTransitionStartSide(ref TransitionStartSide transitionStartSide)
    {
        if (transitionStartSide != TransitionStartSide.Random)
        {
            return;
        }

        int random = Random.Range(0, 4);
        switch (random)
        {
            case 0:
                transitionStartSide = TransitionStartSide.Top;
                break;
            case 1:
                transitionStartSide = TransitionStartSide.Left;
                break;
            case 2:
                transitionStartSide = TransitionStartSide.Bottom;
                break;
            case 3:
                transitionStartSide = TransitionStartSide.Right;
                break;
            default:
                Debug.Log("Random number out of bounds " + random.ToString());
                transitionStartSide = TransitionStartSide.Top;
                break;
        }
    }

    public static void SetIfRandomTransitionDirection(ref TransitionDirection transitionDirection)
    {
        if (transitionDirection != TransitionDirection.Random)
        {
            return;
        }

        int random = Random.Range(0, 2);
        switch (random)
        {
            case 0:
                transitionDirection = TransitionDirection.Horizontal;
                break;
            case 1:
                transitionDirection = TransitionDirection.Vertical;
                break;
            default:
                Debug.Log("Random number out of bounds " + random.ToString());
                transitionDirection = TransitionDirection.Horizontal;
                break;
        }
    }

    public static void SetIfRandomDiamondDirection(ref DiamondDirection diamondDirection)
    {
        if (diamondDirection != DiamondDirection.Random)
        {
            return;
        }

        int random = Random.Range(0, 2);
        switch (random)
        {
            case 0:
                diamondDirection = DiamondDirection.Inward;
                break;
            case 1:
                diamondDirection = DiamondDirection.Outward;
                break;
            default:
                Debug.Log("Random number out of bounds " + random.ToString());
                diamondDirection = DiamondDirection.Inward;
                break;
        }
    }

    public static void SetIfRandomSpiralDirection(ref SpiralDirection spiralDirection)
    {
        if (spiralDirection != SpiralDirection.Random)
        {
            return;
        }

        int random = Random.Range(0, 2);
        switch (random)
        {
            case 0:
                spiralDirection = SpiralDirection.Clockwise;
                break;
            case 1:
                spiralDirection = SpiralDirection.CounterClockwise;
                break;
            default:
                Debug.Log("Random number out of bounds " + random.ToString());
                spiralDirection = SpiralDirection.Clockwise;
                break;
        }
    }
}
