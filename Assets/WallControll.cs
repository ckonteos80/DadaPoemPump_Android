using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallControll : MonoBehaviour
{
    BoxCollider2D boxCollider;
    public bool Vetrtical;

    // Start is called before the first frame update
    void Start()
    {

        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // boxCollider.size = rectTransform.sizeDelta;
        // boxCollider.offset = new Vector2(rectTransform.rect.width / 2f, -rectTransform.rect.height / 2f);
        // Divide by 10 to scale it to the same units as the game world
        if (Vetrtical)
        {
            float newHeight = Screen.height + 100;
            boxCollider.size = new Vector2(boxCollider.size.x, newHeight);
        }
        else
        {
            float newWidth = Screen.width + 100;
            boxCollider.size = new Vector2(newWidth,boxCollider.size.y);
        }
    }
}
