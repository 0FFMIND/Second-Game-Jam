using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathCreator : MonoBehaviour
{
    // 看上去平平无奇，但是PathEditor加在了这个类上面

    [HideInInspector]
    public PathBase pathBase;

    public Color anchorCol = Color.red; // 行走的拐点
    public Color controlCol = Color.white; // 用来走曲线用的
    public Color segmentCol = Color.green;  //显示路径
    public Color selectedSegmentCol = Color.yellow; // 当关键点被拖动显示黄色
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
