using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class disbleParticleButton : MonoBehaviour
{
    Button targetButton;  // The Button you want to disable
    public Slider particleSlider;

    void Start()
    {
        targetButton = GetComponent<Button>();
        // Check if the targetButton is assigned

    }

    void Update()
    {
        if (targetButton.interactable == true)
        {
            if (particleSlider.value < 0.1)
            {
                targetButton.interactable = false;
            }



        }
        if (targetButton.interactable == false)
        {
            if (particleSlider.value > 0.1)
            {
                targetButton.interactable = true;
            }
        }

    }
}
