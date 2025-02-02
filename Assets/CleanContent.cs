using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanContent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CheckAndDestroy()
    {
        // Call the recursive method to check all children and descendants
        if (CheckChildren(transform))
        {
            // Destroy the GameObject if any child or descendant has the 'ClickPlacement' component
            Destroy(gameObject);
        }
    }
     bool CheckChildren(Transform parent)
    {
         // Loop through all children of the current parent
        foreach (Transform child in parent)
        {
            // Check if the child has the 'ClickPlacement' component
            if (child.GetComponent<ClickPlacement>() != null)
            {
                // Destroy the child GameObject that has the 'ClickPlacement' component
                Destroy(child.gameObject);
            }

            // Recursively check the child's children (grandchildren)
            CheckChildren(child);
        }
        return false; // Return false if no 'ClickPlacement' component is found
    }
}
