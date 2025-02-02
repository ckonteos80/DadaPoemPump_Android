using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class drawButton : MonoBehaviour
{
    public RectTransform targetRectTransform;
    public Vector3[] corners = new Vector3[4];
    public float XSpace;

    public float RandomSpace;

    public CanvasRenderer canvasRenderer;
    public Vector3[] PointsLast;
    public bool keepDraw;

    public bool startDraw;

    public bool hasDrawn;

    public bool runAfterStart;

    public bool scaleToScreen;

    public float divider;


    // Start is called before the first frame update
    void Start()
    {
        targetRectTransform = GetComponent<RectTransform>();
        canvasRenderer = GetComponent<CanvasRenderer>();
        //  FindShape();
        startDraw = true;
        keepDraw = true;

        if (scaleToScreen)
        {
            float screenWidth;
            float screenHeight;
           
                screenWidth = Screen.width - Screen.width / divider - 10;
                screenHeight = targetRectTransform.sizeDelta.y;
           
            // Get screen dimensions

            // Set the RectTransform size to match the screen size
            targetRectTransform.sizeDelta = new Vector2(screenWidth, screenHeight);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (startDraw)
        {
            FindShape();


            if (keepDraw)
            {
                keepDraw = false;
            }
            startDraw = false;
        }
        if (keepDraw)
        {
            FindShape();
        }
        if (runAfterStart)
        {
            Invoke("setDraw", 0.01f);

        }

        // bool pointsNotEqual = AreCornersNotEqual();

        // if (pointsNotEqual)
        // {
        //     Debug.Log("The canvas-rendered corners are NOT equal to the reference points.");
        //     // startDraw = true;
        //     FindShape();
        // }
        // else
        // {
        //     Debug.Log("The canvas-rendered corners are equal to the reference points.");
        //     // startDraw = false;
        // }

    }
    public void FindShape()
    {

        targetRectTransform.GetLocalCorners(corners);
        corners[0] = new Vector3(corners[0].x + XSpace + UnityEngine.Random.Range(-RandomSpace, RandomSpace), corners[0].y + UnityEngine.Random.Range(-RandomSpace, RandomSpace), corners[0].z);
        corners[1] = new Vector3(corners[1].x + XSpace + UnityEngine.Random.Range(-RandomSpace, RandomSpace), corners[1].y + UnityEngine.Random.Range(-RandomSpace, RandomSpace), corners[1].z);
        corners[2] = new Vector3(corners[2].x - XSpace + UnityEngine.Random.Range(-RandomSpace, RandomSpace), corners[2].y + UnityEngine.Random.Range(-RandomSpace, RandomSpace), corners[2].z);
        corners[3] = new Vector3(corners[3].x - XSpace + UnityEngine.Random.Range(-RandomSpace, RandomSpace), corners[3].y + UnityEngine.Random.Range(-RandomSpace, RandomSpace), corners[3].z);
        DrawShapes(corners);
    }
    void setDraw()
    {
        // startDraw = true;
        FindShape();
        runAfterStart = false;
    }
    // bool AreCornersNotEqual()
    // {
    //     // Vector3[] corners = new Vector3[4];
    //      targetRectTransform.GetWorldCorners(corners);

    //     for (int i = 0; i < 4; i++)
    //     {
    //         if (corners[i] != PointsLast[i])
    //         {
    //             return true; // If any corner does not match, return true
    //         }
    //     }

    //     return false; // All corners matched

    // }
    void DrawShapes(Vector3[] Points)
    {
        PointsLast = Points;
        List<Vector3> vertices = new List<Vector3>();
        for (int i = 0; i < PointsLast.Length; i++)
        {
            vertices.Add(PointsLast[i]);
        }

        int[] triangles = new int[(PointsLast.Length - 2) * 3];
        for (int i = 0, vi = 1; i < triangles.Length; i += 3, vi++)
        {
            triangles[i] = 0;
            triangles[i + 1] = vi;
            triangles[i + 2] = vi + 1;
        }

        Mesh mesh = new Mesh();
        mesh.SetVertices(vertices);
        mesh.SetTriangles(triangles, 0);
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        // canvasRenderer = GetComponent<CanvasRenderer>();
        canvasRenderer.SetMesh(mesh);
        // canvasRenderer.SetMaterial(image.material, null);
        //  canvasRenderer.SetTexture(image.mainTexture);
        // canvasRenderer.SetColor(Color.green);

        hasDrawn = true;



    }
}
