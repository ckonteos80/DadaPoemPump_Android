using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WidthController : MonoBehaviour
{
    RectTransform MyRect;
    // Start is called before the first frame update
    void Start()
    {
        MyRect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        MyRect.sizeDelta = new Vector2(Screen.width - 350, MyRect.sizeDelta.y);
    }
}
