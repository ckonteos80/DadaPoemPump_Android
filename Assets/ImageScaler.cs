using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageScaler : MonoBehaviour
{
    RectTransform targetRectTransform;
    public float adjustment;
    public float divider;


    public bool onlyX;
    // Start is called before the first frame update
    void Start()
    {
        targetRectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (targetRectTransform != null)
        {
            AdjustToScreenSize(targetRectTransform);
        }
    }

    void AdjustToScreenSize(RectTransform rectTransform)
    {
        float screenWidth;
        float screenHeight;
        if (onlyX)
        {
            screenWidth = Screen.width -Screen.width/divider -10;
            screenHeight = rectTransform.sizeDelta.y;
        }
        else
        {
            screenWidth = Screen.width + adjustment;
            screenHeight = Screen.height + adjustment;

        }
        // Get screen dimensions

        // Set the RectTransform size to match the screen size
        rectTransform.sizeDelta = new Vector2(screenWidth, screenHeight);
    }
}
