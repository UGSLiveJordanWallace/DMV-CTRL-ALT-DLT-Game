using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class JointDial : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public BodyWindow parentWindow;
    public Transform dialVisual;
    public float returnSpeed = 5f;

    private bool _isDragging = false;
    private Quaternion _initialRotation;

    private float lastAngle;

    void Start()
    {
        _initialRotation = dialVisual.localRotation;
        lastAngle = 0f;
    }

    void Update()
    {
        // If player leaves the room while dragging, force release
        if (parentWindow != null && !parentWindow.CanInteract()) _isDragging = false;

        if (_isDragging)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector2 direction = mousePos - dialVisual.position;
            float angle = 0;
            if (direction.y < 0)
            {
                angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                angle += 360f;
            }
            else
            {
                angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            }
            dialVisual.rotation = Quaternion.Euler(0, 0, angle);
            lastAngle = angle;
        }
        else
        {
            // Idle state: Snap back to original position
            dialVisual.localRotation = Quaternion.Slerp(dialVisual.localRotation, _initialRotation, Time.deltaTime * returnSpeed);
        }
    }

    public float GetSliderValue()
    {
        return lastAngle / 360f;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (parentWindow.CanInteract()) _isDragging = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isDragging = false;
    }
}