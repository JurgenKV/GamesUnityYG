using UnityEngine;
using UnityEngine.UI;

public class CameraSizeChanger : MonoBehaviour
{
    private Slider _slider = null;
    private CameraDrag _cameraDrag = null;
    [SerializeField] private Camera _camera = null; // Camera reference to be assigned via the inspector

    void Start()
    {
        _cameraDrag = FindAnyObjectByType<CameraDrag>();
        _cameraDrag.CalculateCameraSizeLimits();
        InitializeScrollbarAndCamera();

        if (_camera != null)
        {
            _cameraDrag.SetCameraSize(_cameraDrag.MaxCameraSize / _cameraDrag.MinCameraSize);
        }

        SetScrollbarValueBasedOnCameraSize();
    }

    private void InitializeScrollbarAndCamera()
    {
        // Ensure the scrollbar is assigned
        if (_slider == null)
            _slider = GetComponent<Slider>();

        // Ensure the camera is assigned, otherwise, use the main camera
        if (_camera == null)
            _camera = Camera.main;
    }

    private void SetScrollbarValueBasedOnCameraSize()
    {
        if (_camera != null && _slider != null)
        {
            // Reverse normalization: larger size -> smaller value
            float reversedNormalizedSize = Mathf.InverseLerp(_cameraDrag.MaxCameraSize, _cameraDrag.MinCameraSize, _camera.orthographicSize);
            _slider.value = reversedNormalizedSize; // Set the scrollbar value
        }
    }

    public void ScrollHelp()
    {
        _camera.transform.position = new Vector3(0, 0, -10);
        _slider.value = 0;
        ChangeSize(_slider.value);
    }

    public void ChangeSize(float newSize)
    {
        // Reverse interpolation: smaller scrollbar value -> larger camera size
        float newCameraSize = Mathf.Lerp(_cameraDrag.MaxCameraSize, _cameraDrag.MinCameraSize, newSize);

        // Apply the new camera size
        if (_camera != null)
        {
            _cameraDrag.SetCameraSize(newCameraSize); // Set the camera size using the CameraDrag component
        }
    }
}