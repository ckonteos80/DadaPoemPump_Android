using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlaceOnTap : MonoBehaviour
{
    public GameObject PrefabToPlace;
    public GameObject PlacedPrefab;
    private ARRaycastManager aRRaycastManager;
    public ARPlaneManager aRPlaneManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    public ARconnect myConnect;

    public bool isPlacing;

    public bool Placed;

    public Transform trackableHolders;

    private InputAction touchAction;

    void Awake()
    {
        aRRaycastManager = GetComponent<ARRaycastManager>();
        aRPlaneManager = GetComponent<ARPlaneManager>();

        // Initialize touch action for the new Input System
        touchAction = new InputAction(type: InputActionType.PassThrough, binding: "<Touchscreen>/primaryTouch/position");
        touchAction.Enable();
    }

    void OnEnable()
    {
        touchAction.Enable();
    }

    void OnDisable()
    {
        touchAction.Disable();
    }

    void Start()
    {
        foreach (ARPlane plane in aRPlaneManager.trackables)
        {
            Destroy(plane.gameObject);  // Destroys the plane's GameObject
        }
    }

    void Update()
    {
        if (isPlacing)
        {
            if (!myConnect.myMaster.BackgroundImage.gameObject.activeSelf)
            {
                if (!Placed)
                {
                    if (aRPlaneManager.trackables.count == 0)
                    {
                        myConnect.myMaster.myTipsController.ChangeText("Slowly move your phone to scan for surfaces to place your poem", myConnect.myMaster.myTipsController.centerPos);
                        //    myConnect.myMaster.myTipsController.tipsText.text = "Slowly move your phone to scan for surfaces to place your poem";
                    }
                    else
                    {
                        myConnect.myMaster.myTipsController.ChangeText("Tap on a highlighted surface to place your poem", myConnect.myMaster.myTipsController.centerPos);
                        //    myConnect.myMaster.myTipsController.tipsText.text = "Tap on a highlighted surface to place your poem";
                    }
                }
                else
                {
                    myConnect.myMaster.myTipsController.ChangeText("Tap on the SET button to hide available surfaces", myConnect.myMaster.myTipsController.centerPos);
             //       myConnect.myMaster.myTipsController.tipsText.text = "Tap on the SET button to hide available surfaces";
                    myConnect.myMaster.myTipsController.changeFlashingText(myConnect.myMaster.myTipsController.ARsetButtonText, null);
                }
            }

            if (EventSystem.current == null)
            {
                Debug.LogWarning("EventSystem is missing!");
            }

            // Check if there's an active touch
            if (!Touchscreen.current.primaryTouch.press.isPressed)
                return;

            // Get touch position
            Vector2 touchPosition = touchAction.ReadValue<Vector2>();
            if (IsTouchOverUI(touchPosition))
                return;

            Debug.Log("Processing touch on a non-UI element.");

            if (aRRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
            {
                var hitPose = hits[0].pose;
                var hitTrackableId = hits[0].trackableId;

                // Get the ARPlane that was hit
                ARPlane hitPlane = aRPlaneManager.GetPlane(hitTrackableId);

                if (PlacedPrefab == null)
                {
                    PlacedPrefab = Instantiate(PrefabToPlace, hitPose.position, GetSimplifiedRotationForPlane(hitPlane));

                    // Set center anchors and pivot for the RectTransform
                    RectTransform rectTransform = PlacedPrefab.GetComponent<RectTransform>();
                    rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                    rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                    rectTransform.pivot = new Vector2(0.5f, 0.5f);

                    PlacedPrefab.SetActive(true);
                }
                else
                {
                    PlacedPrefab.SetActive(true);

                    RectTransform rectTransform = PlacedPrefab.GetComponent<RectTransform>();
                    rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                    rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                    rectTransform.pivot = new Vector2(0.5f, 0.5f);

                    PlacedPrefab.transform.position = hitPose.position;
                    PlacedPrefab.transform.rotation = GetSimplifiedRotationForPlane(hitPlane);
                    Placed = true;
                }
            }
        }
    }

    public bool IsTouchOverUI(Vector2 touchPosition)
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
        {
            position = touchPosition
        };

        List<RaycastResult> results = new List<RaycastResult>();

        foreach (GraphicRaycaster raycaster in FindObjectsOfType<GraphicRaycaster>())
        {
            raycaster.Raycast(pointerEventData, results);
            if (results.Count > 0)
            {
                foreach (var result in results)
                {
                    Debug.Log("UI element detected: " + result.gameObject.name);
                }
                return true;
            }
        }
        return false;
    }

    private Quaternion GetSimplifiedRotationForPlane(ARPlane plane)
    {
        if (plane.alignment == PlaneAlignment.HorizontalUp || plane.alignment == PlaneAlignment.HorizontalDown)
        {
            return Quaternion.Euler(90, 0, 0);
        }
        else if (plane.alignment == PlaneAlignment.Vertical)
        {
            return Quaternion.Euler(0, 0, 0);
        }

        return Quaternion.identity;
    }

    public void setPrefabs(bool state)
    {
        aRPlaneManager.enabled = state;

        foreach (ARPlane plane in aRPlaneManager.trackables)
        {
            plane.gameObject.SetActive(state);
        }
    }
}














// public class PlaceOnTap : MonoBehaviour
// {
//     public GameObject PrefabToPlace;
//     public GameObject PlacedPrefab;
//     private ARRaycastManager aRRaycastManager;
//     public ARPlaneManager aRPlaneManager;
//     private List<ARRaycastHit> hits = new List<ARRaycastHit>();

//     public ARconnect myConnect;

//     public bool isPlacing;

//     public bool Placed;

//     public Transform trackableHolders;



