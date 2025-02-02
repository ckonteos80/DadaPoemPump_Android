using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drawPolyCollider : MonoBehaviour
{
    PolygonCollider2D MyPolyColider;
    DrawShape MyDrawShape;
    Vector2[] points = new Vector2[5];
    // Start is called before the first frame update
    void Start()
    {
        MyDrawShape = GetComponent<DrawShape>();
        MyPolyColider = GetComponent<PolygonCollider2D>();
        MyPolyColider.pathCount = 5;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        points[0] = new Vector2(MyDrawShape.PointsLast[0].x, MyDrawShape.PointsLast[0].y);
        points[1] = new Vector2(MyDrawShape.PointsLast[1].x, MyDrawShape.PointsLast[1].y);
        points[2] = new Vector2(MyDrawShape.PointsLast[2].x, MyDrawShape.PointsLast[2].y);
        points[3] = new Vector2(MyDrawShape.PointsLast[3].x, MyDrawShape.PointsLast[3].y);
        points[4] = new Vector2(MyDrawShape.PointsLast[4].x, MyDrawShape.PointsLast[4].y);

        MyPolyColider.SetPath(0, points);
    }
}
