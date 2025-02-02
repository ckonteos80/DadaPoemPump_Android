using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DestroyFlash : MonoBehaviour
{
    /// <summary>
    /// float maxTime;
    /// </summary>
    // public float timeToDestroy;

    // // Start is called before the first frame update
    // void Start()
    // {
    //     Destroy(gameObject, timeToDestroy);
    // }

    // // Update is called once per frame
    // void Update()
    // {

    // }

    public bool isTrue;         // The bool variable to be set to true
    public float timeToDestroy;
    public bool isPicture;

    void Start()
    {
        if (isPicture)
        {
            timeToDestroy = Random.Range(0.5f, 0.75f);
        }

        // Start the coroutine to set the bool to true after the delay
        StartCoroutine(SetBoolAfterDelay());

    }

    IEnumerator SetBoolAfterDelay()
    {
        // Wait for the specified amount of time
        yield return new WaitForSeconds(timeToDestroy);

        // Set the bool to true
        isTrue = true;
    }
}
