using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;




public class ArTextController : MonoBehaviour
{

    public ARconnect myConnect;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CheckText()
    {
        // Call the recursive method to check all children and descendants
        if (CheckChildrenText(transform))
        {
            myConnect.myMaster.TextListAR.Add(transform.GetComponent<TextMeshProUGUI>());
        }
    }
    bool CheckChildrenText(Transform parent)
    {
        // Loop through all children of the current parent
        foreach (Transform child in parent)
        {
            // Check if the child has the 'ClickPlacement' component
            if (child.GetComponent<TextMeshProUGUI>() != null)
            {
                // Destroy the child GameObject that has the 'ClickPlacement' component
                myConnect.myMaster.TextListAR.Add(child.GetComponent<TextMeshProUGUI>());
                // Destroy(child.gameObject);
            }

            // Recursively check the child's children (grandchildren)
            CheckChildrenText(child);
        }
        return false; // Return false if no 'ClickPlacement' component is found
    }

    public void CheckPaper()
    {
        // Call the recursive method to check all children and descendants
        if (CheckChildrenPaper(transform))
        {

            myConnect.myMaster.PaperListAR.Add(transform.GetComponent<Image>());
        }
    }
    bool CheckChildrenPaper(Transform parent)
    {
        // Loop through all children of the current parent
        foreach (Transform child in parent)
        {
            // Check if the child has the 'ClickPlacement' component
            if (child.GetComponent<isImageBG>() != null)
            {
                // Destroy the child GameObject that has the 'ClickPlacement' component
                myConnect.myMaster.PaperListAR.Add(child.GetComponent<Image>());
                // Destroy(child.gameObject);
            }

            // Recursively check the child's children (grandchildren)
            CheckChildrenPaper(child);
        }
        return false; // Return false if no 'ClickPlacement' component is found
    }



}
