using UnityEngine;

public class JointLever : MonoBehaviour
{
    public BodyWindow parentWindow;
    public Transform leverHandle; // The part that actually tilts
    
    [Header("Settings")]
    public float tiltAngle = 45f; // How far it tilts left/right
    public bool isOn = false;

    void Update()
    {
        // If the player leaves the room, we might want to reset or just block interaction
        if (parentWindow != null && !parentWindow.CanInteract()) return;

        // Smoothly rotate the handle to the target position
        float targetZ = isOn ? tiltAngle : -tiltAngle;
        Quaternion targetRotation = Quaternion.Euler(0, 0, targetZ);
        leverHandle.localRotation = Quaternion.Slerp(leverHandle.localRotation, targetRotation, Time.deltaTime * 10f);
    }

    private void OnMouseDown()
    {
        // Only flip if the player is actually in the room
        if (parentWindow != null && parentWindow.CanInteract())
        {
            isOn = !isOn;
            Debug.Log(parentWindow.jointName + " Lever is now: " + (isOn ? "ON" : "OFF"));
        }
    }
}