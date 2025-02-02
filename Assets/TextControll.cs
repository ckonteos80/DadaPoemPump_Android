using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.Linq;



public class TextControll : MonoBehaviour
{
    Master myMaster;
    public List<string> words;
    //  = new List<string>();
    public TextMeshProUGUI inputField;

    public RectTransform TextBGmIage;
    public RectTransform ContentImage;
    // public RectTransform ScrollContent;

    public GameObject FlashWordPrefab;
    public GameObject PaperCutPrefab;
    // public List<GameObject> PaperCutBGPrefab;

    // public GameObject ArticleGO;

    public Camera MainCam;

    public Transform PaperCuts;

    // public Button CutButton;

    // public BagControll MyBagControll;



    // TMP_Text textField;

    public float minHeight = 50f;
    public float maxHeight = 50000f;


    public float preferredHeight;

    public RectTransform InputRectTransform;
    // public RectTransform TextBGImage;
    // public RectTransform ContnetRectTransform;
    // public Transform PaperBackgroundTRLeft;
    // public Transform PaperBackgroundTRRight;

    public RectTransform MousePoint;

    public PeperBagController BagController;

    public PaperController MyPaper;
    public LineController MyLineController;

    public float delayBetweenSpawns;

    public bool spawningCuts;

    NewsAPI myNews;




    // Start is called before the first frame update
    void Start()
    {
     
        myNews = GetComponentInParent<NewsAPI>();
        myMaster = GetComponentInParent<Master>();
        // textField = inputField.GetComponent<TMP_Text>();

        InputRectTransform = inputField.GetComponent<RectTransform>();

        // Get the preferred height of the text in the input field
        preferredHeight = inputField.preferredHeight;

        // Set the initial height of the input field
        InputRectTransform.sizeDelta = new Vector2(InputRectTransform.sizeDelta.x, Mathf.Max(preferredHeight, minHeight));

        // MyPaper = GetComponentInChildren<PaperController>();
        // AdjustHeight();
        // MyPaper.draw = true;
        MyPaper.FindShape();
    }

    // Update is called once per frame
    void Update()
    {
        // if (inputField.isFocused && Input.GetKeyDown(KeyCode.Space))
        // {
        //     cutWords();
        //     ShowFlashText(words[words.Count - 1]);
        // }

        // if (inputField.isFocused && Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.V))
        // {
        //     cutWords();
        //     AdjustHeight();
        //     ShowFlashText(inputField.text);
        // }

        if (words.Count != 0)
        {
            myMaster.SetStage(15);
        }


        // if (myMaster.theConfirm != null)
        // {
        //     confirmController myConfirmController = myMaster.theConfirm.GetComponent<confirmController>();
        //     if (myConfirmController.confirmed)
        //     {
        //         inputField.text = "";
        //         words.Clear();

        //         for (int i = 0; i < myMaster.PaperList.Count; i++)
        //         {
        //             if (myMaster.PaperList != null)
        //             {
        //                 Destroy(myMaster.PaperList[i]);
        //             }
        //         }
        //         myMaster.PaperList.Clear();

        //         MyLineController.ClearLines();
        //         BagController.ClearScraps();


        //         preferredHeight = inputField.preferredHeight;


        //         InputRectTransform.sizeDelta = new Vector2(InputRectTransform.sizeDelta.x, 40);


        //         MyPaper.FindShape();

        //         myMaster.SetStage(1);

        //         myMaster.theConfirm = null;
        //         myMaster.hideMenu();
        //         Debug.Log("cleared");
        //         // Destroy(myMaster.theConfirm.gameObject);
        //     }
        // }

    }


    public void cutWords()
    {
        words = inputField.text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        // , ',', '.', '!', '?' 
    }

    void ShowFlashText(string TextToShow)
    {
        GameObject myFlashWord = Instantiate(FlashWordPrefab, new Vector3(Screen.width / 2, Screen.height / 2, 0), gameObject.transform.rotation);
        myFlashWord.transform.SetParent(inputField.transform.parent);
        TextMeshProUGUI myFlashText = myFlashWord.GetComponentInChildren<TextMeshProUGUI>();
        myFlashText.text = TextToShow;
    }

    public void CutPaper()
    {
      //  cutWords();
        // gameObject.SetActive(false);
        inputField.text = "";

        spawningCuts = true;

        // myMaster.PasteButton.gameObject.SetActive(false);
        // myMaster.CutButton.gameObject.SetActive(false);
        // //  myMaster.PlaceButton.gameObject.SetActive(true);
        // myMaster.SaveButton.gameObject.SetActive(false);



        myMaster.textPaperToCut.SetActive(false);
        // myMaster.stage = 25;

        StartCoroutine(InstantiateObjects());

        // myMaster.SetStage(2);

        // MyBagControll.CreateBag();
        //  MyBagControll.MakingBag =true;
        //  myMaster.TextGameobj.SetActive(false);



    }

