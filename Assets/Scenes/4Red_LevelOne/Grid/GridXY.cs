using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridXY
{
    private int width;
    private int height;
    private int[,] gridArray;
    private float cellSize;
    public GridXY(int _width, int _height, float _cellSize)
    {
        this.width = _width;
        this.height = _height;
        this.cellSize = _cellSize;
        gridArray = new int[width, height];
        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                CreateWorldText(gridArray[x, y].ToString(),Color.white, TextAnchor.MiddleCenter,TextAlignment.Center, null, GetWorldPosition(x, y), 20);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
            }
        }
    }
    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize;
    }
    // 简单重载调用，涉及到可选参数必须在必需参数之后
    public TextMesh CreateWorldText(string text,Color color,TextAnchor textAnchor,TextAlignment textAlignment = TextAlignment.Center,Transform parent = null, Vector3 localPosition = default(Vector3),int fontSize = 40)
    {
        return CreateWorldText(color, text, textAnchor, textAlignment, parent, localPosition, fontSize);
    }
    // 底层，为了避免具有两意性，需要把前面两个参数换一个位置
    public TextMesh CreateWorldText(Color color, string text, TextAnchor textAnchor, TextAlignment textAlignment, Transform parent, Vector3 localposition, int fontSize)
    {
        // GameObject gameObject
        return null;
    }
}
