using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaperController : MonoBehaviour
{
    // public Vector2[] points;
    public RectTransform targetRectTransform;

    // public CanvasRenderer canvasRenderer;

    // public Vector3[] corners;

    // public Image image;

    public float XSpace;

    public float RandomSpace;

    public Vector3[] corners = new Vector3[4];

    // public bool draw;

    // public bool HasDrawn;

    DrawShape MyDrawShape;
    // Start is called before the first frame update
    void Start()
    {
        MyDrawShape = GetComponent<DrawShape>();
        // targetRectTransform = GetComponent<RectTransform>();
      
    }

 

    public void FindShape()
    {

        targetRectTransform.GetLocalCorners(corners);
        corners[0] = new Vector3(corners[0].x + XSpace + Random.Range(-RandomSpace, RandomSpace), corners[0].y + Random.Range(-RandomSpace, RandomSpace), corners[0].z);
        corners[1] = new Vector3(corners[1].x + XSpace + Random.Range(-RandomSpace, RandomSpace), corners[1].y + Random.Range(-RandomSpace, RandomSpace), corners[1].z);
        corners[2] = new Vector3(corners[2].x - XSpace + Random.Range(-RandomSpace, RandomSpace), corners[2].y + Random.Range(-RandomSpace, RandomSpace), corners[2].z);
        corners[3] = new Vector3(corners[3].x - XSpace + Random.Range(-RandomSpace, RandomSpace), corners[3].y + Random.Range(-RandomSpace, RandomSpace), corners[3].z);
    }



    void Update()
    {


        MyDrawShape.DrawShapes(corners);

    }

    // Update is called once per frame
    // public void SetPaper()
    // {
    //     List<Vector3> vertices = new List<Vector3>();
    //     for (int i = 0; i < corners.Length; i++)
    //     {
    //         vertices.Add(corners[i]);
    //     }

    //     int[] triangles = new int[(corners.Length - 2) * 3];
    //     for (int i = 0, vi = 1; i < triangles.Length; i += 3, vi++)
    //     {
    //         triangles[i] = 0;
    //         triangles[i + 1] = vi;
    //         triangles[i + 2] = vi + 1;
    //     }

    //     Mesh mesh = new Mesh();
    //     mesh.SetVertices(vertices);
    //     mesh.SetTriangles(triangles, 0);
    //     mesh.RecalculateBounds();
    //     mesh.RecalculateNormals();

    //     // canvasRenderer = GetComponent<CanvasRenderer>();
    //     canvasRenderer.SetMesh(mesh);
    //     // canvasRenderer.SetMaterial(image.material, null);
    //     //  canvasRenderer.SetTexture(image.mainTexture);
    //     canvasRenderer.SetColor(Color.green);

    // }
}
