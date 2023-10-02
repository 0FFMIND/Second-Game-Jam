using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(PathCreator))] // ��˼����������չPathCreator�ı༭������
public class PathEditor : Editor
{
    PathCreator creator; // creator��������·��
    PathBase PathBase // PathBase��������·������
    {
        get
        {
            return creator.pathBase;
        }
    }
    const float segmentSelectDistanceThreshold = .1f; // ·���еķֶε�
    int selectedSegmentIndex = -1;  // ·���еĿ��Ƶ�

    // ������Inspector�����ֱ����ʾ
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUI.BeginChangeCheck();
        if(GUILayout.Button("Create new")) // Undo����������˼���������û�ˣ������button����������Inspector����toggle
        {
            Undo.RecordObject(creator, "Create new");
            creator.CreatePath();
        }
        bool isClosed = GUILayout.Toggle(PathBase.IsClosed, "Closed"); //������˼������closed��ͱպ���
        if(isClosed != PathBase.IsClosed)
        {
            Undo.RecordObject(creator, "Toggle closed");
            PathBase.IsClosed = isClosed;
        }
        bool autoSetControlPoints = GUILayout.Toggle(PathBase.AutoSetControlPoints, "Auto Set Control Points"); // ������˼��һ��toggle
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
        // ��ȡ���������ռ��е�λ��
        Event guiEvent = Event.current;
        Vector2 mousePos = HandleUtility.GUIPointToWorldRay(guiEvent.mousePosition).origin;
        // ��˼���������������������������ͬʱ������shift���Ż�ִ��
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