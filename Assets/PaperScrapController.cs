using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PaperScrapController : MonoBehaviour
{
    public int aa;




    public TextMeshProUGUI TextField;
    // public RectTransform MyCanvasTransform;
    public string CutText;

    public Camera TheCamera;


    public int Stage;
    // 0 closed
    // 1 opening
    // 2 closing
    // 3 open


    // public RectTransform TheMousePoint;
    public bool isOpen;
    public float transitionSpeed;
    public float textWidth;
    public PeperBagController MyPaperBag;
    public Draggable MyDragable;
    public RectTransform MyOpenScrap;
    PaperController MyPaperCont;
    public Vector3[] OpenCorners = new Vector3[4];
    public Vector3[] ClosedCorners = new Vector3[5];
    public bool openTheScrap;
    public bool closeTheScrap;
    public float OpenTransition;
    public float XSpace;
    public float RandomSpace;
    DrawShape MyDrawShape;

    public CircleCollider2D MyCircleColider;
    public PolygonCollider2D MyPolyColider;
    public RectTransform MainRectTransform;

    public Image PaperImage;

    // public float minX;
    // public float maxX;
    // public float minY;
    // public float maxY;
    // Start is called before the first frame update
    void Start()
    {
        // CloseScap();
        MyPaperBag = GetComponentInParent<PeperBagController>();
        MyDragable = GetComponent<Draggable>();
        MyPaperCont = GetComponentInChildren<PaperController>();
        TextField = GetComponentInChildren<TextMeshProUGUI>();
        MyDrawShape = GetComponentInChildren<DrawShape>();
        Canvas NewPaperCanvas = GetComponentInChildren<Canvas>();
        RectTransform MyCanvasTransform = NewPaperCanvas.GetComponent<RectTransform>();
        MyCircleColider = GetComponent<CircleCollider2D>();
        MyPolyColider = GetComponentInChildren<PolygonCollider2D>();
        MainRectTransform = GetComponent<RectTransform>();
        Master MyMaster = GetComponentInParent<Master>();

        NewPaperCanvas.worldCamera = TheCamera;

        TextField.font = MyMaster.fontList[MyMaster.FontNo];
        TextField.text = CutText;
        textWidth = TextField.preferredWidth;
        TextField.GetComponent<RectTransform>().sizeDelta = new Vector2(textWidth, TextField.GetComponent<RectTransform>().sizeDelta.y);
        MyCanvasTransform.sizeDelta = new Vector2(textWidth + XSpace, MyCanvasTransform.sizeDelta.y);
        TextField.enableAutoSizing = true;

        MyMaster.TextList.Add(TextField);

        PaperImage.sprite = MyMaster.PaperImage.sprite;
        MyMaster.PaperList.Add(PaperImage);

        MyOpenScrap.GetLocalCorners(OpenCorners);
        OpenCorners[0] = new Vector3(OpenCorners[0].x - XSpace + Random.Range(-RandomSpace, RandomSpace), OpenCorners[0].y + Random.Range(-RandomSpace, RandomSpace), OpenCorners[0].z);
        OpenCorners[1] = new Vector3(OpenCorners[1].x - XSpace + Random.Range(-RandomSpace, RandomSpace), OpenCorners[1].y + Random.Range(-RandomSpace, RandomSpace), OpenCorners[1].z);
        OpenCorners[2] = new Vector3(OpenCorners[2].x + XSpace + Random.Range(-RandomSpace, RandomSpace), OpenCorners[2].y + Random.Range(-RandomSpace, RandomSpace), OpenCorners[2].z);
        OpenCorners[3] = new Vector3(OpenCorners[3].x + XSpace + Random.Range(-RandomSpace, RandomSpace), OpenCorners[3].y + Random.Range(-RandomSpace, RandomSpace), OpenCorners[3].z);
        // gameObject.GetComponentInChildren<BoxCollider2D>().size = new Vector2(textWidth, gameObject.GetComponentInChildren<BoxCollider2D>().size.y); ;
        ClosedCorners[0] = new Vector3(-40 + Random.Range(-10, 10), 0 + Random.Range(-10, 10), 0);
        ClosedCorners[1] = new Vector3(0 + Random.Range(-10, 10), 40 + Random.Range(-10, 10), 0);
        ClosedCorners[2] = new Vector3(40 + Random.Range(-10, 10), 0 + Random.Range(-10, 10), 0);
        ClosedCorners[3] = new Vector3(20 + Random.Range(-10, 10), -40 + Random.Range(-10, 10), 0);
        ClosedCorners[4] = new Vector3(-20 + Random.Range(-10, 10), -40 + Random.Range(-10, 10), 0);


        // ClosedCorners[0] = new Vector3(Random.Range(-25, -100), Random.Range(-25, 80), 0);
        // ClosedCorners[1] = new Vector3(Random.Range(-30, 100), Random.Range(25, 100), 0);
        // ClosedCorners[2] = new Vector3(Random.Range(25, 100), Random.Range(-25, 100), 0);
        // ClosedCorners[3] = new Vector3(Random.Range(-30, 100), Random.Range(-25, -100), 0);
        // ClosedCorners[4] = new Vector3(Random.Range(-30, 100), Random.Range(-25, -100), 0);

        OpenTransition = 0;

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
    void FixedUpdate()
    {
        if (!isOpen)
        {
            if (OpenTransition > 0)
            {
                OpenTransition = OpenTransition - (transitionSpeed * Time.deltaTime);
            }
            TextField.color = new Vector4(0, 0, 0, 0.25f);
        }
        else
        {
            if (OpenTransition < 1)
            {
                OpenTransition = OpenTransition + (transitionSpeed * Time.deltaTime);
            }
            else
            {
                TextField.color = new Vector4(0, 0, 0, 1);
            }

        }

        Vector3[] OpeningCorners = new Vector3[5];
        OpeningCorners[0] = new Vector3(Mathf.Lerp(ClosedCorners[0].x, OpenCorners[0].x, OpenTransition), Mathf.Lerp(ClosedCorners[0].y, OpenCorners[0].y, OpenTransition), Mathf.Lerp(ClosedCorners[0].z, OpenCorners[0].z, OpenTransition));
        OpeningCorners[1] = new Vector3(Mathf.Lerp(ClosedCorners[1].x, (OpenCorners[1].x + OpenCorners[0].x) / 2, OpenTransition), Mathf.Lerp(ClosedCorners[1].y, (OpenCorners[0].y + OpenCorners[1].y) / 2, OpenTransition), Mathf.Lerp(ClosedCorners[1].z, OpenCorners[1].z, OpenTransition));
        OpeningCorners[2] = new Vector3(Mathf.Lerp(ClosedCorners[2].x, OpenCorners[1].x, OpenTransition), Mathf.Lerp(ClosedCorners[1].y, OpenCorners[1].y, OpenTransition), Mathf.Lerp(ClosedCorners[1].z, OpenCorners[1].z, OpenTransition));
        OpeningCorners[3] = new Vector3(Mathf.Lerp(ClosedCorners[3].x, OpenCorners[2].x, OpenTransition), Mathf.Lerp(ClosedCorners[2].y, OpenCorners[2].y, OpenTransition), Mathf.Lerp(ClosedCorners[2].z, OpenCorners[2].z, OpenTransition));
        OpeningCorners[4] = new Vector3(Mathf.Lerp(ClosedCorners[4].x, OpenCorners[3].x, OpenTransition), Mathf.Lerp(ClosedCorners[3].y, OpenCorners[3].y, OpenTransition), Mathf.Lerp(ClosedCorners[3].z, OpenCorners[3].z, OpenTransition));
        MyDrawShape.DrawShapes(OpeningCorners);

        TextField.characterSpacing = Mathf.Lerp(-40, 0, OpenTransition);

        if (openTheScrap)
        {
            // MyPolyColider.enabled = false;
            // MyCircleColider.enabled = true;
            OpenScap();
            // MyDragable.MyRb.freezeRotation = true;
            openTheScrap = false;
        }
        if (closeTheScrap)
        {
            // MyPolyColider.enabled = true;
            // MyCircleColider.enabled = false;
            CloseScap();
            // MyDragable.MyRb.freezeRotation = false;
            closeTheScrap = false;

        }
    }

    void OpenScap()
    {
        MyDragable.MyMaster.MyPictureController.play = true;
        // // Image MyImage = MyScrap.GetComponentInChildren<Image>();
        // MyOpenScrap = Instantiate(OpenScrapPrefab, gameObject.transform.position, Quaternion.identity);
        // MyOpenScrap.transform.SetParent(gameObject.transform);
        // TextField = GetComponentInChildren<TextMeshProUGUI>();
        // TextField.text = CutText;
        // Canvas NewPaperCanvas = MyOpenScrap.GetComponent<Canvas>();
        // NewPaperCanvas.worldCamera = TheCamera;


        // if (MyScrap != null)
        // {
        //     Destroy(MyScrap);
        // }
        isOpen = true;
        MyPaperBag.MyLineController.EnablePoints(true);
        MyPaperBag.MyLineController.CheckSize(TextField.preferredWidth);
    }

    void CloseScap()
    {
        // MyScrap = Instantiate(ScrapPrefab, MyOpenScrap.transform.position, gameObject.transform.rotation);
        // MyScrap.transform.SetParent(gameObject.transform);
        // Canvas NewPaperCanvas = MyScrap.GetComponent<Canvas>();
        // NewPaperCanvas.worldCamera = TheCamera;


        // if (MyOpenScrap != null)
        // {
        //     Destroy(MyOpenScrap);
        // }
        MyDragable.MyMaster.MyPictureController.play = true;
        isOpen = false;

        MyPaperBag.MyLineController.EnablePoints(false);
    }
}
