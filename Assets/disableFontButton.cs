using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class disableFontButton : MonoBehaviour
{
    Button targetButton;  // The Button you want to disable
    Master myMaster;
    public TextMeshProUGUI textMeshPro;

    void Start()
    {
        targetButton = GetComponent<Button>();
        myMaster = GetComponentInParent<Master>();
        // Check if the targetButton is assigned

    }
    void Update()
    {
        if (targetButton.interactable == true)
        {
            if (myMaster.stage == 1)
            {
                if (IsTextEmpty())
                {
                    targetButton.interactable = false;
                }

            }



        }
        if (targetButton.interactable == false)
        {
            if (myMaster.stage == 1)
            {
                if (!IsTextEmpty())
                {
                    targetButton.interactable = true;
                }
            }
            else
            {
                targetButton.interactable = true;
            }
        }

    }
    bool IsTextEmpty()
    {
        // Check if the text is null or empty
        return string.IsNullOrEmpty(textMeshPro.text);
    }
}
