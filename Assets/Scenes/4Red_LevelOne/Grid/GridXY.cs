using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridXY
{
    private int width;
    private int height;
    private int[,] gridArray;
    public GridXY(int width, int height)
    {
        this.width = width;
        this.height = height;
        gridArray = new int[width, height];
        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {

            }
        }
    }
}
