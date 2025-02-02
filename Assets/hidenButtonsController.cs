using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class hidenButtonsController : MonoBehaviour
{

    public List<Button> Buttons;
    public List<TextMeshProUGUI> Texts;

    public List<GameObject> ToHide;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ColorChange(Color newcolor)
    {
        Color inverted = new Color(1 - newcolor.r, 1 - newcolor.g, 1 - newcolor.b, newcolor.a);

        for (int i = 0; i < Buttons.Count; i++)
        {
            ColorBlock colorBlock = Buttons[i].colors;

            colorBlock.normalColor = newcolor;
            colorBlock.selectedColor = newcolor;
            //  Buttons[i].highlightedColor = normalColor; 
            //  Buttons[i].pressedColor = normalColor;      

            // Set the disabled color
            colorBlock.disabledColor = newcolor;

            Buttons[i].colors = colorBlock;

        }
        for (int i = 0; i < Texts.Count; i++)
        {

            Texts[i].color = inverted;


        }
    }

    public void hideHidden()
    {
        for (int i = 0; i < ToHide.Count; i++)
        {
            ToHide[i].gameObject.SetActive(false);
        }

    }
    public void unhideHidden()
    {
        for (int i = 0; i < ToHide.Count; i++)
        {
            ToHide[i].gameObject.SetActive(true);
        }

    }
}
