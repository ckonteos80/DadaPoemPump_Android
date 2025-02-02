using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpacityController : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticleController myParticleController;
    Slider mySlider;

    ParticleSystem ChangingParticle;

    public float subtractValue;

    RectTransform rectTransform;



    void Start()
    {
        mySlider = GetComponent<Slider>();

        rectTransform = GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            // Get the screen width and calculate the target width
            float targetWidth = (Screen.width / 2f) - subtractValue;

            // Update the width of the RectTransform
            rectTransform.sizeDelta = new Vector2(targetWidth, rectTransform.sizeDelta.y);
        }


    }

    // Update is called once per frame
    void Update()
    {


        if (myParticleController.CurrentParticle != null)
        {
            if (myParticleController.CurrentParticle.GetComponent<ParticleSystem>() == null)
            {
                mySlider.interactable = false;
            }
            else
            {
                mySlider.interactable = true;
                ChangingParticle = myParticleController.CurrentParticle.GetComponent<ParticleSystem>();
            }
        }
        else
        {
            mySlider.interactable = false;
        }

        if (ChangingParticle != null)
        {
            ParticleSystem.MainModule mainModule = ChangingParticle.main;

            ParticleSystem.MinMaxGradient startColorGradient = mainModule.startColor;
            Color startColor = startColorGradient.color;
            startColor.a = mySlider.value;

            // Set the start color
            mainModule.startColor = new ParticleSystem.MinMaxGradient(startColor);
            // ParticleSystem.MainModule mainModule = ChangingParticle.main;
            // ParticleSystem.MinMaxGradient startColorGradient = mainModule.startColor;
            // Color startColor = ChangingParticle.main.startColor;

            // // Set the new start color back to the particle system's main module
            // startColorGradient.color = startColor;
            // mainModule.startColor = startColorGradient;
        }
    }
}
