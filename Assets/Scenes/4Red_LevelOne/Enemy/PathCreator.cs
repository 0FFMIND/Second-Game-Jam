using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathCreator : MonoBehaviour
{
    // ����ȥƽƽ���棬����PathEditor���������������

    [HideInInspector]
    public PathBase pathBase;

    public Color anchorCol = Color.red; // ���ߵĹյ�
    public Color controlCol = Color.white; // �����������õ�
    public Color segmentCol = Color.green;  //��ʾ·��
    public Color selectedSegmentCol = Color.yellow; // ���ؼ��㱻�϶���ʾ��ɫ
    public float anchorDiameter;
    public float controlDiameter;
    public bool displayControlPoints = true;

    public void CreatePath()
    {
        pathBase = new PathBase(transform.position);
    }
    private void Reset()
    {
        CreatePath();
    }
}
