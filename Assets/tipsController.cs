using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class tipsController : MonoBehaviour
{
    Master myMaster;
    public TextMeshProUGUI tipsText;



    public List<Transform> textMeshPro;  // Drag your TextMeshPro component here in the Inspector
    public Color targetColor = Color.red; // The target color to transition to
    public float duration = 0.5f;         // Total duration of the color transition
    public bool changeColor = false;      // Bool to control whether color should change

    private Coroutine colorCoroutine = null;  // Coroutine reference
    public Color initialColor = Color.white; // Initial color of the text (white)

    public Transform pasteButtonText;
    public Transform cutButtonText;
    public Transform MoreButtonText;
    public Transform ARbuttonText;
    public Transform ARsetButtonText;
    public Transform placeButtonText;

    public Transform centerPos;
    public Transform upPos;
    public Transform upPosButtons;
    public Transform scrapsButtons;

    public bool showingTips;

    public Toggle myToggle;

    private void Start()
    {
        // Set the initial color of the text to white
        // textMeshPro.color = initialColor;

        myMaster = GetComponentInParent<Master>();
        tipsText = GetComponentInChildren<TextMeshProUGUI>();
        myToggle.isOn = showingTips;
    }

    private void Update()
    {
        // tipsText.color = new Color(1f - myMaster.BackgroundImage.color.r, 1f - myMaster.BackgroundImage.color.g, 1f - myMaster.BackgroundImage.color.b, tipsText.color.a);
        if (showingTips)
        {
            tipsText.enabled = true;

            if (textMeshPro.Count > 0)
            {
                changeColor = true;
            }
            else
            {
                changeColor = false;
            }
        }
        else
        {
            tipsText.enabled = false;
            changeColor = false;
        }
        // If the bool is true and no coroutine is running, start the color-changing coroutine
        if (changeColor && colorCoroutine == null)
        {
            colorCoroutine = StartCoroutine(ChangeTextColor());
        }
        // If the bool is false and the coroutine is running, stop it and reset the color to white
        else if (!changeColor && colorCoroutine != null)
        {
            StopCoroutine(colorCoroutine);
            colorCoroutine = null;
            for (int i = 0; i < textMeshPro.Count; i++)
            {
                if (textMeshPro[i] != null)
                {
                    if (textMeshPro[0].GetComponent<TextMeshProUGUI>() != null)
                    {
                        textMeshPro[0].GetComponent<TextMeshProUGUI>().color = initialColor;
                    }
                }
            }
            // Set text back to white
        }
    }

    IEnumerator ChangeTextColor()
    {
        // Get the duration for transitioning to and from the target color
        float halfDuration = duration / 2f;

        // Color StartColor;

        //  if (textMeshPro.Count > 0)
        //         {
        //          StartColor = textMeshPro[0].vertex
        //         }

        while (changeColor)
        {
            // Transition from white to the target color
            float t = 0;
            while (t < halfDuration)
            {
                t += Time.deltaTime;
                if (textMeshPro.Count > 0)
                {
                    for (int i = 0; i < textMeshPro.Count; i++)
                    {
                        if (textMeshPro[i] != null)
                        {
                            changeColorTransform(textMeshPro[i], Color.Lerp(initialColor, targetColor, t / halfDuration));
                        }
                    }
                }
                yield return null; // Wait for the next frame
            }


            if (textMeshPro.Count > 0)
            {
                for (int i = 0; i < textMeshPro.Count; i++)
                {
                    if (textMeshPro[i] != null)
                    {
                        changeColorTransform(textMeshPro[i], targetColor);
                    }
                }
            }
            // Ensure the final color is set to the target color


            // Transition back to white
            t = 0;
            while (t < halfDuration)
            {
                t += Time.deltaTime;
                if (textMeshPro.Count > 0)
                {
                    for (int i = 0; i < textMeshPro.Count; i++)
                    {
                        if (textMeshPro[i] != null)
                        {
                            changeColorTransform(textMeshPro[i], Color.Lerp(targetColor, initialColor, t / halfDuration));
                        }
                    }
                }
                yield return null; // Wait for the next frame
            }

            // Ensure the final color is set to white
            if (textMeshPro.Count > 0)
            {
                for (int i = 0; i < textMeshPro.Count; i++)
                {
                    if (textMeshPro[i] != null)
                    {
                        changeColorTransform(textMeshPro[i], initialColor);
                    }
                }
            }


            // Repeat the loop as long as the bool is true
        }
    }

    void changeColorTransform(Transform ChangeTransform, Color toColor)
    {
        if (ChangeTransform.GetComponent<TextMeshProUGUI>() != null)
        {
            ChangeTransform.GetComponent<TextMeshProUGUI>().color = toColor;
        }
        if (ChangeTransform.GetComponent<UnityEngine.UI.Image>() != null)
        {
            ChangeTransform.GetComponent<UnityEngine.UI.Image>().color = toColor;
        }

    }

    public void changeFlashingText(Transform selectedText, List<Transform> itemsToAdd)
    {
        for (int i = 0; i < textMeshPro.Count; i++)
        {
            if (textMeshPro[i] != null)
            {
                changeColorTransform(textMeshPro[i], initialColor);
            }
        }
        textMeshPro.Clear();
        if (itemsToAdd != null)
        {
            for (int i = 0; i < itemsToAdd.Count; i++)
            {
                textMeshPro.Add(itemsToAdd[i]);
            }
        }
        if (selectedText != null)
        {
            textMeshPro.Add(selectedText);
        }
    }

    public void ChangeOnOff()
    {
        showingTips = !showingTips;
        myToggle.isOn = showingTips;


        if (!showingTips)
        {
            if (colorCoroutine != null)
            {
                StopCoroutine(colorCoroutine);
                colorCoroutine = null;

                if (textMeshPro.Count != 0)
                {
                    for (int i = 0; i < textMeshPro.Count; i++)
                    {
                        if (textMeshPro[i] != null)
                        {
                            if (textMeshPro[i].GetComponent<Image>() != null)
                            {
                                textMeshPro[i].GetComponent<Image>().color = initialColor;
                            }
                            if (textMeshPro[i].GetComponent<TextMeshProUGUI>() != null)
                            {
                                textMeshPro[i].GetComponent<TextMeshProUGUI>().color = initialColor;
                            }
                        }
                    }
                }
            }
        }


    }

    public void ChangeText(string newText, Transform newpos)
    {
        tipsText.text = newText;
        tipsText.transform.position = newpos.position;
    }
    // Start is called before the first frame update

}
