using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menuController : MonoBehaviour
{
    Master myMaster;
    public TextControll myTextControll;
    public GameObject startButton;
    public GameObject newButton;
    public GameObject backbutton;

    // Start is called before the first frame update
    void Start()
    {
        myMaster = GetComponentInParent<Master>();
    }

    // Update is called once per frame
    void Update()
    {
        if (myMaster.stage == 1 && myTextControll.words.Count == 0 && myTextControll.inputField.text == "")
        {
            startButton.gameObject.SetActive(true);
            newButton.gameObject.SetActive(false);
            backbutton.gameObject.SetActive(false);
        }
        else
        {
            startButton.gameObject.SetActive(false);
            newButton.gameObject.SetActive(true);
            backbutton.gameObject.SetActive(true);
        }

    }

    public void newStart()
    {

    }
}
