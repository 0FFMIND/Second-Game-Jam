using System.Collections;
using System;

public class TransitionOrderSpiral : TransitionOrderBase
{
    public SpiralDirection _spiralDirection;
    public TransitionStartCorner _transitionStartCorner;

    private int _xDirection;
    private int _yDirection;
    private int _currentX;
    private int _currentY;
    private bool[,] _visitedPositions;

    public override void OnSetup()
    {
        TransitionEnumRandomizer.SetIfRandomSpiralDirection(ref _spiralDirection);
        TransitionEnumRandomizer.SetIfRandomTransitionStartCorner(ref _transitionStartCorner);

        _visitedPositions = new bool[_rows, _columns];
        for(int row = 0; row < _rows; ++row)
        {
            for(int col = 0; col < _columns; ++col)
            {
                _visitedPositions[row, col] = false;
            }
        }
        SetStartPositionAndDirection();

        bool lastMove = true;
        bool currentMove = true;
        while(lastMove || currentMove)
        {
            lastMove = currentMove;

            AddTransitionBlock(_transitionBlocks[_currentY, _currentX]);
            currentMove = Move();
            if(!currentMove)
            {
                Turn();
                lastMove = currentMove;
                currentMove = Move();
            }
        }
    }

    void Turn()
    {
        if (_xDirection == 0 && _yDirection == 1)
        {
            if (_spiralDirection == SpiralDirection.Clockwise)
            {
                _xDirection = 1;
                _yDirection = 0;
            }
            else
            {
                _xDirection = -1;
                _yDirection = 0;
            }
        }
        else if (_xDirection == 0 && _yDirection == -1)
        {
            if (_spiralDirection == SpiralDirection.Clockwise)
            {
                _xDirection = -1;
                _yDirection = 0;
            }
            else
            {
                _xDirection = 1;
                _yDirection = 0;
            }
        }
        else if (_xDirection == 1 && _yDirection == 0)
        {
            if (_spiralDirection == SpiralDirection.Clockwise)
            {
                _xDirection = 0;
                _yDirection = -1;
            }
            else
            {
                _xDirection = 0;
                _yDirection = 1;
            }
        }
        else //_xDirection == -1 && _yDirection == 0
        {
            if (_spiralDirection == SpiralDirection.Clockwise)
            {
                _xDirection = 0;
                _yDirection = 1;
            }
            else
            {
                _xDirection = 0;
                _yDirection = -1;
            }
        }
    }

    bool Move()
    {
        int nextX = _currentX + _xDirection;
        int nextY = _currentY + _yDirection;
        if (IsPositionInGrid(nextX, nextY) && !_visitedPositions[nextY, nextX])
        {
            _currentX = nextX;
            _currentY = nextY;
            SetVisited(nextX, nextY);
            return true;
        }
        return false;
    }

    void SetVisited(int x, int y)
    {
        _visitedPositions[y, x] = true;
    }

    void SetStartPositionAndDirection()
    {
        if (_transitionStartCorner == TransitionStartCorner.TopLeft)
        {
            _currentX = 0;
            _currentY = _rows - 1;
            _xDirection = _spiralDirection == SpiralDirection.Clockwise ? 1 : 0;
            _yDirection = _spiralDirection == SpiralDirection.Clockwise ? 0 : -1;
        }
        else if (_transitionStartCorner == TransitionStartCorner.TopRight)
        {
            _currentX = _columns - 1;
            _currentY = _rows - 1;
            _xDirection = _spiralDirection == SpiralDirection.Clockwise ? 0 : -1;
            _yDirection = _spiralDirection == SpiralDirection.Clockwise ? -1 : 0;
        }
        else if (_transitionStartCorner == TransitionStartCorner.BottomLeft)
        {
            _currentX = 0;
            _currentY = 0;
            _xDirection = _spiralDirection == SpiralDirection.Clockwise ? 0 : 1;
            _yDirection = _spiralDirection == SpiralDirection.Clockwise ? 1 : 0;
        }
        else //_transitionStartCorner == TransitionStartCorner.BottomRight
        {
            _currentX = _columns - 1;
            _currentY = 0;
            _xDirection = _spiralDirection == SpiralDirection.Clockwise ? -1 : 0;
            _yDirection = _spiralDirection == SpiralDirection.Clockwise ? 0 : 1;
        }

        SetVisited(_currentX, _currentY);
    }
}
