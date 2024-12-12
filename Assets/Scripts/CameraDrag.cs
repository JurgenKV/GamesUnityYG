using UnityEngine;
using UnityEngine.EventSystems; // Required for UI detection

public class CameraDrag : MonoBehaviour
{
    [Header("Settings")]
    public float dragSpeed = 2f; // Speed of dragging the camera

    [Header("Sprite Settings")]
    public SpriteRenderer targetSprite; // SpriteRenderer of the target image

    private Camera cam; // Reference to the camera
    private Vector3 dragOrigin; // Initial drag point
    private Vector2 minBounds; // Minimum camera bounds
    private Vector2 maxBounds; // Maximum camera bounds

    [HideInInspector] public float MinCameraSize; // Fixed minimum camera size
    public float MaxCameraSize; // Maximum camera size (calculated)

    private void Awake()
    {
        MinCameraSize = 4f;
        InitializeCamera();
    }

    private void Update()
    {
        // If pointer is over UI, limit movement to bounds only
        if (IsPointerOverUI())
        {
            HandleBoundaryClamp();
            return;
        }

        // Handle normal input
        HandleInput();
    }

    private void InitializeCamera()
    {
        cam = Camera.main;

        if (cam == null)
        {
            Debug.LogError("Main camera not found!");
            return;
        }

        if (targetSprite == null)
        {
            Debug.LogError("SpriteRenderer is not assigned! Please assign it in the Inspector.");
            return;
        }

        CalculateCameraSizeLimits();
        UpdateCameraBounds();
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetDragOrigin(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            DragCamera(Input.mousePosition - dragOrigin);
            SetDragOrigin(Input.mousePosition);
        }

        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 touchDelta = Input.GetTouch(0).deltaPosition;
            DragCamera(touchDelta * dragSpeed * Time.deltaTime);
        }
    }

    private void SetDragOrigin(Vector3 origin)
    {
        dragOrigin = origin;
    }

    private void DragCamera(Vector3 delta)
    {
        Vector3 difference = cam.ScreenToViewportPoint(delta);
        Vector3 move = new Vector3(-difference.x * dragSpeed, -difference.y * dragSpeed, 0);

        transform.Translate(move, Space.World);
        ClampCameraPosition();
    }

    private void ClampCameraPosition()
    {
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minBounds.x, maxBounds.x);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, minBounds.y, maxBounds.y);
        transform.position = clampedPosition;
    }

    public void UpdateCameraBounds()
    {
        if (targetSprite == null || cam == null)
        {
            Debug.LogError("Cannot update bounds: SpriteRenderer or camera is not assigned.");
            return;
        }

        Bounds spriteBounds = targetSprite.bounds;

        float camHeight = cam.orthographicSize * 2;
        float camWidth = camHeight * cam.aspect;

        minBounds = new Vector2(
            spriteBounds.min.x + camWidth / 2,
            spriteBounds.min.y + camHeight / 2
        );

        maxBounds = new Vector2(
            spriteBounds.max.x - camWidth / 2,
            spriteBounds.max.y - camHeight / 2
        );
    }

    public void SetCameraSize(float newSize)
    {
        if (newSize < MinCameraSize || newSize > MaxCameraSize)
        {
            Debug.LogWarning("Camera size is outside valid bounds.");
            newSize = Mathf.Clamp(newSize, MinCameraSize, MaxCameraSize);
        }

        cam.orthographicSize = newSize;
        UpdateCameraBounds();
    }

    public void CalculateCameraSizeLimits()
    {
        if (targetSprite == null || cam == null)
        {
            Debug.LogError("SpriteRenderer or camera is not assigned.");
            return;
        }

        Bounds spriteBounds = targetSprite.bounds;
        float aspectRatio = cam.aspect;

        // Calculate the maximum camera size
        float maxHeight = spriteBounds.size.y / 2; // Height limit
        float maxWidth = spriteBounds.size.x / (2 * aspectRatio); // Width limit

        MaxCameraSize = Mathf.Min(maxHeight, maxWidth);
        // Log the results
        Debug.Log($"Calculated Camera Size Limits: Min = {MinCameraSize}, Max = {MaxCameraSize}");
    }

    private bool IsPointerOverUI()
    {
        // Проверяем стандартное нахождение указателя над объектом UI
        
        // if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        // {
        //     Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Луч от камеры через позицию мыши
        //     RaycastHit hit;
        //     if (Physics.Raycast(ray, out hit))
        //     {
        //         // Проверяем, находится ли объект в слое UI
        //         int uiLayer = LayerMask.NameToLayer("UI");
        //         if (hit.collider.gameObject.layer == uiLayer)
        //         {
        //             return true;
        //         }
        //     }
        // }
        return false;
    }

    private void HandleBoundaryClamp()
    {
        ClampCameraPosition();
    }
}
