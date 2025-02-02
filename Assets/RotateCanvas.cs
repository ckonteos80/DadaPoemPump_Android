using UnityEngine;

public class RotateCanvas : MonoBehaviour
{
    private RectTransform rectTransform; // The RectTransform of the Canvas

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        // Check the device's orientation
        if (Screen.orientation == ScreenOrientation.LandscapeLeft)
        {
            // Rotate Canvas for Landscape Left
            rectTransform.localRotation = Quaternion.Euler(0, 0, -90);
        }
        else if (Screen.orientation == ScreenOrientation.LandscapeRight)
        {
            // Rotate Canvas for Landscape Right
            rectTransform.localRotation = Quaternion.Euler(0, 0, 90);
        }
        else
        {
            // Reset rotation for Portrait or Upside Down
            rectTransform.localRotation = Quaternion.identity;
        }
    }
}
