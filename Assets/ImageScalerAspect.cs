using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageScalerAspect : MonoBehaviour
{
    RectTransform targetRectTransform;
    public float adjustmentValue;
    public float Xmove;
    public float divider;
    void Start()
    {
        targetRectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        adjustmentValue = (Screen.width /divider) * -1;

        if (targetRectTransform != null)
        {
            AdjustRectTransform(targetRectTransform, adjustmentValue);
        }
        else
        {
            Debug.LogError("No RectTransform assigned to targetRectTransform.");
        }
    }

    void AdjustRectTransform(RectTransform rectTransform, float adjustment)
    {
        // Get screen dimensions
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // Desired aspect ratio
        float targetAspectRatio = 9f / 16f;

        // Calculate the target width and height while maintaining the aspect ratio
        float targetWidth, targetHeight;

        if (screenWidth / screenHeight > targetAspectRatio)
        {
            // Screen is wider than the target aspect ratio
            targetHeight = screenHeight + adjustment;
            targetWidth = targetHeight * targetAspectRatio;
        }
        else
        {
            // Screen is narrower than the target aspect ratio
            targetWidth = screenWidth + adjustment;
            targetHeight = targetWidth / targetAspectRatio;
        }

         Vector2 currentPosition = rectTransform.anchoredPosition;
         currentPosition = Vector2.zero;

            // Modify the X position
            currentPosition.x += Xmove;

         

        // Set the RectTransform size to match the calculated dimensions
        rectTransform.sizeDelta = new Vector2(targetWidth, targetHeight);

        // Optionally adjust the position to keep it centered
        rectTransform.anchoredPosition =currentPosition;
    }
}
