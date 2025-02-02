using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiagonalColider : MonoBehaviour
{
    private EdgeCollider2D edgeCollider;
    public DiagonalTop MyDiagonal;

    private void Update()
    {
        edgeCollider = GetComponent<EdgeCollider2D>();
        Vector2[] points = new Vector2[MyDiagonal.points.Count];

        for (int i = 0; i < MyDiagonal.points.Count; i++)
        {

            points[i] = MyDiagonal.points[i];
        }

        edgeCollider.points = points;
    }
}
