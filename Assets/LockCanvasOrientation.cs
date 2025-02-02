using UnityEngine;

public class LockCanvasOrientation : MonoBehaviour
{
    private Quaternion initialRotation;

    void Start()
    {
        // Store the canvas's initial rotation
        initialRotation = transform.rotation;
    }

    void Update()
    {
        // Reset the canvas rotation to its initial rotation
        transform.rotation = initialRotation;
    }
}
