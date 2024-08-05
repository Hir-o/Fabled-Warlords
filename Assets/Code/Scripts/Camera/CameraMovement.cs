using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private CameraSettings cameraSettings;
    private Camera _camera;
    private Vector3 _mouseScrollStartPos;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        PanCamera();
        ZoomCamera();
    }

    private void PanCamera()
    {
        if (Input.GetMouseButtonDown(2))
        {
            _mouseScrollStartPos = _camera.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(2))
        {
            var movement = Vector3.zero;
            movement = _camera.ScreenToWorldPoint(Input.mousePosition) - _mouseScrollStartPos;
            _camera.transform.position -= movement;
            
            ClampCameraPosition();
        }
    }

    private void ZoomCamera()
    {
        var scrollData = Input.GetAxis("Mouse ScrollWheel");

        if (scrollData == 0.0f) return;
        _camera.orthographicSize -= scrollData * cameraSettings.zoomSpeed;
        _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize, cameraSettings.minCamSize, cameraSettings.maxCamSize);

        ClampCameraPosition();
    }

    private void ClampCameraPosition()
    {
        float camHeight = _camera.orthographicSize;
        float camWidth = _camera.aspect * camHeight;

        float minX = cameraSettings.minBounds.x + camWidth;
        float maxX = cameraSettings.maxBounds.x - camWidth;
        float minY = cameraSettings.minBounds.y + camHeight;
        float maxY = cameraSettings.maxBounds.y - camHeight;

        var clampedPosition = _camera.transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, minY, maxY);
        _camera.transform.position = clampedPosition;
    }
}