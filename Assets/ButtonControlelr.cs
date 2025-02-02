using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonControlelr : MonoBehaviour
{
    public float leftPadding = 50f; // Padding from the left screen edge
    public float rightPadding = 50f; // Padding from the right screen edge



    public List<RectTransform> childRects = new List<RectTransform>(); // List of child RectTransforms

    void Start()
    {
        // If no children or only one child, do nothing
        if (childRects.Count <= 1)
            return;

        // Calculate the total width available for child objects
        float screenWidth = Screen.width;
        float availableWidth = screenWidth - (leftPadding + rightPadding);

        // Calculate the combined width of all child objects
        float totalChildWidth = 0f;
        foreach (var childRect in childRects)
        {
            totalChildWidth += childRect.rect.width;
        }

        // Calculate the remaining space after accounting for all child widths
        float remainingSpace = availableWidth - totalChildWidth;

        // Calculate the spacing between each child
        float spacing = remainingSpace / (childRects.Count - 1);

        // Position each child object with the calculated spacing
        float currentXPosition = leftPadding;
        foreach (var childRect in childRects)
        {
            childRect.anchoredPosition = new Vector2(currentXPosition, childRect.anchoredPosition.y);
            currentXPosition += childRect.rect.width + spacing;
        }
    }

}
