using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DiagonalTop : MonoBehaviour
{
    public List<Vector2> points;
    private Image image;
    public bool pointsSet;
    // public Sprite sprite;
    // public float Multiplier;
    public bool top;

    Master myMaster;

    DrawShape MyDrawShape;
    public Vector3[] Points3D = new Vector3[4];




    private void Start()
    {

        // image = GetComponent<Image>();
        myMaster = GetComponentInParent<Master>();
        MyDrawShape = GetComponent<DrawShape>();
    }

    private void Update()
    {
        if (pointsSet == false)
        {
            if (top)
            {
                points.Add(new Vector2(-(Screen.width / 2), (Screen.height / 2)));
                points.Add(new Vector2((Screen.width / 2), (Screen.height / 2)));
                points.Add(new Vector2((Screen.width / 2), (Screen.height / 2) - (myMaster.baseYSpace + myMaster.diagonalAngle)));
                points.Add(new Vector2(-(Screen.width / 2), (Screen.height / 2) - myMaster.baseYSpace));
            }
            else
            {
                points.Add(new Vector2(-(Screen.width / 2), -(Screen.height / 2)));
                points.Add(new Vector2((Screen.width / 2), -(Screen.height / 2)));
                points.Add(new Vector2((Screen.width / 2), -(Screen.height / 2) + (myMaster.baseYSpace - myMaster.diagonalAngle +200)));
                points.Add(new Vector2(-(Screen.width / 2), -(Screen.height / 2) + myMaster.baseYSpace + 250));

            }
            pointsSet = true;
        }

        if (pointsSet)
        {

            if (points != null && points.Count >= 2)
            {
                for (int i = 0; i < points.Count; i++)
                {
                    Points3D[i] = new Vector3(points[i].x, points[i].y, 0);
                }

                MyDrawShape.DrawShapes(Points3D);
                // Vector2[] screenPoints = new Vector2[points.Count];
                // for (int i = 0; i < points.Count; i++)
                // {
                //     screenPoints[i] = RectTransformUtility.WorldToScreenPoint(null, points[i]);
                // }

                // List<Vector3> vertices = new List<Vector3>();
                // for (int i = 0; i < screenPoints.Length; i++)
                // {
                //     vertices.Add(screenPoints[i]);
                // }

                // int[] triangles = new int[(screenPoints.Length - 2) * 3];
                // for (int i = 0, vi = 1; i < triangles.Length; i += 3, vi++)
                // {
                //     triangles[i] = 0;
                //     triangles[i + 1] = vi;
                //     triangles[i + 2] = vi + 1;
                // }

                // Mesh mesh = new Mesh();
                // mesh.SetVertices(vertices);
                // mesh.SetTriangles(triangles, 0);
                // mesh.RecalculateBounds();
                // mesh.RecalculateNormals();

                // var canvasRenderer = GetComponent<CanvasRenderer>();
                // canvasRenderer.SetMesh(mesh);
                // // canvasRenderer.SetMaterial(image.material, null);
                // //  canvasRenderer.SetTexture(image.mainTexture);
                // canvasRenderer.SetColor(image.color);
            }


        }
    }
}
