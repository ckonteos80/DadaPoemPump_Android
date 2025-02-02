using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AdjustTextHeight : MonoBehaviour
{
    public TMP_InputField inputField;
    public float minHeight;
    public float maxHeight;

    private void Update()
    {
        var preferredHeight = LayoutUtility.GetPreferredHeight(inputField.textComponent.rectTransform);
        var clampedHeight = Mathf.Clamp(preferredHeight, minHeight, maxHeight);
        inputField.GetComponent<LayoutElement>().preferredHeight = clampedHeight;
    }
}
