
using UnityEngine;
using UnityEngine.Android;

public class ARconnect : MonoBehaviour
{
    public PlaceOnTap myPlaceOnTap;

    public GameObject poemToPlace;

    public Vector3 ARscale;
    public Vector3 ARscaleAdjusted;
    public Vector3 ARscaleIncrement;
    public float YrotationIncrement;


    public Transform ARpoemCanvas;
    public static ARconnect Instance;

    public Master myMaster;

    public ArTextController myTextController;





    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject);  // Optional: Keeps the object across scenes
        }
        // else
        // {
        //     Destroy(gameObject);
        // }
        // CheckAndDestroy();
    }

    void Start()
    {
        // Check and request camera permission
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            Permission.RequestUserPermission(Permission.Camera);
        }
        else
        {
            InitializeAR();
        }
    }

    void InitializeAR()
    {
        // Code to initialize AR features
        Debug.Log("AR Initialized!");
    }

    public void setARpoem(GameObject poem)
    {
        GameObject NewPoem = Instantiate(poem);
        NewPoem.transform.SetParent(ARpoemCanvas);
        poemToPlace = NewPoem;
        RectTransform newTransform = NewPoem.GetComponent<RectTransform>();
        newTransform.anchorMin = new Vector2(0.5f, 0.5f);
        newTransform.anchorMax = new Vector2(0.5f, 0.5f);

        // Set pivot to (0.5, 0.5) for center pivot
        newTransform.pivot = new Vector2(0.5f, 0.5f);

        // Optionally reset position if you want the element to be at the center of the parent
        newTransform.anchoredPosition = new Vector2(newTransform.rect.width / 2, (newTransform.rect.height / 2) * -1);
        newTransform.localScale = ARscale;

        myTextController.CheckText();
        myTextController.CheckPaper();


    }


    public void DoSomething()
    {
        Debug.Log("Doing something from the loaded scene!");
    }


    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        // if (myPlaceOnTap.aRPlaneManager.trackables.count == 0)
        // {
        //     myMaster.myTipsController.tipsText.text = "Slowly move your phone to scan for surfaces to place your poem";
        // }
        // else
        // {
        //     myMaster.myTipsController.tipsText.text = "Tap on a highlighted surface to place your poem";
        // }
    }

    public void ScaleUp()
    {
        if (poemToPlace != null)
        {
            ARscaleAdjusted = ARpoemCanvas.GetComponent<RectTransform>().localScale;
            Vector3 newScale = ARscaleAdjusted + ARscaleIncrement;
            ARpoemCanvas.GetComponent<RectTransform>().localScale = newScale;
        }

    }

    public void ScaleDown()
    {
        if (poemToPlace != null)
        {
            ARscaleAdjusted = ARpoemCanvas.GetComponent<RectTransform>().localScale;
            Vector3 newScale = ARscaleAdjusted - ARscaleIncrement;
            ARpoemCanvas.GetComponent<RectTransform>().localScale = newScale;
        }
    }

    public void leftY()
    {
        if (poemToPlace != null)
        {
            Vector3 currentRotation = ARpoemCanvas.GetComponent<RectTransform>().eulerAngles;

            // Add the increment to the Y rotation
            currentRotation.y += YrotationIncrement;

            // Set the new rotation back to the RectTransform
            ARpoemCanvas.GetComponent<RectTransform>().eulerAngles = currentRotation;
        }

    }

    public void rightY()
    {
        if (poemToPlace != null)
        {
            Vector3 currentRotation = ARpoemCanvas.GetComponent<RectTransform>().eulerAngles;

            // Add the increment to the Y rotation
            currentRotation.y -= YrotationIncrement;

            // Set the new rotation back to the RectTransform
            ARpoemCanvas.GetComponent<RectTransform>().eulerAngles = currentRotation;
        }

    }

    public void upX()
    {
        if (poemToPlace != null)
        {
            Vector3 currentRotation = ARpoemCanvas.GetComponent<RectTransform>().eulerAngles;

            // Add the increment to the Y rotation
            currentRotation.x += YrotationIncrement;

            // Set the new rotation back to the RectTransform
            ARpoemCanvas.GetComponent<RectTransform>().eulerAngles = currentRotation;
        }

    }

    public void downX()
    {
        if (poemToPlace != null)
        {
            Vector3 currentRotation = ARpoemCanvas.GetComponent<RectTransform>().eulerAngles;

            // Add the increment to the Y rotation
            currentRotation.x -= YrotationIncrement;

            // Set the new rotation back to the RectTransform
            ARpoemCanvas.GetComponent<RectTransform>().eulerAngles = currentRotation;
        }

    }


}
