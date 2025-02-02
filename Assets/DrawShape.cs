using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawShape : MonoBehaviour
{
    // public Vector3[] corners = new Vector3[4];
    CanvasRenderer canvasRenderer;
    public Vector3[] PointsLast;
    public bool keepDraw;


    // Start is called before the first frame update
    void Start()
    {
        canvasRenderer = GetComponent<CanvasRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (keepDraw)
        {
            DrawShapes(PointsLast);
        }
    }

    public void DrawShapes(Vector3[] Points)
    {
        PointsLast = Points;
        if (PointsLast == null || PointsLast.Length < 3)
        {
            Debug.LogWarning("Not enough points to form a shape.");
            return;
        }
        else
        {


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
            canvasRenderer.SetColor(Color.green);
        }

    }
}