    IEnumerator InstantiateObjects()
    {
        myMaster.myTipsController.tipsText.color = new Color(0, 0, 0, 0);
        for  (int i = words.Count - 1; i >= 0; i--)
        //(int i = 0; i < words.Count; i++)
        {
            // myMaster.myTipsController.tipsText.text = "";
            // myMaster.myTipsController.changeFlashingText(null, null);

            // Instantiate new paper at a random position
            GameObject NewPaper = Instantiate(
                PaperCutPrefab,
                new Vector3(
                    gameObject.transform.position.x + UnityEngine.Random.Range(-100, 100),
                    gameObject.transform.position.y + UnityEngine.Random.Range(-100, 100),
                    gameObject.transform.position.z
                ),
                gameObject.transform.rotation
            );

            // Set the new paper as a child of the PaperCuts parent object
            NewPaper.transform.SetParent(PaperCuts);

            // Get the PaperScrapController component and assign values
            PaperScrapController MyPaperCut = NewPaper.GetComponent<PaperScrapController>();
            MyPaperCut.CutText = words[i]; // Use the last word from the list
            MyPaperCut.TheCamera = MainCam;
            MyPaperCut.aa = i;

            // Add the Rigidbody2D of the new paper to a list for further use
            BagController.PaperCutRBs.Add(NewPaper.GetComponent<Rigidbody2D>());

            // Remove the word that was just used from the list
            words.RemoveAt(i);

            // Wait for the specified delay before spawning the next object
            yield return new WaitForSeconds(delayBetweenSpawns);
        }



        // // Iterate through each object in the list
        // foreach (GameObject obj in objectList)
        // {
        //     // Instantiate the object at the spawn point
        //     Instantiate(obj, spawnPoint.position, Quaternion.identity);

        //     // Wait for 0.5 seconds before continuing
        //     yield return new WaitForSeconds(0.5f);
        // }

        // Code to run after all objects are instantiated
        OnAllObjectsInstantiated();
    }

    void OnAllObjectsInstantiated()
    {
        myMaster.myTipsController.tipsText.color = new Color(256, 0, 0, 1);
        myMaster.SetStage(2);
        spawningCuts = false;
        // Add your custom code here
    }



    //  public void CutPaper() BACKUPPPPPPP
    // {
    //     cutWords();
    //     // gameObject.SetActive(false);
    //     inputField.text = "";

    //     for (int i = 0; i < words.Count; i++)
    //     {
    //         GameObject NewPaper = Instantiate(PaperCutPrefab, new Vector3(gameObject.transform.position.x + UnityEngine.Random.Range(-100, 100), gameObject.transform.position.y + UnityEngine.Random.Range(-100, 100), gameObject.transform.position.z), gameObject.transform.rotation);
    //         NewPaper.transform.SetParent(PaperCuts);

    //         // PaperCutController MyPaperCut = NewPaper.GetComponent<PaperCutController>();
    //         // MyPaperCut.CutText = words[words.Count - 1];
    //         // MyPaperCut.TheCamera = MainCam;
    //         // MyPaperCut.TheMousePoint = MousePoint;
    //         // MyPaperCut.aa = i;

    //         PaperScrapController MyPaperCut = NewPaper.GetComponent<PaperScrapController>();
    //         MyPaperCut.CutText = words[words.Count - 1];
    //         MyPaperCut.TheCamera = MainCam;
    //         // MyPaperCut.TheMousePoint = MousePoint;
    //         MyPaperCut.aa = i;
    //         BagController.PaperCutRBs.Add(NewPaper.GetComponent<Rigidbody2D>());
    //         // TextMeshProUGUI myPaperText = NewPaper.GetComponentInChildren<TextMeshProUGUI>();
    //         // myPaperText.text = words[words.Count - 1];
    //         words.RemoveAt(words.Count - 1);
    //     }

    //     myMaster.SetStage(2);

    //     // MyBagControll.CreateBag();
    //     //  MyBagControll.MakingBag =true;

    // }

    // public void OnTextChanged(string newText)
    // {





    //     // textField.text = newText;
    //     // textField.ForceMeshUpdate(); // Required to update the preferred height
    //     // float preferredHeight = textField.preferredHeight;                                                                                                                                                                                                                                                          
    //     // RectTransform inputFieldTransform = inputField.GetComponent<RectTransform>();
    //     // inputFieldTransform.sizeDelta = new Vector2(inputFieldTransform.sizeDelta.x, preferredHeight);
    // }

