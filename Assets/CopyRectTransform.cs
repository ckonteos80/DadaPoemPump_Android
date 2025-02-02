using UnityEngine;

public class CopyRectTransform : MonoBehaviour
{
    public RectTransform sourceRectTransform; // The RectTransform to copy from

    void Update()
    {
        if (sourceRectTransform != null)
        {
            RectTransform targetRectTransform = GetComponent<RectTransform>();
            if (targetRectTransform != null)
            {
                CopyRectTransformProperties(sourceRectTransform, targetRectTransform);
            }
            else
            {
                Debug.LogWarning("The GameObject does not have a RectTransform component.");
            }
        }
        else
        {
            Debug.LogWarning("Source RectTransform is not assigned.");
        }
    }

    private void CopyRectTransformProperties(RectTransform source, RectTransform target)
    {
        // Copy position and rotation
        target.anchoredPosition = source.anchoredPosition;
        target.sizeDelta = source.sizeDelta;
        target.anchorMin = source.anchorMin;
        target.anchorMax = source.anchorMax;
        target.pivot = source.pivot;
        target.localScale = source.localScale;
        target.rotation = source.rotation;
    }
}
