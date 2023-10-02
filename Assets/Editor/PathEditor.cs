using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(PathCreator))] // 意思是它用来扩展PathCreator的编辑器功能
public class PathEditor : Editor
{
    PathCreator creator; // creator用来创建路径
    PathBase PathBase // PathBase用来访问路径对象
    {
        get
        {
            return creator.pathBase;
        }
    }
    const float segmentSelectDistanceThreshold = .1f; // 路径中的分段点
    int selectedSegmentIndex = -1;  // 路径中的控制点

    // 这里在Inspector面板上直接显示
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUI.BeginChangeCheck();
        if(GUILayout.Button("Create new")) // Undo就是字面意思，按了真就没了，这个是button，下面是在Inspector创建toggle
        {
            Undo.RecordObject(creator, "Create new");
            creator.CreatePath();
        }
        bool isClosed = GUILayout.Toggle(PathBase.IsClosed, "Closed"); //字面意思，按了closed真就闭合了
        if(isClosed != PathBase.IsClosed)
        {
            Undo.RecordObject(creator, "Toggle closed");
            PathBase.IsClosed = isClosed;
        }
        bool autoSetControlPoints = GUILayout.Toggle(PathBase.AutoSetControlPoints, "Auto Set Control Points"); // 字面意思的一个toggle
        if (autoSetControlPoints != PathBase.AutoSetControlPoints)
        {
            Undo.RecordObject(creator, "Toggle auto set controls");
            PathBase.AutoSetControlPoints = autoSetControlPoints;
        }

        if (EditorGUI.EndChangeCheck())
        {
            SceneView.RepaintAll();
        }
    }
    private void OnSceneGUI()
    {
        Input();
        Draw();
    }
    private void Input()
    {
        // 获取鼠标在世界空间中的位置
        Event guiEvent = Event.current;
        Vector2 mousePos = HandleUtility.GUIPointToWorldRay(guiEvent.mousePosition).origin;
        // 意思是鼠标点击，并且是左键点击，并且同时按下了shift键才会执行
        if (guiEvent.type == EventType.MouseDown && guiEvent.button == 0 && guiEvent.shift)
        {
            if (selectedSegmentIndex != -1)
            {
                Undo.RecordObject(creator, "Split segment");
                PathBase.SplitSegment(mousePos, selectedSegmentIndex);
            }
            else if (!PathBase.IsClosed)
            {
                Undo.RecordObject(creator, "Add segment");
                PathBase.AddSegment(mousePos);
            }
        }

        if (guiEvent.type == EventType.MouseDown && guiEvent.button == 1)
        {
            float minDstToAnchor = creator.anchorDiameter * .5f;
            int closestAnchorIndex = -1;

            for (int i = 0; i < PathBase.NumPoints; i += 3)
            {
                float dst = Vector2.Distance(mousePos, PathBase[i]);
                if (dst < minDstToAnchor)
                {
                    minDstToAnchor = dst;
                    closestAnchorIndex = i;
                }
            }

            if (closestAnchorIndex != -1)
            {
                Undo.RecordObject(creator, "Delete segment");
                PathBase.DeleteSegment(closestAnchorIndex);
            }
        }

        if (guiEvent.type == EventType.MouseMove)
        {
            float minDstToSegment = segmentSelectDistanceThreshold;
            int newSelectedSegmentIndex = -1;

            for (int i = 0; i < PathBase.NumSegments; i++)
            {
                Vector2[] points = PathBase.GetPointsInSegment(i);
                float dst = HandleUtility.DistancePointBezier(mousePos, points[0], points[3], points[1], points[2]);
                if (dst < minDstToSegment)
                {
                    minDstToSegment = dst;
                    newSelectedSegmentIndex = i;
                }
            }

            if (newSelectedSegmentIndex != selectedSegmentIndex)
            {
                selectedSegmentIndex = newSelectedSegmentIndex;
                HandleUtility.Repaint();
            }
        }

        HandleUtility.AddDefaultControl(0);
    }
    private void Draw()
    {
        for (int i = 0; i < PathBase.NumSegments; i++)
        {
            Vector2[] points = PathBase.GetPointsInSegment(i);
            if (creator.displayControlPoints)
            {
                Handles.color = Color.black;
                Handles.DrawLine(points[1], points[0]);
                Handles.DrawLine(points[2], points[3]);
            }
            Color segmentCol = (i == selectedSegmentIndex && Event.current.shift) ? creator.selectedSegmentCol : creator.segmentCol;
            Handles.DrawBezier(points[0], points[3], points[1], points[2], segmentCol, null, 2);
        }


        for (int i = 0; i < PathBase.NumPoints; i++)
        {
            if (i % 3 == 0 || creator.displayControlPoints)
            {
                Handles.color = (i % 3 == 0) ? creator.anchorCol : creator.controlCol;
                float handleSize = (i % 3 == 0) ? creator.anchorDiameter : creator.controlDiameter;
                Vector2 newPos = Handles.FreeMoveHandle(PathBase[i], Quaternion.identity, handleSize, Vector2.zero, Handles.CylinderHandleCap);
                if (PathBase[i] != newPos)
                {
                    Undo.RecordObject(creator, "Move point");
                    PathBase.MovePoint(i, newPos);
                }
            }
        }
    }
    private void OnEnable()
    {
        creator = (PathCreator)target;
        if(creator.pathBase == null)
        {
            creator.CreatePath();
        }
    }
}