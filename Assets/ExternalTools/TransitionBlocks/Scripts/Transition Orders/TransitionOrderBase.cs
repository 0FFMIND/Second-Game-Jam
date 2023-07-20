using UnityEngine;
using System.Collections.Generic;

public enum TransitionType
{
    In,
    Out
}

public enum TransitionStartCorner
{
    TopLeft,
    TopRight,
    BottomLeft,
    BottomRight,
    Random
}

public enum TransitionStartSide
{
    Top,
    Left,
    Bottom,
    Right,
    Random
}

public enum TransitionDirection
{
    Horizontal,
    Vertical,
    Random
}

public enum DiamondDirection
{
    Inward,
    Outward,
    Random
}

public enum SpiralDirection
{
    Clockwise,
    CounterClockwise,
    Random
}

public abstract class TransitionOrderBase : MonoBehaviour
{
    protected int _columns;
    protected int _rows;
    protected float _blockScale;
    protected TransitionBlock[,] _transitionBlocks;
    protected TransitionType _transitionType;
    protected int _centerXPosition;
    protected int _centerYPosition;

    private Queue<List<TransitionBlock>> _transitionBlocksOrder;
    private float _percentPerBlockActivate;
    private float _nextTriggerPercent;
    private Transform _transform;
    private float _initialCameraSize;
    private Camera _transitionCamera;

    public abstract void OnSetup();

    public void Setup(int columns, int rows, float blockScale, TransitionType transitionType, GameObject transitionBlockPrefab, Camera transitionCamera)
    {
        _columns = columns;
        _rows = rows;
        _blockScale = blockScale;
        _transitionType = transitionType;
        _transitionCamera = transitionCamera;
        _transform = transform;
        _centerXPosition = (_columns / 2);
        _centerYPosition = (_rows / 2);

        float halfBlockScale = _blockScale / 2.0f;
        float xTranslation = (_columns * -halfBlockScale) + halfBlockScale;
        float yTranslation = (_rows * -halfBlockScale) + halfBlockScale;

        _transitionBlocks = new TransitionBlock[rows, columns];
        for (int row = 0; row < _rows; ++row)
        {
            for (int col = 0; col < _columns; ++col)
            {
                _transitionBlocks[row, col] = CreateTransitionBlock(row, col, xTranslation, yTranslation, transitionBlockPrefab);
            }
        }

        _transitionBlocksOrder = new Queue<List<TransitionBlock>>();
        OnSetup();
        if (_transitionBlocksOrder.Count == 0)
        {
            Debug.LogWarning("You need to add transition blocks to the _transitionBlocksOrder list");
            return;
        }

        _percentPerBlockActivate = 1.0f / _transitionBlocksOrder.Count;
        _nextTriggerPercent = _percentPerBlockActivate / 2.0f;

        if (_transitionCamera.orthographic)
        {
            _initialCameraSize = _transitionCamera.orthographicSize;
        }
    }

    void Update()
    {
        //Allow the transition to change size to accomodate zooming in and out
        if (_transitionCamera.orthographic)
        {
            _transform.localScale = Vector3.one * (_transitionCamera.orthographicSize / _initialCameraSize);
        }
    }


    private TransitionBlock CreateTransitionBlock(int row, int col, float xTranslation, float yTranslation, GameObject transitionBlockPrefab)
    {
        Vector3 location = new Vector3((col * _blockScale) + xTranslation, (row * _blockScale) + yTranslation, 0.0f);

        GameObject block = (GameObject)Instantiate(transitionBlockPrefab, location, Quaternion.identity);
        block.transform.localScale = new Vector3(_blockScale, _blockScale);
        block.transform.parent = _transform;

        return block.GetComponent<TransitionBlock>();
    }

    public void SetPercent(float percent)
    {
        while (percent >= _nextTriggerPercent && _nextTriggerPercent < 1.0f && _transitionBlocksOrder.Count > 0)
        {
            _nextTriggerPercent += _percentPerBlockActivate;
            ActivateBlocks(_transitionBlocksOrder.Dequeue());
        }
    }

    private void ActivateBlocks(List<TransitionBlock> blocksToActivate)
    {
        foreach (TransitionBlock transitionBlock in blocksToActivate)
        {
            transitionBlock.StartTransition();
        }
    }

    protected void AddTransitionBlock(TransitionBlock transitionBlock)
    {
        List<TransitionBlock> transitionBlockList = new List<TransitionBlock>();
        transitionBlockList.Add(transitionBlock);
        AddTransitionBlocks(transitionBlockList);
    }

    protected void AddTransitionBlocks(List<TransitionBlock> trasnitionBlocks)
    {
        if (trasnitionBlocks.Count == 0)
        {
            return;
        }
        _transitionBlocksOrder.Enqueue(trasnitionBlocks);
    }

    protected bool IsPositionInGrid(int x, int y)
    {
        return (x >= 0 && x < _columns && y >= 0 && y < _rows);
    }
}
