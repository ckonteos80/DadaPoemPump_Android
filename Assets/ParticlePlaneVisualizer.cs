using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ParticlePlaneVisualizer : MonoBehaviour
{

    public ARPlane arPlane;
    public ParticleSystem particleSystem;
    public LineRenderer lineRenderer;

    // public GameObject ARparticle;

    // public GameObject ARparticleHorizontal;

    // public GameObject ARparticleVertical;

    public Material ARparticleHorizontal;

    public Material ARparticleVertical;
    public Material ARlineHorizontal;

    public Material ARlineVertical;

    void Awake()
    {
        // Get the ARPlane and ParticleSystem components from the prefab
        arPlane = GetComponent<ARPlane>();
        particleSystem = GetComponent<ParticleSystem>();
        lineRenderer = GetComponent<LineRenderer>();

        // Optional: Disable particle system by default
        // if (particleSystem != null)
        // {
        //     particleSystem.Stop();
        // }
    }

    void Start()
    {
        // Check the plane alignment at the start
        CheckPlaneAlignment();
    }

    private void CheckPlaneAlignment()
    {
        if (arPlane == null)
            return;

        // Change behavior based on plane alignment
        switch (arPlane.alignment)
        {
            case PlaneAlignment.HorizontalUp:
                // Code for horizontal plane
                Debug.Log("This is a horizontal plane.");
                // Make changes specific to horizontal planes, e.g., change material, scale, etc.
                ChangeToHorizontalAppearance();
                break;

            case PlaneAlignment.Vertical:
                // Code for vertical plane
                Debug.Log("This is a vertical plane.");
                // Make changes specific to vertical planes
                ChangeToVerticalAppearance();
                break;

            default:
                Debug.Log("This is an unsupported plane type.");
                break;
        }
    }

    private void ChangeToHorizontalAppearance()
    {
        ParticleSystemRenderer particleRenderer = particleSystem.GetComponent<ParticleSystemRenderer>();
        particleRenderer.material = ARparticleHorizontal;
        lineRenderer.material = ARlineHorizontal;

        // ARparticle = Instantiate(ARparticleHorizontal);
        // ARparticle.transform.SetParent(gameObject.transform);
        // particleSystem = ARparticle.GetComponent<ParticleSystem>();

        // Example: Change the color of the plane or its visibility
        // GetComponent<Renderer>().material.color = Color.green; // Change to green for horizontal
        // Additional modifications for horizontal planes
    }

    private void ChangeToVerticalAppearance()
    {
        ParticleSystemRenderer particleRenderer = particleSystem.GetComponent<ParticleSystemRenderer>();
        particleRenderer.material = ARparticleVertical;
        lineRenderer.material = ARlineVertical;

        //  ARparticle = Instantiate(ARparticleVertical);
        // ARparticle.transform.SetParent(gameObject.transform);
        // particleSystem = ARparticle.GetComponent<ParticleSystem>();

        // Example: Change the color of the plane or its visibility
        // GetComponent<Renderer>().material.color = Color.blue; // Change to blue for vertical
        // Additional modifications for vertical planes
    }

    void OnEnable()
    {
        // Subscribe to boundaryChanged event (updated from boundaryUpdated)
        arPlane.boundaryChanged += OnBoundaryChanged;
    }

    void OnDisable()
    {
        // Unsubscribe from boundaryChanged event
        arPlane.boundaryChanged -= OnBoundaryChanged;
    }

    // Event handler for boundary changes on the AR plane
    void OnBoundaryChanged(ARPlaneBoundaryChangedEventArgs args)
    {
        // Update the particle system when the AR plane's boundary is updated
        if (particleSystem != null && arPlane != null)
        {
            // Get the AR plane's mesh
            Mesh planeMesh = arPlane.GetComponent<MeshFilter>().mesh;

            // Update the particle system's shape to use the AR plane's mesh
            var shape = particleSystem.shape;
            shape.shapeType = ParticleSystemShapeType.Mesh;
            shape.mesh = planeMesh;

            // Play the particle system if it's not already playing
            if (!particleSystem.isPlaying)
            {
                particleSystem.Play();
            }
        }
    }
}
