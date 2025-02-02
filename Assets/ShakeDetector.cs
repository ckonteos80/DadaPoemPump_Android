using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Unity.Mathematics;

public class ShakeDetector : MonoBehaviour
{
    public TMP_Text TextAccelarate;
    // public float ShakeThreshold;
    // public float MinInterval;

    // float sqrShakeThreshold;
    // float timeSinceLastShake;

    PeperBagController MyBag;

    public bool CanShake;

    public Button PlaceButton;

    public bool hasShaked;

    public Vector2 acceleration;

    public float shakenFor;

    public tipsController myTipsController;

    public Transform ShakeButton;

    Master myMaster;

    public bool shaking;




    public InputActionProperty accelerometerInput;

    // List of Rigidbody2D components to apply force to
    // public List<Rigidbody2D> PaperCutRBs = new List<Rigidbody2D>();

    // Threshold for detecting a shake
    public float shakeThreshold = 5.0f;    // Adjust this value as needed
    public float shakeCooldown = 0.5f;     // Time in seconds to wait before detecting another shake

    // Force multiplier to control the strength of the force applied
    // public float forceMultiplier = 100.0f;

    private float lastShakeTime;

    void Start()
    {
        myMaster = GetComponentInParent<Master>();
        // Ensure the accelerometer is enabled
        if (Accelerometer.current != null)
        {
            InputSystem.EnableDevice(Accelerometer.current);
            TextAccelarate.text = "Device enabled";
            CanShake = true;  // Allow shaking
            ShakeButton.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            // If the accelerometer is not available, disable the shake functionality
            TextAccelarate.text = "No accelerometer device";
            // CanShake = false;
            // PlaceButton.interactable = true;
            // PlaceButton.enabled = true;
            // hasShaked = true;
        }

        MyBag = GetComponent<PeperBagController>();
    }

    void Update()
    {
        if (myMaster.stage == 2)
        {


            // Ensure accelerometer is available and enabled
            if (Accelerometer.current != null && CanShake)
            {
                shaking = true;

                acceleration = new Vector2(Accelerometer.current.acceleration.ReadValue().x, Accelerometer.current.acceleration.ReadValue().y);

                // Check if the magnitude of the acceleration exceeds the threshold
                // if (Mathf.Abs(acceleration.x) > shakeThreshold)
                // {
                TextAccelarate.text = "enabled " + acceleration.ToString() + " and shaken " + shakenFor.ToString();
                // TextAccelarate.text = "shake";
                // if (Time.time - lastShakeTime > shakeCooldown)
                // {
                // lastShakeTime = Time.time;
                ApplyShakeForce(acceleration);


                if (Mathf.Abs(acceleration.x) > shakeThreshold)
                {
                    shakenFor = shakenFor + 1f;
                }

                if (shakenFor > 12)
                {
                    finishShake();
                }
                else
                {
                    if (shakenFor < 6)
                    {
                        // myTipsController.tipsText.text = "Dont be afraid, shake your phone";
                        myTipsController.ChangeText("Dont be afraid, shake your phone", myTipsController.centerPos);

                    }
                    else
                    {
                        myTipsController.ChangeText("Shake some more", myTipsController.centerPos);
                        //   myTipsController.tipsText.text = "Shake some more";
                    }
                }
                // // }
                // else
                // {
                //     TextAccelarate.color = new Color(1, 1, 1); // Default color (white)
                //   

                // }
            }
            else
            {
                shaking = false;

                if (Accelerometer.current == null)
                {
                    if (hasShaked == false)
                    {
                        ShakeButton.transform.parent.gameObject.SetActive(true);
                        myTipsController.ChangeText("I cant find accelerometer in your device, tap the SHAKE button", myTipsController.centerPos);
                        // myTipsController.tipsText.text = "I cant find accelerometer in your device, tap the SHAKE button";
                        myTipsController.changeFlashingText(ShakeButton, null);
                    }

                    // PlaceButton.interactable = true;
                    // PlaceButton.enabled = true;
                    // hasShaked = true;
                }
                // Handle the case where the accelerometer is unavailable

            }
        }
        else
        {
            shaking = false;
        }
    }


    private void ApplyShakeForce(Vector2 shakeDirection)
    {
        // Convert 3D shake direction to a 2D direction (ignoring the z-axis)
        // Vector2 forceDirection = new Vector2(shakeDirection, shakeDirection).normalized;

        // Apply force to each Rigidbody2D in the list
        foreach (Rigidbody2D rb in MyBag.PaperCutRBs)
        {
            // Only apply force if the Rigidbody2D is not null
            if (rb != null)
            {
                // rb.AddForce(forceDirection * shakeDirection.magnitude * 10);
                rb.AddForce(shakeDirection * 500000);
            }
        }

        Debug.Log("Shake force applied to all Rigidbodies!");
    }

    public void finishShake()
    {
        // myTipsController.tipsText.text = "Tap the PLACE button to continue";
        myTipsController.ChangeText("Tap the PLACE button to continue", myTipsController.centerPos);
        myTipsController.changeFlashingText(myTipsController.placeButtonText, null);
        PlaceButton.interactable = true;
        PlaceButton.enabled = true;
        hasShaked = true;
    }
}