    public void AdjustHeight()
    {
        // Get the preferred height of the text in the input field
        preferredHeight = inputField.preferredHeight + 40f;
        // Set the new height of the input field
        InputRectTransform.sizeDelta = new Vector2(InputRectTransform.sizeDelta.x, Mathf.Max(preferredHeight, minHeight));

        // float TimesVertical = InputRectTransform.sizeDelta.y / 200f;
        // CreatePaperBG(Mathf.RoundToInt(TimesVertical));


        TextBGmIage.sizeDelta = new Vector2(TextBGmIage.sizeDelta.x, InputRectTransform.sizeDelta.y + 100);
        // ContnetRectTransform.sizeDelta = new Vector2(ContnetRectTransform.sizeDelta.x, InputRectTransform.sizeDelta.y + 200);
    }

    // void CreatePaperBG(int Times)
    // {
    //     for (int i = 0; i < PaperBackgroundTRLeft.childCount; i++)
    //     {
    //         // Destroy the child object
    //         Destroy(PaperBackgroundTRLeft.GetChild(i).gameObject);
    //     }
    //     for (int i = 0; i < PaperBackgroundTRRight.childCount; i++)
    //     {
    //         // Destroy the child object
    //         Destroy(PaperBackgroundTRRight.GetChild(i).gameObject);
    //     }

    //     for (int i = 0; i < Times + 1; i++)
    //     {
    //         // Destroy the child object
    //         Instantiate(PaperCutBGPrefab[0]).transform.SetParent(PaperBackgroundTRLeft);
    //     }
    //     for (int i = 0; i < Times + 1; i++)
    //     {
    //         // Destroy the child object
    //         Instantiate(PaperCutBGPrefab[1]).transform.SetParent(PaperBackgroundTRRight);
    //     }

    //     ContnetRectTransform.sizeDelta = new Vector2(ContnetRectTransform.sizeDelta.x, (Times + 1) * 200);
    // }

    public void DoPaste(bool API)
    {
        if (myMaster.stage == 1)
        {
            if (!API)
            {
                inputField.text = UniClipboard.GetText();
            }
            else
            {
                inputField.text = myNews.title + "\n" + myNews.description + "\n";
                //  + "From: " + myNews.url;
            }

            cutWords();


            // AdjustHeight();
            preferredHeight = inputField.preferredHeight + 40f;
            // Set the new height of the input field
            InputRectTransform.sizeDelta = new Vector2(InputRectTransform.sizeDelta.x, Mathf.Max(preferredHeight, minHeight));

            // float TimesVertical = InputRectTransform.sizeDelta.y / 200f;
            // CreatePaperBG(Mathf.RoundToInt(TimesVertical));


            TextBGmIage.sizeDelta = new Vector2(TextBGmIage.sizeDelta.x, InputRectTransform.sizeDelta.y + 100);
            ContentImage.sizeDelta = new Vector2(ContentImage.sizeDelta.x, InputRectTransform.sizeDelta.y + 500);

            inputField.enableAutoSizing = true;
            inputField.fontSizeMax = 40f;
            // MyPaper.draw = true;
            MyPaper.FindShape();

            myMaster.MyPictureController.play = true;

        }
    }

    public void DoClear()
    {
        if (myMaster.theConfirm == null)
        {
            myMaster.theConfirm = Instantiate(myMaster.confirmPrefrab);
            myMaster.theConfirm.transform.SetParent(myMaster.Menu.transform.parent);
            RectTransform rectTransform = myMaster.theConfirm.GetComponent<RectTransform>();

            rectTransform.anchorMin = new Vector2(0, 0);  // Bottom-left corner
            rectTransform.anchorMax = new Vector2(1, 1);  // Top-right corner

            // Reset the offsets to ensure it fills the entire screen
            rectTransform.offsetMin = Vector2.zero;  // Bottom and Left offsets
            rectTransform.offsetMax = Vector2.zero;  // Top and Right offsets


            // Reset position, rotation, and scale
            rectTransform.anchoredPosition = Vector2.zero;
            rectTransform.localRotation = Quaternion.identity;
            rectTransform.localScale = Vector3.one;

            myNews.GetNewsAPI();
        }
        // else
        // {
        //     confirmController myConfirmController = myMaster.theConfirm.GetComponent<confirmController>();
        //     if (myConfirmController.confirmed)
        //     {
        //         inputField.text = "";
        //         words.Clear();

        //         preferredHeight = inputField.preferredHeight;


        //         InputRectTransform.sizeDelta = new Vector2(InputRectTransform.sizeDelta.x, 40);


        //         MyPaper.FindShape();

        //         myMaster.SetStage(1);
        //         Destroy(myMaster.theConfirm.gameObject);
        //         myMaster.theConfirm = null;
        //     }
        // }

    }

}
