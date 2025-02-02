using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PaperPlayController : MonoBehaviour
{
    Master myMaster;
    public bool findText;
    public bool play;

    public int noPlaying;
    public List<string> texts = new List<string>();

    public GameObject currentText;

    public GameObject textPrefab;
    public GameObject gfxPrefab;
    RawImage background;

    public float characterTimeDuration;
    public float maxTime;

    public ScaleController myScaleController;

    public List<Sprite> imageBank;
    // Vector3 screenCenter;
    // Vector3 worldCenter;
    // Start is called before the first frame update
    void Start()
    {
        background = GetComponent<RawImage>();
        noPlaying = 0;
        myMaster = GetComponentInParent<Master>();

        // screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane);

        // // Convert the screen center point to world point
        // worldCenter = myMaster.TheCamera.ScreenToWorldPoint(screenCenter);
    }

    // Update is called once per frame
    void Update()
    {
        if (findText)
        {
            for (int i = 0; i < myMaster.TheLineController.Lines.Count; i++)
            {
                if (myMaster.TheLineController.Lines[i].GetComponent<RowController>() != null)
                {
                    RowController checkeRowCont = myMaster.TheLineController.Lines[i].GetComponent<RowController>();
                    if (checkeRowCont != null)
                    {
                        if (checkeRowCont.Rows.Count > 0)
                        {
                            texts.Add("");

                            TextMeshProUGUI[] textMeshProComponents = myMaster.TheLineController.Lines[i].GetComponentsInChildren<TextMeshProUGUI>();
                            foreach (TextMeshProUGUI tmp in textMeshProComponents)
                            {
                                texts.Add(tmp.text);

                            }
                        }

                    }
                }
            }

            if (texts.Count > 0)
            {
                play = true;
            }


            findText = false;
        }

        if (play)
        {
            if (noPlaying < texts.Count)
            {
                background.enabled = true;


                if (currentText == null)
                {
                    if (texts[noPlaying] != "")
                    {
                        makeTextImage();

                    }
                    else
                    {
                        makeImage();
                    }

                }
                else
                {
                    DestroyFlash myDestroy = currentText.GetComponent<DestroyFlash>();
                    if (myDestroy.isTrue)
                    {
                        Destroy(currentText.gameObject);
                        // currentText = null;
                        if (texts[noPlaying] != "")
                        {
                            makeTextImage();

                        }
                        else
                        {
                            makeImage();
                        }
                        // makeTextImage();
                    }
                }
            }
            else
            {
                DestroyFlash myDestroy = currentText.GetComponent<DestroyFlash>();
                if (myDestroy.isTrue)
                {
                    Destroy(currentText.gameObject);
                    currentText = null;
                    noPlaying = 0;
                    background.enabled = false;
                    play = false;
                    texts.Clear();
                }

            }

        }

    }

    public void PlayStart()
    {
        findText = true;
    }

    void makeTextImage()
    {
        Vector3 position = Vector3.zero;

        GameObject newText = Instantiate(textPrefab, position, Quaternion.identity);
        newText.transform.SetParent(gameObject.transform);
        RectTransform rectTransform = newText.GetComponent<RectTransform>();
        rectTransform.localPosition = Vector3.zero;
        rectTransform.anchorMin = new Vector2(0, 0);  // Bottom-left corner
        rectTransform.anchorMax = new Vector2(1, 1);  // Top-right corner


        PaperScrapPlay myScrapPlay = newText.GetComponentInChildren<PaperScrapPlay>();
        myScrapPlay.TextField.color = new Color(0, 0, 0, 256);
        myScrapPlay.CutText = texts[noPlaying];
        myScrapPlay.backgroundImage.color = myMaster.BackgroundImage.color;
        myScrapPlay.PaperImage.sprite = myMaster.PaperList[0].sprite;
        myScrapPlay.PaperImage.color = myMaster.PaperList[0].color;

        RectTransform myScrapPlayRect = myScrapPlay.GetComponent<RectTransform>();
        myScrapPlayRect.localScale = myScaleController.currentScale;


        currentText = newText;

        DestroyFlash myDestroy = currentText.GetComponent<DestroyFlash>();
        if (texts[noPlaying].Length * characterTimeDuration <= maxTime)
        {
            myDestroy.timeToDestroy = texts[noPlaying].Length * characterTimeDuration;
        }
        else
        {
            myDestroy.timeToDestroy = maxTime;
        }


        noPlaying = noPlaying + 1;
    }
    void makeImage()
    {
        Vector3 position = Vector3.zero;

        GameObject newText = Instantiate(gfxPrefab, position, Quaternion.identity);
        newText.transform.SetParent(gameObject.transform);
        RectTransform rectTransform = newText.GetComponent<RectTransform>();

        // // Set anchors to stretch horizontally while remaining centered
        // rectTransform.anchorMin = new Vector2(0, 0.5f); // Bottom-middle
        // rectTransform.anchorMax = new Vector2(1, 0.5f); // Top-middle

        // // Reset offset (ensures it stretches across the X-axis)
        // rectTransform.offsetMin = new Vector2(0, rectTransform.offsetMin.y);
        // rectTransform.offsetMax = new Vector2(0, rectTransform.offsetMax.y);

        // // Adjust vertical size and position (centered by default)
        // rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, rectTransform.sizeDelta.y );
        // rectTransform.anchoredPosition = new Vector2(0, 0); // Centered on the Y-axis

        rectTransform.localPosition = Vector3.zero;
        rectTransform.anchorMin = new Vector2(0, 0);  // Bottom-left corner
        rectTransform.anchorMax = new Vector2(1, 1);  // Top-right corner

        // Reset the offsets to ensure it fills the entire screen
        rectTransform.offsetMin = Vector2.zero;  // Bottom and Left offsets
        rectTransform.offsetMax = Vector2.zero;  // Top and Right offsets

        videoPlayController myVideoPlay = newText.GetComponent<videoPlayController>();
        myVideoPlay.background.color = myMaster.BackgroundImage.color;
        myVideoPlay.displayImage.sprite = imageBank[Random.Range(0, imageBank.Count)];
        myVideoPlay.displayImage.color = findColor(myMaster.BackgroundImage.color);
        //new Color(1 - myMaster.BackgroundImage.color.r, 1 - myMaster.BackgroundImage.color.g, 1 - myMaster.BackgroundImage.color.b, 1);

        currentText = newText;

        noPlaying = noPlaying + 1;
    }

    Color findColor(Color backgroundColor)
    {
        // Get the background color
        Color bgColor = backgroundColor;

        // Calculate the opposite (complementary) color
        Color oppositeColor = new Color(1 - bgColor.r, 1 - bgColor.g, 1 - bgColor.b, 1);

        // Calculate the luminance of the opposite color
        float luminance = 0.2126f * oppositeColor.r + 0.7152f * oppositeColor.g + 0.0722f * oppositeColor.b;

        // If the luminance of the opposite color is too close to the background color, adjust it to ensure high contrast
        if (luminance > 0.5f)
        {
            // If the opposite color is bright (and might not stand out enough), make it darker
            oppositeColor = new Color(oppositeColor.r * 0.7f, oppositeColor.g * 0.7f, oppositeColor.b * 0.7f);
        }
        else
        {
            // If the opposite color is dark, you can make it lighter
            oppositeColor = new Color(oppositeColor.r + 0.2f, oppositeColor.g + 0.2f, oppositeColor.b + 0.2f);
        }

        // Clamp the values to ensure they remain within valid color range
        oppositeColor.r = Mathf.Clamp01(oppositeColor.r);
        oppositeColor.g = Mathf.Clamp01(oppositeColor.g);
        oppositeColor.b = Mathf.Clamp01(oppositeColor.b);

        // Apply the adjusted opposite color to the display image
        return oppositeColor;

    }


}
