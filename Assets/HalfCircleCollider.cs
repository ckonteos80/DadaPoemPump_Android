using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfCircleCollider : MonoBehaviour
{
    public int numPoints = 20; // Number of points on the curved edge
    private EdgeCollider2D edgeCollider;

    public List<Transform> MainPoints;

    RectTransform MyRect;

    public Vector2[] points;

    private void Update()
    {

        MyRect = GetComponent<RectTransform>();

        edgeCollider = GetComponent<EdgeCollider2D>();

        // Get screen dimensions
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // Define start, middle, and end points
        // Vector2 startPoint = new Vector2(MainPoints[0].position.x,MainPoints[0].position.y);
        // Vector2 middlePoint = new Vector2(MainPoints[1].position.x,MainPoints[1].position.y);
        // Vector2 endPoint = new Vector2(MainPoints[2].position.x,MainPoints[2].position.y);

        Vector2 startPoint = new Vector2(-screenWidth / 2, 0);
        Vector2 middlePoint = new Vector2(0, (screenHeight - 600) * -1);
        Vector2 endPoint = new Vector2(screenWidth / 2, 0);


        // Create an array to store the points
        points = new Vector2[numPoints];

        // Calculate the position of each point on the curve
        for (int i = 0; i < numPoints; i++)
        {
            float t = (float)i / (numPoints - 1);
            points[i] = Vector2.Lerp(Vector2.Lerp(startPoint, middlePoint, t), Vector2.Lerp(middlePoint, endPoint, t), t);
        }

        // Set the points to the EdgeCollider2D
        edgeCollider.points = points;


    }
}

