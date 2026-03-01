using UnityEngine;

public class JointDial : MonoBehaviour
{
    public BodyWindow parentWindow;
    public Transform dialVisual;
    public float returnSpeed = 5f;

    private bool _isDragging = false;
    private Quaternion _initialRotation;

    void Start()
    {
        _initialRotation = dialVisual.localRotation;
    }

    void Update()
    {
        // If player leaves the room while dragging, force release
        if (parentWindow != null && !parentWindow.CanInteract()) _isDragging = false;

        if (_isDragging)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = mousePos - dialVisual.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            dialVisual.rotation = Quaternion.Euler(0, 0, angle - 90f);
        }
        else
        {
            // Idle state: Snap back to original position
            dialVisual.localRotation = Quaternion.Slerp(dialVisual.localRotation, _initialRotation, Time.deltaTime * returnSpeed);
        }
    }

    private void OnMouseDown() { if (parentWindow.CanInteract()) _isDragging = true; }
    private void OnMouseUp() { _isDragging = false; }
}