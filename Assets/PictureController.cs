using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PictureController : MonoBehaviour
{
    public Image image;
    public Sprite[] animationFrames;
    public float frameDuration = 0.5f;
    public bool playAnimation;
    public bool play;

    public List<GameObject> button;


    void Update()
    {
        if (play)
        {
            StartAnimation();
            play = false;
        }
        // else
        // {
        //     StopAnimation();
        // }
    }

    void StartAnimation()
    {
        if (!playAnimation)
        {
              for (int i = 0; i < button.Count; i++)
            {
                drawButton myDrawButton = button[i].GetComponent<drawButton>();
                myDrawButton.FindShape();
                button[i].SetActive(false);
            }
            // button.SetActive(false);
            image.enabled = true;
            playAnimation = true;
            StartCoroutine(PlayAnimation());

            // Introduce a delay of 2 seconds and then stop the animation
            StartCoroutine(StopAnimationAfterDelay(1f));
        }
    }

    void StopAnimation()
    {
        if (playAnimation)
        {
            for (int i = 0; i < button.Count; i++)
            {
                drawButton myDrawButton = button[i].GetComponent<drawButton>();
                myDrawButton.FindShape();
                button[i].SetActive(true);
            }


            
            image.enabled = false;
            playAnimation = false;
            StopAllCoroutines();
            play = false;



        }
    }

    IEnumerator PlayAnimation()
    {
        while (playAnimation)
        {
            for (int i = 0; i < animationFrames.Length; i++)
            {
                // Switch the sprite
                image.sprite = animationFrames[Random.Range(0, animationFrames.Length)];

                // Wait for the specified frame duration
                yield return new WaitForSeconds(Random.Range(frameDuration, frameDuration * 2));
            }
        }
    }

    IEnumerator StopAnimationAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        StopAnimation();
        play = false;
        playAnimation = false;
    }
}

