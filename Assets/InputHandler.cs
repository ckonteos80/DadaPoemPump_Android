using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class InputHandler : MonoBehaviour
{
    public TMP_InputField inputField;
    // Start is called before the first frame update
    void Start()
    {
        // Allow the user to input multiple lines of text
        inputField.lineType = TMP_InputField.LineType.MultiLineNewline;
        inputField.characterLimit = 0; // unlimited

        // Add a listener to the input field to handle changes
        inputField.onValueChanged.AddListener(OnValueChanged);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnValueChanged(string value)
    {
        // Remove line breaks from the text to prevent errors
        value = value.Replace("\n", "");
        inputField.text = value;
    }
}
