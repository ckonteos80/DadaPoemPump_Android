using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScaleController : MonoBehaviour
{
    public RectTransform CutItemsScale;
    public Button ScaleUpButton;
    public Button ScaleDownButton;

    public float scaleIncrement;
    public float pushIncrement;
    public Vector3 currentScale;
    // Start is called before the first frame update
    void Start()
    {
        currentScale = new Vector3(CutItemsScale.transform.localScale.x,CutItemsScale.transform.localScale.y,CutItemsScale.transform.localScale.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (CutItemsScale.localScale.x >= 2)
        {
            ScaleUpButton.interactable = false;
        }
        else
        {
            ScaleUpButton.interactable = true;
        }

        if (CutItemsScale.localScale.x <= 1)
        {
            ScaleDownButton.interactable = false;
        }
        else
        {
            ScaleDownButton.interactable = true;
        }
    }

    public void ScaleUp()
    {
        currentScale = CutItemsScale.localScale;
        Vector3 newScale = currentScale + new Vector3(scaleIncrement, scaleIncrement, 0);
        CutItemsScale.localScale = newScale;
    }
    public void ScaleDown()
    {
        currentScale = CutItemsScale.localScale;
        Vector3 newScale = currentScale - new Vector3(scaleIncrement, scaleIncrement, 0);
        CutItemsScale.localScale = newScale;
    }

    public void PushUp()
    {


        CutItemsScale.position = new Vector2(CutItemsScale.position.x, CutItemsScale.position.y + pushIncrement);
    }
    public void PushDown()
    {


        CutItemsScale.position = new Vector2(CutItemsScale.position.x, CutItemsScale.position.y - pushIncrement);
    }

}
