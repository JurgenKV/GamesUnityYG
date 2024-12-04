using UnityEngine;
using UnityEngine.UI;

public class CameraSizeChanger : MonoBehaviour
{
    private Scrollbar _scrollbar = null; 
    private CameraDrag _cameraDrag = null; 
    [SerializeField] private Camera _camera = null; // Camera reference to be assigned via the inspector
    
    void Start()
    {
        _cameraDrag = FindAnyObjectByType<CameraDrag>();
        _cameraDrag.CalculateCameraSizeLimits();
        InitializeScrollbarAndCamera();
        
        if (_camera != null)
        {
            _cameraDrag.SetCameraSize(_cameraDrag.MaxCameraSize/ _cameraDrag.MinCameraSize);
        }
        
        SetScrollbarValueBasedOnCameraSize();
    }

    private void InitializeScrollbarAndCamera()
    {
        // Ensure the scrollbar is assigned
        if (_scrollbar == null)
            _scrollbar = GetComponent<Scrollbar>();

        // Ensure the camera is assigned, otherwise, use the main camera
        if (_camera == null)
            _camera = Camera.main;
    }

    private void SetScrollbarValueBasedOnCameraSize()
    {
        if (_camera != null && _scrollbar != null)
        {
            // Normalize the camera size between the min and max range
            float normalizedSize = Mathf.InverseLerp(_cameraDrag.MinCameraSize, _cameraDrag.MaxCameraSize, _camera.orthographicSize);
            _scrollbar.value = normalizedSize; // Set the scrollbar value
        }
    }

    public void ChangeSize(float newSize)
    {
        // Interpolate the camera size between the minimum and maximum values based on the scrollbar
        float newCameraSize = Mathf.Lerp(_cameraDrag.MinCameraSize, _cameraDrag.MaxCameraSize, newSize);

        // Apply the new camera size
        if (_camera != null)
        {
            _cameraDrag.SetCameraSize(newCameraSize); // Set the camera size using the CameraDrag component
        }
    }
}
