using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILineRenderer : MonoBehaviour
{
    public List<Vector2> points;
    // public List<Transform> pointsTR;
    // public Color color = Color.white;
    // public float thickness = 1f;
    private Image image;
    // private RectTransform rectTransform;
    public HalfCircleCollider MyCircle;
    public bool pointsSet;
    public Sprite sprite;

    public float Multiplier;

    private void Start()
    {
        image = GetComponent<Image>();
        // rectTransform = GetComponent<RectTransform>();
        // image.color = color;

    }
    // private void OnEnable()
    // {
    //     image.color = color;

    // }

    // private void OnDisable()
    // {
    //     image.color = Color.clear;
    // }

    // private void OnValidate()
    // {
    //     if (image != null)
    //     {
    //         image.color = color;
    //     }
    // }

    private void OnEnable()
    {

    }

    private void Update()
    {

        if (MyCircle.points.Length != 0)
        {
            if (pointsSet == false)
            {
                points.Add(new Vector2(Multiplier * (Screen.width / 2), 0));
                points.Add(MyCircle.points[1]);
                points.Add(new Vector2(Multiplier *(Screen.width / 2),  -(Screen.height / 2)));


                //  points.Add(new Vector2(-(Screen.width / 2), -(Screen.height / 2)));
                // points.Add(new Vector2(-(Screen.width / 2), 0));

                // for (int i = 0; i < MyCircle.points.Length; i++)
                // {
                //     
                // }

                // points.Add(new Vector2(Screen.width / 2, -(Screen.height / 2)));
                // points.Add(new Vector2(0, -(Screen.height / 2)));
                // points.Add(new Vector2(0, -(Screen.height / 2)));

                pointsSet = true;
            }
        }

        if (pointsSet)
        {
            if (points != null && points.Count >= 2)
            {
                Vector2[] screenPoints = new Vector2[points.Count];
                for (int i = 0; i < points.Count; i++)
                {
                    screenPoints[i] = RectTransformUtility.WorldToScreenPoint(null, points[i]);
                }

                List<Vector3> vertices = new List<Vector3>();
                for (int i = 0; i < screenPoints.Length; i++)
                {
                    vertices.Add(screenPoints[i]);
                }

                int[] triangles = new int[(screenPoints.Length - 2) * 3];
                for (int i = 0, vi = 1; i < triangles.Length; i += 3, vi++)
                {
                    triangles[i] = 0;
                    triangles[i + 1] = vi;
                    triangles[i + 2] = vi + 1;
                }

                // triangles[triangles.Length - 1] = 0; // Connect the last vertex with the second one to close the shape

                Mesh mesh = new Mesh();
                mesh.SetVertices(vertices);
                mesh.SetTriangles(triangles, 0);
                mesh.RecalculateBounds();
                mesh.RecalculateNormals();

                var canvasRenderer = GetComponent<CanvasRenderer>();
                canvasRenderer.SetMesh(mesh);
                // canvasRenderer.SetMaterial(image.material, null);
                //  canvasRenderer.SetTexture(image.mainTexture);
                canvasRenderer.SetColor(image.color);
            }
        }
    }
}
