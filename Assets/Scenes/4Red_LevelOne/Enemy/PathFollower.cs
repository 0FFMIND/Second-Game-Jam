
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    public float Speed;
    public float OffsetAmount;

    private Vector2 PositionOffset;
    private PathBase path;
    private int segmentIndex;

    private Vector2 A, B, C, D;
    private Vector2 v1, v2, v3;
    private float t;

    void OnEnable()
    {
        path = GameObject.Find("GroundPath").GetComponent<PathCreator>().pathBase;
        segmentIndex = 0;
        PositionOffset = Random.insideUnitCircle * Random.Range(-OffsetAmount, OffsetAmount); // 稍微偏移一点路线
        RecomputeSegment();
        transform.position = new Vector3(A.x, A.y, 1f);
    }

    void FixedUpdate()
    {
        if (segmentIndex >= path.NumSegments) return;

        if (t >= 1.0f)
        {
            segmentIndex++;
            if (segmentIndex >= path.NumSegments) return;

            RecomputeSegment();
        }

        var tangent = t * t * v1 + t * v2 + v3;
        t = t + Time.deltaTime * Speed / tangent.magnitude;

        transform.position = new Vector3(EvaluateCubic(A, B, C, D, t).x,EvaluateCubic(A,B,C,D,t).y,1f);
        // transform.eulerAngles = new Vector3(0.0f, 0.0f, Angle(tangent, Vector2.right));

        if (gameObject.layer == LayerMask.NameToLayer("enemy")) return;
            //EnemyManager.Instance.UpdateEnemy(gameObject, path.NumSegments - segmentIndex - t);
    }

    private void RecomputeSegment()
    {
        var segment = path.GetPointsInSegment(segmentIndex);

        A = segment[0] + PositionOffset;
        B = segment[1] + PositionOffset;
        C = segment[2] + PositionOffset;
        D = segment[3] + PositionOffset;

        v1 = -3 * A + 9 * B - 9 * C + 3 * D;
        v2 = 6 * A - 12 * B + 6 * C;
        v3 = -3 * A + 3 * B;

        t = 0;
    }
    // 接受四个控制点，用来绘制三次贝塞尔曲线(起点为a，终点为d，根据bc位置绘制出一条流体曲线)
    private Vector2 EvaluateCubic(Vector2 a, Vector2 b, Vector2 c, Vector2 d, float t)
    {
        Vector2 p0 = EvaluateQuadratic(a, b, c, t);
        Vector2 p1 = EvaluateQuadratic(b, c, d, t);
        return Vector2.Lerp(p0, p1, t); 
    }
    private Vector2 EvaluateQuadratic(Vector2 a, Vector2 b, Vector2 c, float t)
    {
        Vector2 p0 = Vector2.Lerp(a, b, t);
        Vector2 p1 = Vector2.Lerp(b, c, t);
        return Vector2.Lerp(p0, p1, t);
    }
    public float Angle(Vector2 a, Vector2 b)
    {
        var an = a.normalized;
        var bn = b.normalized;
        var x = an.x * bn.x + an.y * bn.y;
        var y = an.y * bn.x - an.x * bn.y;
        return Mathf.Atan2(y, x) * Mathf.Rad2Deg;
    }
}
