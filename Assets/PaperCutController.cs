using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PaperCutController : MonoBehaviour
{
    public int aa;
    public GameObject ScrapPrefab;
    public GameObject OpenScrapPrefab;

    public GameObject MyScrap;
    public GameObject MyOpenScrap;

    public TextMeshProUGUI TextField;
    // public RectTransform MyCanvasTransform;
    public string CutText;

    public Camera TheCamera;

    public bool openTheScrap;
    public bool closeTheScrap;

    public RectTransform TheMousePoint;
    public bool isOpen;

    public float textWidth;
    public PeperBagController MyPaperBag;

    public Draggable MyDragable;
    // Start is called before the first frame update
    void Start()
    {
        // CloseScap();
        MyPaperBag = GetComponentInParent<PeperBagController>();
        MyDragable = GetComponent<Draggable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (openTheScrap)
        {
            OpenScap();
            openTheScrap = false;
        }
        if (closeTheScrap)
        {
            CloseScap();
            closeTheScrap = false;
        }
    }

    void OpenScap()
    {
        // Image MyImage = MyScrap.GetComponentInChildren<Image>();
        MyOpenScrap = Instantiate(OpenScrapPrefab, gameObject.transform.position, Quaternion.identity);
        MyOpenScrap.transform.SetParent(gameObject.transform);
        TextField = GetComponentInChildren<TextMeshProUGUI>();
        TextField.text = CutText;
        Canvas NewPaperCanvas = MyOpenScrap.GetComponent<Canvas>();
        NewPaperCanvas.worldCamera = TheCamera;
        AdjustXsize();

        if (MyScrap != null)
        {
            Destroy(MyScrap);
        }
        isOpen = true;
        MyPaperBag.MyLineController.EnablePoints(true);
        MyPaperBag.MyLineController.CheckSize(TextField.preferredWidth);
    }

    void CloseScap()
    {
        MyScrap = Instantiate(ScrapPrefab, MyOpenScrap.transform.position, gameObject.transform.rotation);
        MyScrap.transform.SetParent(gameObject.transform);
        Canvas NewPaperCanvas = MyScrap.GetComponent<Canvas>();
        NewPaperCanvas.worldCamera = TheCamera;

        if (MyOpenScrap != null)
        {
            Destroy(MyOpenScrap);
        }
        isOpen = false;

        MyPaperBag.MyLineController.EnablePoints(false);
    }

    void AdjustXsize()
    {
        textWidth = TextField.preferredWidth;
        TextField.GetComponent<RectTransform>().sizeDelta = new Vector2(textWidth, TextField.GetComponent<RectTransform>().sizeDelta.y);
        RectTransform MyCanvasTransform = MyOpenScrap.GetComponent<RectTransform>();
        MyCanvasTransform.sizeDelta = new Vector2(textWidth + 20, MyCanvasTransform.sizeDelta.y);
        PaperController MyPaperCont =  MyOpenScrap.GetComponentInChildren<PaperController>();
        MyPaperCont.FindShape();
        // gameObject.GetComponentInChildren<BoxCollider2D>().size = new Vector2(textWidth, gameObject.GetComponentInChildren<BoxCollider2D>().size.y); ;
    }
}
