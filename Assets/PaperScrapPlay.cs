using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PaperScrapPlay : MonoBehaviour
{

    public int aa;




    public TextMeshProUGUI TextField;
    // public RectTransform MyCanvasTransform;
    public string CutText;

    public Camera TheCamera;



    public float textWidth;
    public RectTransform MyOpenScrap;
    // PaperController MyPaperCont;
    public Vector3[] OpenCorners = new Vector3[4];
    public Vector3[] ClosedCorners = new Vector3[5];

    public float XSpace;
    public float RandomSpace;
    DrawShape MyDrawShape;
    public Canvas NewPaperCanvas;

    // public RectTransform MainRectTransform;

    public Image PaperImage;
    public RawImage backgroundImage;

    // public float minX;
    // public float maxX;
    // public float minY;
    // public float maxY;
    // Start is called before the first frame update
    void Start()
    {
        // CloseScap();

        // MyPaperCont = GetComponentInChildren<PaperController>();
        // TextField = GetComponentInChildren<TextMeshProUGUI>();
        MyDrawShape = GetComponentInChildren<DrawShape>();
        //  NewPaperCanvas = GetComponentInChildren<Canvas>();
        RectTransform MyCanvasTransform = NewPaperCanvas.GetComponent<RectTransform>();

        // MainRectTransform = GetComponent<RectTransform>();
        Master MyMaster = GetComponentInParent<Master>();

        TheCamera = MyMaster.TheCamera;
        NewPaperCanvas.worldCamera = TheCamera;


        TextField.font = MyMaster.fontList[MyMaster.FontNo];
        TextField.text = CutText;
        // TextField.enableAutoSizing = true;
        textWidth = TextField.preferredWidth;
        TextField.GetComponent<RectTransform>().sizeDelta = new Vector2(textWidth, TextField.GetComponent<RectTransform>().sizeDelta.y);
        MyCanvasTransform.sizeDelta = new Vector2(textWidth + XSpace, MyCanvasTransform.sizeDelta.y);


        // MyMaster.TextList.Add(TextField);

        //  PaperImage.sprite = MyMaster.PaperImage.sprite;
        // MyMaster.PaperList.Add(PaperImage);

        MyOpenScrap.GetLocalCorners(OpenCorners);
        OpenCorners[0] = new Vector3(OpenCorners[0].x - XSpace + Random.Range(-RandomSpace, RandomSpace), OpenCorners[0].y + Random.Range(-RandomSpace, RandomSpace), OpenCorners[0].z);
        OpenCorners[1] = new Vector3(OpenCorners[1].x - XSpace + Random.Range(-RandomSpace, RandomSpace), OpenCorners[1].y + Random.Range(-RandomSpace, RandomSpace), OpenCorners[1].z);
        OpenCorners[2] = new Vector3(OpenCorners[2].x + XSpace + Random.Range(-RandomSpace, RandomSpace), OpenCorners[2].y + Random.Range(-RandomSpace, RandomSpace), OpenCorners[2].z);
        OpenCorners[3] = new Vector3(OpenCorners[3].x + XSpace + Random.Range(-RandomSpace, RandomSpace), OpenCorners[3].y + Random.Range(-RandomSpace, RandomSpace), OpenCorners[3].z);
        // gameObject.GetComponentInChildren<BoxCollider2D>().size = new Vector2(textWidth, gameObject.GetComponentInChildren<BoxCollider2D>().size.y); ;
        // ClosedCorners[0] = new Vector3(-40 + Random.Range(-10, 10), 0 + Random.Range(-10, 10), 0);
        // ClosedCorners[1] = new Vector3(0 + Random.Range(-10, 10), 40 + Random.Range(-10, 10), 0);
        // ClosedCorners[2] = new Vector3(40 + Random.Range(-10, 10), 0 + Random.Range(-10, 10), 0);
        // ClosedCorners[3] = new Vector3(20 + Random.Range(-10, 10), -40 + Random.Range(-10, 10), 0);
        // ClosedCorners[4] = new Vector3(-20 + Random.Range(-10, 10), -40 + Random.Range(-10, 10), 0);


        // ClosedCorners[0] = new Vector3(Random.Range(-25, -100), Random.Range(-25, 80), 0);
        // ClosedCorners[1] = new Vector3(Random.Range(-30, 100), Random.Range(25, 100), 0);
        // ClosedCorners[2] = new Vector3(Random.Range(25, 100), Random.Range(-25, 100), 0);
        // ClosedCorners[3] = new Vector3(Random.Range(-30, 100), Random.Range(-25, -100), 0);
        // ClosedCorners[4] = new Vector3(Random.Range(-30, 100), Random.Range(-25, -100), 0);


        // MyPolyColider.pathCount = 5;
        // Vector2[] points = new Vector2[5];
        // points[0] = new Vector2(ClosedCorners[0].x, ClosedCorners[0].y);
        // points[1] = new Vector2(ClosedCorners[1].x, ClosedCorners[1].y);
        // points[2] = new Vector2(ClosedCorners[2].x, ClosedCorners[2].y);
        // points[3] = new Vector2(ClosedCorners[3].x, ClosedCorners[3].y);
        // points[4] = new Vector2(ClosedCorners[4].x, ClosedCorners[4].y);

        // // Set the points for the first path (index 0)


        // MyPolyColider.SetPath(0, points);

    }

    // Update is called once per frame
    void Update()
    {


        Vector3[] OpeningCorners = new Vector3[5];

        MyDrawShape.DrawShapes(OpenCorners);



    }



}
