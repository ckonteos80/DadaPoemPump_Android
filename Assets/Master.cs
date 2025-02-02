using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class Master : MonoBehaviour
{

    public int stage;


    public Button PasteButton;
    public Button CutButton;
    public Button PlaceButton;
    public Button SaveButton;

    public List<drawButton> bottomButtons;


    public GameObject Menu;
    public GameObject Text1;
    public GameObject Text1_5;
    public GameObject Text2;
    public GameObject Text3;
    public GameObject Text4;


    public GameObject TopMask;
    public GameObject BottomMask;

    public GameObject BottomButton;

    public GameObject Particles;

    public Image BackgroundImage;

    // public GameObject WallPaperScrap;

    public GameObject TextGameobj;

    public ShakeDetector MyShake;

    public float baseYSpace;

    public float textXspace;

    public RectTransform TextItems;
    public RectTransform CutItems;

    public Vector2 CutItemsPos;



    TextMeshProUGUI Text1text;
    public float diagonalAngle;

    public RectTransform Picture;
    public RectTransform PictureFrame;

    public TextMeshProUGUI InputText;
    public int FontNo;
    public List<TMP_FontAsset> fontList = new List<TMP_FontAsset>();
    public List<TextMeshProUGUI> TextList = new List<TextMeshProUGUI>();
    public List<TextMeshProUGUI> TextListAR = new List<TextMeshProUGUI>();

    public List<Sprite> PaperTexturesList = new List<Sprite>();
    public List<Image> PaperList = new List<Image>();
    public List<Image> PaperListAR = new List<Image>();

    public ParticleController myParticleContr;

    public Image PaperImage;

    public Color PaperColor;



    public PictureController MyPictureController;

    public Camera TheCamera;

    public LineController TheLineController;

    public bool drawButtons;

    public drawButton StartButtonDraw;

    public RectTransform menuButton;

    public hidenButtonsController myHidenButtons;

    public GameObject confirmPrefrab;
    public GameObject theConfirm;
    public PictureController startPictureController;
    TextControll myTextControll;

    public GameObject ARMenu;

    public GameObject poemForAr;

    public GameObject colorBGbutton;

    public TextMeshProUGUI ARsetButtonText;
    public Button ARsetButton;

    public tipsController myTipsController;

    public GameObject textPaperToCut;

    public List<GameObject> infoPrefab;

    NewsAPI myNewsAPI;

    public GameObject findNewsButton;

    public GameObject landscapeWarningPrefab;

    public GameObject landscapeWarning;

    // public Image rotationIndicator;

    public Transform stages;

    public ShakeDetector myShakeDetector;

    public float timer;
    public float targetTime = 2f;



    // 1 Copy an article of the length you want to make your poem and Paste the article on the app.

    // 1 carefully cut out each of the words that makes up this article and put them all in a bag.
    // 2 Shake gently.
    // 5 Next take out each cutting one after the other.
    // 6 The poem will resemble you.

    // Take a newspaper.
    // Take some scissors.
    // Choose from this paper an article of the length you want to make your poem.
    // Cut out the article.
    // Next carefully cut out each of the words that makes up this article and put them all in a bag.
    // Shake gently.
    // Next take out each cutting one after the other.
    // Copy conscientiously in the order in which they left the bag.
    // The poem will resemble you.
    // And there you are—an infinitely original author of charming sensibility, even though unappreciated by the vulgar herd.

    // Start is called before the first frame update
    void Start()
    {
        UniClipboard.SetText("");
        textXspace = Screen.width - 250;
        MyPictureController = GetComponentInChildren<PictureController>();

        MyShake = GetComponentInChildren<ShakeDetector>();

        Text1text = Text1.GetComponent<TextMeshProUGUI>();
        myNewsAPI = GetComponent<NewsAPI>();

        SetStage(1);

        FontNo = Random.Range(0, 3);
        InputText.font = fontList[FontNo];

        PaperImage.sprite = PaperTexturesList[Random.Range(0, PaperTexturesList.Count)];

        diagonalAngle = Screen.height / 8;

        TextItems.position = new Vector3(TextItems.position.x, Screen.height - (baseYSpace + diagonalAngle) - 100, TextItems.position.z);
        TextItems.sizeDelta = new Vector2(TextItems.sizeDelta.x, Screen.height - baseYSpace * 2 - diagonalAngle);

        CutItems.position = new Vector3(CutItems.position.x + 75, Screen.height - (baseYSpace + diagonalAngle) - 50, CutItems.position.z);
        CutItems.sizeDelta = new Vector2(Screen.width - 150, Screen.height - baseYSpace * 2 - diagonalAngle);
        CutItemsPos = CutItems.position;


        Picture.transform.position = new Vector3(Picture.transform.position.x, Screen.height - baseYSpace - diagonalAngle, 0);
        RectTransform PictureRect = Picture.GetComponent<RectTransform>();
        RectTransform PictureRectFrame = PictureFrame.GetComponent<RectTransform>();
        PictureRect.sizeDelta = new Vector2(diagonalAngle + 50, diagonalAngle + 50);
        PictureRectFrame.sizeDelta = new Vector2(diagonalAngle + 50, diagonalAngle + 50);
        menuButton.anchoredPosition += new Vector2(0, diagonalAngle / 5);
        // Picture.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, diagonalAngle + 50);
        // PictureFrame.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, diagonalAngle + 50);

        myHidenButtons.ColorChange(Color.white);
        myTextControll = GetComponentInChildren<TextControll>();
        ARMenu.SetActive(false);

        myTipsController = GetComponentInChildren<tipsController>();



        PaperColor = Color.white;



    }

    // Update is called once per frame
    void Update()
    {
        if (theConfirm != null)
        {
            confirmController myConfirmController = theConfirm.GetComponent<confirmController>();
            if (myConfirmController.confirmed)
            {
                myTextControll.inputField.text = "";
                myTextControll.words.Clear();

                for (int i = 0; i < PaperList.Count; i++)
                {
                    if (PaperList != null)
                    {
                        Destroy(PaperList[i]);
                    }
                }
                PaperList.Clear();

                TheLineController.ClearLines();
                TheLineController.setFirstLine();
                myTextControll.BagController.ClearScraps();


                myTextControll.preferredHeight = myTextControll.inputField.preferredHeight;


                myTextControll.InputRectTransform.sizeDelta = new Vector2(myTextControll.InputRectTransform.sizeDelta.x, Mathf.Max(myTextControll.preferredHeight, myTextControll.minHeight));
                myTextControll.TextBGmIage.sizeDelta = new Vector2(myTextControll.TextBGmIage.sizeDelta.x, 200);

                // Set the initial height of the input field
                //   InputRectTransform.sizeDelta = new Vector2(InputRectTransform.sizeDelta.x, Mathf.Max(preferredHeight, minHeight));


                myTextControll.MyPaper.FindShape();

                SetStage(1);

                theConfirm = null;
                hideMenu();
                Debug.Log("cleared");
                // Destroy(myMaster.theConfirm.gameObject);
                PaperColor = Color.white;
            }
        }

        if (stage == 1)
        {
            if (myNewsAPI.title != "" & myNewsAPI.description != "")
            {
                findNewsButton.SetActive(true);
            }
            else
            {
                findNewsButton.SetActive(false);
            }

            PasteButton.gameObject.SetActive(true);
            string clipboardText = UniClipboard.GetText();
            if (clipboardText != "" && clipboardText.Length > 5)
            {

                PasteButton.interactable = true;

                Text1text.text = "Paste the article to the app";



                if (myTextControll.inputField.text == "")
                {
                    if (!myTextControll.spawningCuts)
                    {
                        // myTipsController.tipsText.text = "Tap the PASTE button to continue";
                        myTipsController.ChangeText("Tap the PASTE button to continue", myTipsController.centerPos);
                        myTipsController.changeFlashingText(myTipsController.pasteButtonText, null);
                    }
                    else
                    {
                        myTipsController.tipsText.text = "";
                        myTipsController.changeFlashingText(null, null);
                    }
                }
                else
                {
                    if (!myTextControll.spawningCuts)
                    {
                        myTipsController.ChangeText("Tap the CUT button to cut the article into scraps", myTipsController.upPos);
                        //    myTipsController.tipsText.text = "Tap the CUT button to cut the article into scraps";
                        myTipsController.changeFlashingText(myTipsController.cutButtonText, null);
                    }
                    else
                    {
                        myTipsController.tipsText.text = "";
                        myTipsController.changeFlashingText(null, null);
                    }
                }

            }
            else
            {
                if (myTextControll.inputField.text == "")
                {


                    Text1text.text = "Copy an article of the length you want to make your poem ";
                    PasteButton.interactable = false;

                    if (!findNewsButton.activeSelf)
                    {
                        myTipsController.ChangeText("Leave the app and visit a truthfull news source. Copy an article of your choise. Come back to this app. I will be here waiting", myTipsController.centerPos);
                    }
                    else
                    {
                        myTipsController.ChangeText("Leave the app and visit a truthfull news source. Copy an article of your choise. Come back to this app. I will be here waiting. Or tap the FIND button to get a random short artiicle.", myTipsController.centerPos);
                    }

                    //   myTipsController.tipsText.text = "Leave the app and visit a truthfull news source. Copy an article of your choise. Come back to this app. I will be here waiting";
                }
                else
                {
                    if (!myTextControll.spawningCuts)
                    {
                        myTipsController.ChangeText("Tap the CUT button to cut the article into scraps", myTipsController.upPos);
                        //    myTipsController.tipsText.text = "Tap the CUT button to cut the article into scraps";
                        myTipsController.changeFlashingText(myTipsController.cutButtonText, null);
                    }
                    else
                    {
                        myTipsController.tipsText.text = "";
                        myTipsController.changeFlashingText(null, null);
                    }
                }
            }

        }
        if (drawButtons)
        {
            for (int i = 0; i < bottomButtons.Count; i++)
            {

                bottomButtons[i].startDraw = true;


            }
            drawButtons = false;
        }


        if (IsSceneLoaded("ARnew"))
        {
            Screen.orientation = ScreenOrientation.AutoRotation;

            if (ARconnect.Instance != null)
            {
                if (ARconnect.Instance.myPlaceOnTap.isPlacing)
                {
                    ARsetButtonText.text = "SET";
                }
                else
                {
                    ARsetButtonText.text = "RESET";
                }

                if (ARconnect.Instance.myPlaceOnTap.Placed)
                {
                    ARsetButton.interactable = true;
                }
                else
                {
                    ARsetButton.interactable = false;
                }
            }
        }
        else
        {
            Screen.orientation = ScreenOrientation.Portrait;

            if (!myShakeDetector.shaking)
            {
                if (Input.deviceOrientation == DeviceOrientation.Portrait)
                {
                    // rotationIndicator.color = Color.white;
                    if (landscapeWarning != null)
                    {
                        Destroy(landscapeWarning);
                    }
                    timer = 0f;
                }

                if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft || Input.deviceOrientation == DeviceOrientation.LandscapeRight || Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown)
                {
                    timer += Time.deltaTime;

                    // Check if the timer has reached the target time
                    if (timer >= targetTime)
                    {
                        RectTransform rectTransform = null;
                        // rotationIndicator.color = Color.red;
                        if (landscapeWarning == null)
                        {
                            landscapeWarning = Instantiate(landscapeWarningPrefab);
                            landscapeWarning.transform.SetParent(stages.transform);
                            rectTransform = landscapeWarning.GetComponent<RectTransform>();

                            rectTransform.anchorMin = new Vector2(0, 0);  // Bottom-left corner
                            rectTransform.anchorMax = new Vector2(1, 1);  // Top-right corner

                            // Reset the offsets to ensure it fills the entire screen
                            rectTransform.offsetMin = Vector2.zero;  // Bottom and Left offsets
                            rectTransform.offsetMax = Vector2.zero;  // Top and Right offsets


                            // Reset position, rotation, and scale
                            rectTransform.anchoredPosition = Vector2.zero;
                            rectTransform.localRotation = Quaternion.identity;
                            rectTransform.localScale = Vector3.one;
                        }
                        if (landscapeWarning != null)
                        {

                            TextMeshProUGUI warningText = rectTransform.GetComponentInChildren<TextMeshProUGUI>();
                            if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft)
                            {
                                warningText.text = "Dont turn to the Left";
                                RectTransform warningRect = warningText.GetComponent<RectTransform>();
                                warningRect.localRotation = Quaternion.Euler(0, 0, -90);
                            }
                            if (Input.deviceOrientation == DeviceOrientation.LandscapeRight)
                            {
                                warningText.text = "Dont turn to the Right";
                                RectTransform warningRect = warningText.GetComponent<RectTransform>();
                                warningRect.localRotation = Quaternion.Euler(0, 0, 90);
                            }
                            if (Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown)
                            {
                                warningText.text = "Turn your life upside down not your phone";
                                RectTransform warningRect = warningText.GetComponent<RectTransform>();
                                warningRect.localRotation = Quaternion.Euler(0, 0, -180);
                            }

                        }
                    }
                }
            }
        }

        if (stage == 2)
        {
            // if (MyShake.hasShaked)
            // {
            //     myTipsController.tipsText.text = "Tap the PLACE button to continue";
            //     myTipsController.changeFlashingText(myTipsController.placeButtonText, null);
            // }
            // else
            // {
            //     myTipsController.tipsText.text = "Dont be afraid, shake your phone";
            //     myTipsController.changeFlashingText(null, null);
            // }
        }




    }

    public void SetStage(int NewStage)
    {

        if (NewStage == 1)
        {
            TextGameobj.SetActive(true);
            textPaperToCut.SetActive(true);
            PasteButton.gameObject.SetActive(false);
            CutButton.gameObject.SetActive(false);
            PlaceButton.gameObject.SetActive(false);
            SaveButton.gameObject.SetActive(false);

            Text1.gameObject.SetActive(true);
            Text1_5.gameObject.SetActive(false);
            Text2.gameObject.SetActive(false);
            Text3.gameObject.SetActive(false);
            Text4.gameObject.SetActive(false);


            // WallPaperScrap.SetActive(false);
            // WallPaperScrap.transform.position = new Vector3(WallPaperScrap.transform.position.x, WallPaperScrap.transform.position.y - (Screen.height / 2), WallPaperScrap.transform.position.z);
            // TextGameobj.SetActive(false);

            MyShake.CanShake = false;

            RectTransform Stage1TextRect = Text1.GetComponent<RectTransform>();
            if (Stage1TextRect != null)
            {

                TextMeshProUGUI myText = Stage1TextRect.GetComponent<TextMeshProUGUI>();
                if (myText.preferredWidth < textXspace)
                {
                    Stage1TextRect.sizeDelta = new Vector2(myText.preferredWidth, myText.preferredHeight);

                }
                else
                {
                    Stage1TextRect.sizeDelta = new Vector2(textXspace, myText.preferredHeight);
                }

            }

            stage = 1;
            drawButtons = true;
        }
        if (NewStage == 15)
        {


            // MyPictureController.play = true;
            if (!myTextControll.spawningCuts)
            {


                PasteButton.gameObject.SetActive(false);
                CutButton.gameObject.SetActive(true);
                PlaceButton.gameObject.SetActive(false);
                SaveButton.gameObject.SetActive(false);

                Text1_5.gameObject.SetActive(true);
                Text1.gameObject.SetActive(false);

                Text2.gameObject.SetActive(false);
                Text3.gameObject.SetActive(false);
                Text4.gameObject.SetActive(false);
            }
            else
            {
                PasteButton.gameObject.SetActive(false);
                CutButton.gameObject.SetActive(false);
                PlaceButton.gameObject.SetActive(false);
                SaveButton.gameObject.SetActive(false);

                Text1_5.gameObject.SetActive(false);
                Text1.gameObject.SetActive(false);

                Text2.gameObject.SetActive(false);
                Text3.gameObject.SetActive(false);
                Text4.gameObject.SetActive(false);

                myTipsController.tipsText.text = "";
                myTipsController.changeFlashingText(null, null);
            }

            // WallPaperScrap.SetActive(false);
            // WallPaperScrap.transform.position = new Vector3(WallPaperScrap.transform.position.x, WallPaperScrap.transform.position.y - (Screen.height / 2), WallPaperScrap.transform.position.z);
            // TextGameobj.SetActive(false);

            MyShake.CanShake = false;
            RectTransform Stage1_5TextRect = Text1_5.GetComponent<RectTransform>();
            if (Stage1_5TextRect != null)
            {
                TextMeshProUGUI myText = Text1_5.GetComponent<TextMeshProUGUI>();
                if (myText.preferredWidth < textXspace)
                {
                    Stage1_5TextRect.sizeDelta = new Vector2(myText.preferredWidth, myText.preferredHeight);

                }
                else
                {
                    Stage1_5TextRect.sizeDelta = new Vector2(textXspace, myText.preferredHeight);
                }
            }



            stage = 1;
        }
        if (NewStage == 2)
        {
            MyPictureController.play = true;
            PasteButton.gameObject.SetActive(false);
            CutButton.gameObject.SetActive(false);
            PlaceButton.gameObject.SetActive(true);
            SaveButton.gameObject.SetActive(false);

            Text1.gameObject.SetActive(false);
            Text1_5.gameObject.SetActive(false);
            Text2.gameObject.SetActive(true);
            Text3.gameObject.SetActive(false);
            Text4.gameObject.SetActive(false);

            // WallPaperScrap.SetActive(true);
            // WallPaperScrap.transform.position = new Vector3(WallPaperScrap.transform.position.x, WallPaperScrap.transform.position.y + (Screen.height / 2), WallPaperScrap.transform.position.z);

            RectTransform Stage2textRect = Text2.GetComponent<RectTransform>();
            if (Stage2textRect != null)
            {
                TextMeshProUGUI myText = Text2.GetComponent<TextMeshProUGUI>();
                if (myText.preferredWidth < textXspace)
                {
                    Stage2textRect.sizeDelta = new Vector2(myText.preferredWidth, myText.preferredHeight);

                }
                else
                {
                    Stage2textRect.sizeDelta = new Vector2(textXspace, myText.preferredHeight);
                }
            }

            // if (MyShake.hasShaked)
            // {
            //     myTipsController.tipsText.text = "Tap the PLACE button to continue";
            //     myTipsController.changeFlashingText(myTipsController.placeButtonText, null);
            // }
            // else
            // {
            //     myTipsController.tipsText.text = "Dont be afraid, shake";
            //     myTipsController.changeFlashingText(null, null);
            // }



            TextGameobj.SetActive(false);
            MyShake.CanShake = true;
            stage = 2;
        }
        if (NewStage == 3)
        {
            MyPictureController.play = true;
            PasteButton.gameObject.SetActive(false);
            CutButton.gameObject.SetActive(false);
            PlaceButton.gameObject.SetActive(false);
            SaveButton.gameObject.SetActive(false);

            Text1.gameObject.SetActive(false);
            Text1_5.gameObject.SetActive(false);
            Text2.gameObject.SetActive(false);
            Text3.gameObject.SetActive(true);
            Text4.gameObject.SetActive(false);

            RectTransform Stage3textRect = Text3.GetComponent<RectTransform>();
            if (Stage3textRect != null)
            {
                TextMeshProUGUI myText = Text3.GetComponent<TextMeshProUGUI>();
                if (myText.preferredWidth < Screen.width - 50)
                {
                    Stage3textRect.sizeDelta = new Vector2(myText.preferredWidth, myText.preferredHeight);

                }
                else
                {
                    Stage3textRect.sizeDelta = new Vector2(Screen.width - 50, myText.preferredHeight);
                }
            }



            TextGameobj.SetActive(false);
            MyShake.CanShake = false;
            stage = 3;
        }

        if (NewStage == 4)
        {
            MyPictureController.play = true;
            PasteButton.gameObject.SetActive(false);
            CutButton.gameObject.SetActive(false);
            PlaceButton.gameObject.SetActive(false);
            SaveButton.gameObject.SetActive(true);

            Text1.gameObject.SetActive(false);
            Text1_5.gameObject.SetActive(false);
            Text2.gameObject.SetActive(false);
            Text3.gameObject.SetActive(false);
            Text4.gameObject.SetActive(true);

            // WallPaperScrap.SetActive(false);
            // WallPaperScrap.transform.position = new Vector3(WallPaperScrap.transform.position.x, WallPaperScrap.transform.position.y - (Screen.height / 2), WallPaperScrap.transform.position.z);
            RectTransform Stage4textRect = Text4.GetComponent<RectTransform>();
            if (Stage4textRect != null)
            {
                TextMeshProUGUI myText = Text4.GetComponent<TextMeshProUGUI>();
                if (myText.preferredWidth < textXspace)
                {
                    Stage4textRect.sizeDelta = new Vector2(myText.preferredWidth, myText.preferredHeight);

                }
                else
                {
                    Stage4textRect.sizeDelta = new Vector2(textXspace, myText.preferredHeight);
                }
            }

            myTipsController.ChangeText("Tap on the MORE icon for more fun", myTipsController.upPos);
            //   myTipsController.tipsText.text = "Tap on the MORE icon for more fun";
            myTipsController.changeFlashingText(myTipsController.MoreButtonText, null);

            TextGameobj.SetActive(false);
            MyShake.CanShake = false;
            stage = 4;
        }
    }

    public void StartPlace()
    {
        SetStage(3);
    }

    public void ChangeFontsButtonPress()
    {
        if (IsSceneLoaded("ARnew"))
        {
            ChangeFontsAR();
            ChangeFonts();
        }
        else
        {
            ChangeFonts();
        }

    }

    public void ChangePaperButtonPress()
    {
        if (IsSceneLoaded("ARnew"))
        {
            ChangePaperAR();
            // ChangePaper();
        }
        else
        {
            ChangePaper();
        }

    }

    public void ChangeFonts()
    {
        MyPictureController.play = true;
        FontNo = FontNo + 1;
        if (FontNo >= fontList.Count)
        {
            FontNo = 0;
        }
        for (int i = 0; i < TextList.Count; i++)
        {

            TextList[i].font = fontList[FontNo];
        }
        InputText.font = fontList[FontNo];

    }

    public void ChangeFontsAR()
    {
        // MyPictureController.play = true;
        FontNo = FontNo + 1;
        if (FontNo >= 3)
        {
            FontNo = 0;
        }
        for (int i = 0; i < TextListAR.Count; i++)
        {

            TextListAR[i].font = fontList[FontNo];

        }

        //   InputText.font = fontList[FontNo];

    }

    public void ChangePaper()
    {
        MyPictureController.play = true;

        if (PaperColor != Color.white)
        {
            PaperColor = Color.white;
            PaperImage.color = PaperColor;
        }
        else
        {
            Color randomColor = new Color(Random.value, Random.value, Random.value, 1f);
            // Apply the random color to the Image component
            PaperColor = randomColor;
            PaperImage.color = PaperColor;
        }

        if (PaperTexturesList.Count != 0)
        {
            int r = Random.Range(0, PaperTexturesList.Count);
            PaperImage.sprite = PaperTexturesList[r];
            for (int i = 0; i < PaperList.Count; i++)
            {
                if (PaperList[i] != null)
                {
                    PaperList[i].sprite = PaperTexturesList[r];
                    PaperList[i].color = PaperColor;
                }

            }
        }
    }

    public void ChangePaperAR()
    {
        //  MyPictureController.play = true;

        if (PaperColor != Color.white)
        {
            PaperColor = Color.white;
            PaperImage.color = PaperColor;
        }
        else
        {
            Color randomColor = new Color(Random.value, Random.value, Random.value, 1f);
            // Apply the random color to the Image component
            PaperColor = randomColor;
            PaperImage.color = PaperColor;
        }

        if (PaperTexturesList.Count != 0)
        {
            int r = Random.Range(0, PaperTexturesList.Count);
            PaperImage.sprite = PaperTexturesList[r];
            for (int i = 0; i < PaperListAR.Count; i++)
            {
                if (PaperListAR[i] != null)
                {
                    PaperListAR[i].sprite = PaperTexturesList[r];
                    PaperListAR[i].color = PaperColor;
                }

            }
        }
    }


    public void Hide()
    {

        if (stage == 4)
        {
            TopMask.SetActive(false);
            BottomMask.SetActive(false);
            Text4.SetActive(false);
            BottomButton.SetActive(false);

            myTipsController.ChangeText("Tap AR to add your poem to the real world", myTipsController.upPosButtons);
            //   myTipsController.tipsText.text = "Add your poem to the real world with the AR button";
            myTipsController.changeFlashingText(myTipsController.ARbuttonText, null);
            // CutItems.anchoredPosition = new Vector2(50, -250);
        }


    }

    public void Show()
    {
        if (stage == 4)
        {
            TopMask.SetActive(true);
            BottomMask.SetActive(true);
            Text4.SetActive(true);
            BottomButton.SetActive(true);

            CutItems.position = new Vector3(150, Screen.height - (baseYSpace + diagonalAngle) - 50, CutItems.position.z);
            drawButtons = true;

            myTipsController.ChangeText("Tap on the MORE icon for more fun", myTipsController.upPos);
            //   myTipsController.tipsText.text = "Tap on the MORE icon for more fun";
            myTipsController.changeFlashingText(myTipsController.MoreButtonText, null);
            // CutItems.anchoredPosition = new Vector2(CutItemsPos.x, CutItemsPos.y);
        }



    }

    public void particles()
    {
        // MyPictureController.play = true;
        // if (myParticleContr.CurrentParticle != null)
        // {
        //     Destroy(myParticleContr.CurrentParticle);
        //     myParticleContr.CurrentParticle = null;
        // }
        // else
        // {
        myParticleContr.SetParticles();
        // }
        // Particles.SetActive(!Particles.activeSelf);

    }

    public void ChangeColor()
    {
        MyPictureController.play = true;
        if (BackgroundImage.color != Color.black)
        {
            myHidenButtons.ColorChange(Color.white);
            BackgroundImage.color = Color.black;
        }
        else
        {
            myHidenButtons.ColorChange(Color.black);
            Color randomColor = new Color(Random.value, Random.value, Random.value, 1f);
            // Apply the random color to the Image component
            BackgroundImage.color = randomColor;
        }

    }

    public void showMenu()
    {
        Menu.SetActive(true);
        startPictureController.play = true;
        StartButtonDraw.runAfterStart = true;

    }

    public void hideMenu()
    {
        Menu.SetActive(false);
        drawButtons = true;

    }

    // public void StartWithText(string text)
    // {
    //     hideMenu();
    //     myTextControll.inputField.text = text;
    //     SetStage(15);

    // }

    public void showInfo(int infoNo)
    {
        GameObject newInffo = Instantiate(infoPrefab[infoNo]);
        newInffo.transform.SetParent(Menu.transform.parent);

        // infoController myInfo = newInffo.GetComponent<infoController>();
        // myInfo.infoText.text = info;
        // myInfo.headlineText.text = headline;

        RectTransform rectTransform = newInffo.GetComponent<RectTransform>();

        rectTransform.anchorMin = new Vector2(0, 0);  // Bottom-left corner
        rectTransform.anchorMax = new Vector2(1, 1);  // Top-right corner

        // Reset the offsets to ensure it fills the entire screen
        rectTransform.offsetMin = Vector2.zero;  // Bottom and Left offsets
        rectTransform.offsetMax = Vector2.zero;  // Top and Right offsets


        // Reset position, rotation, and scale
        rectTransform.anchoredPosition = Vector2.zero;
        rectTransform.localRotation = Quaternion.identity;
        rectTransform.localScale = Vector3.one;





    }

    public void ShowAR()
    {
        if (!IsSceneLoaded("ARnew"))
        {
            // If the scene is not loaded, load it additively
            LoadSceneAdditive("ARnew");
            BackgroundImage.gameObject.SetActive(false);
            myHidenButtons.hideHidden();
            ARMenu.SetActive(true);
            myHidenButtons.ColorChange(Color.black);
            colorBGbutton.SetActive(false);

            // myTipsController.showingTips = false;






        }

    }
    public void HideAR()
    {
        if (SceneManager.GetSceneByName("ARnew").isLoaded)
        {
            myTipsController.ChangeText("Tap AR to add your poem to the real world", myTipsController.upPosButtons);
            // myTipsController.tipsText.text = "Add your poem to the real world with the AR button";
            myTipsController.changeFlashingText(myTipsController.ARbuttonText, null);

            SceneManager.UnloadSceneAsync("ARnew");
            BackgroundImage.gameObject.SetActive(true);
            myHidenButtons.unhideHidden();
            ARMenu.SetActive(false);
            poemForAr.SetActive(true);
            colorBGbutton.SetActive(true);

            if (BackgroundImage.color == Color.black)
            {
                myHidenButtons.ColorChange(Color.white);

            }
            else
            {
                myHidenButtons.ColorChange(Color.black);

            }

            TextListAR.Clear();
            PaperListAR.Clear();



            Destroy(ARconnect.Instance);


        }

    }

    bool IsSceneLoaded(string sceneName)
    {
        // Loop through all the loaded scenes
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            // If the scene is found, return true
            if (scene.name == sceneName)
            {
                return true;
            }
        }
        return false; // Scene not loaded
    }

    void LoadSceneAdditive(string sceneName)
    {
        // Load the scene in additive mode
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void UnloadScene(int sceneIndex)
    {

    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Perform actions after the scene is loaded
        if (ARconnect.Instance != null)
        {
            // ARconnect.Instance.poemToPlace = poemForAr;
            //  ARconnect.Instance.poemToPlace.transform.SetParent(ARconnect.Instance.transform);
            ARconnect.Instance.DoSomething();

            // ARconnect.Instance.myPlaceOnTap.PrefabToPlace = ARconnect.Instance.poemToPlace;
            ARconnect.Instance.myMaster = this;
            ARconnect.Instance.setARpoem(poemForAr);

            poemForAr.SetActive(false);

        }


        // // If you no longer need to handle scene loaded events, you can unsubscribe
        // SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void scaleUpARbutton()
    {
        ARconnect.Instance.ScaleUp();
    }
    public void scaleDownARbutton()
    {
        ARconnect.Instance.ScaleDown();
    }

    public void leftYARbutton()
    {
        ARconnect.Instance.leftY();
    }
    public void rightYARbutton()
    {
        ARconnect.Instance.rightY();
    }
    public void upXARbutton()
    {
        ARconnect.Instance.upX();
    }
    public void downXARbutton()
    {
        ARconnect.Instance.downX();
    }

    public void setAR()
    {
        myTipsController.tipsText.text = "";
        myTipsController.changeFlashingText(null, null);
        ARconnect.Instance.myPlaceOnTap.isPlacing = !ARconnect.Instance.myPlaceOnTap.isPlacing;
        ARconnect.Instance.myPlaceOnTap.setPrefabs(ARconnect.Instance.myPlaceOnTap.isPlacing);
    }
}