//     void Awake()
//     {
//         aRRaycastManager = GetComponent<ARRaycastManager>();
//         aRPlaneManager = GetComponent<ARPlaneManager>();
//     }

//     void Start()
//     {
//         foreach (ARPlane plane in aRPlaneManager.trackables)
//         {
//             Destroy(plane.gameObject);  // Destroys the plane's GameObject
//         }
//     }


//     void Update()
//     {
//         if (isPlacing)
//         {
//             if (!myConnect.myMaster.BackgroundImage.gameObject.activeSelf)
//             {
//                 if (!Placed)
//                 {
//                     if (aRPlaneManager.trackables.count == 0)
//                     {
//                         myConnect.myMaster.myTipsController.tipsText.text = "Slowly move your phone to scan for surfaces to place your poem";
//                     }
//                     else
//                     {
//                         myConnect.myMaster.myTipsController.tipsText.text = "Tap on a highlighted surface to place your poem";
//                     }
//                 }
//                 else
//                 {
//                     myConnect.myMaster.myTipsController.tipsText.text = "Tap on the SET button to hide available surfaces";
//                     myConnect.myMaster.myTipsController.changeFlashingText(myConnect.myMaster.myTipsController.ARsetButtonText, null);
//                 }
//             }

//             if (EventSystem.current == null)
//             {
//                 Debug.LogWarning("EventSystem is missing!");
//             }
//             if (Input.touchCount == 0)
//                 return;

//             Touch touch = Input.GetTouch(0);
//             // If no touch, return
//             if (IsTouchOverUI(touch))
//                 return;


//             // Get the touch input
//             // Touch touch = Input.GetTouch(0);


//             if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
//             {
//                 Debug.Log("Touch is over a UI element, ignoring this touch.");
//                 return;  // Ignore the touch if it's over a UI element
//             }
//             else
//             {
//                 Debug.Log("No UI element detected, processing touch.");
//             }
//             // Raycast to check if the touch hits any plane

//             // if (IsTouchOverUI(touch))
//             // {
//             //     Debug.Log("Touch is over a UI element, ignoring this touch.");
//             //     return;  // Ignore the touch if it's over a UI element
//             // }
//             // else
//             // {





//             if (aRRaycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
//             // if (aRRaycastManager.Raycast(Input.GetTouch(0).position, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
//             {
//                 var hitPose = hits[0].pose;
//                 var hitTrackableId = hits[0].trackableId;

//                 // Get the ARPlane that was hit
//                 ARPlane hitPlane = aRPlaneManager.GetPlane(hitTrackableId);

//                 if (PlacedPrefab == null)
//                 {
//                     // Instantiate object with rotation based on horizontal or vertical plane
//                     PlacedPrefab = Instantiate(PrefabToPlace, hitPose.position, GetSimplifiedRotationForPlane(hitPlane));

//                     // Update the position and rotation of the existing object
//                     PlacedPrefab.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
//                     PlacedPrefab.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
//                     // Set pivot to (0.5, 0.5) for center pivot
//                     PlacedPrefab.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);

//                     PlacedPrefab.SetActive(true);

//                     // PlacedPrefab.GetComponentInChildren<RectTransform>().localScale = ARscale;


//                 }
//                 else
//                 {
//                     PlacedPrefab.SetActive(true);

//                     // Update the position and rotation of the existing object
//                     PlacedPrefab.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
//                     PlacedPrefab.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
//                     // Set pivot to (0.5, 0.5) for center pivot
//                     PlacedPrefab.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);

//                     PlacedPrefab.transform.position = hitPose.position;
//                     PlacedPrefab.transform.rotation = GetSimplifiedRotationForPlane(hitPlane);
//                     Placed = true;
//                 }
//             }
//             // }
//         }
//     }

//     public bool IsTouchOverUI(Touch touch)
//     {
//         PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
//         {
//             position = touch.position // Set the position of the touch
//         };

//         // List to store the results of the raycast
//         List<RaycastResult> results = new List<RaycastResult>();

//         // Loop through all active canvases with a GraphicRaycaster and check if touch is over any UI
//         foreach (GraphicRaycaster raycaster in FindObjectsOfType<GraphicRaycaster>())
//         {
//             raycaster.Raycast(pointerEventData, results);

//             // If there are any results (UI elements hit), return true
//             if (results.Count > 0)
//             {
//                 foreach (var result in results)
//                 {
//                     Debug.Log("UI element detected: " + result.gameObject.name);
//                 }
//                 return true;
//             }
//         }

//         // If no UI element was hit, return false
//         return false;
//     }

//     // Function to return specific rotation based on whether the plane is horizontal or vertical
//     private Quaternion GetSimplifiedRotationForPlane(ARPlane plane)
//     {
//         // Check if the plane is horizontal or vertical
//         if (plane.alignment == PlaneAlignment.HorizontalUp || plane.alignment == PlaneAlignment.HorizontalDown)
//         {
//             // Horizontal plane: Set X rotation to 90 degrees, Y and Z to 0
//             return Quaternion.Euler(90, 0, 0);
//         }
//         else if (plane.alignment == PlaneAlignment.Vertical)
//         {
//             // Vertical plane: Set all rotations to 0
//             return Quaternion.Euler(0, 0, 0);
//         }

//         // Default to no rotation (in case the plane is neither strictly horizontal nor vertical)
//         return Quaternion.identity;
//     }

//     public void setPrefabs(bool state)
//     {
//         aRPlaneManager.enabled = state;

//         // Hide all currently tracked planes
//         foreach (ARPlane plane in aRPlaneManager.trackables)
//         {
//             plane.gameObject.SetActive(state);
//         }

//     }
// }
