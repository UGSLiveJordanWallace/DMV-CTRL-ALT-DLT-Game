using UnityEngine;

public class BodyWindow : MonoBehaviour
{
    [Header("Joint Identity")]
    public string jointName; 

    [Header("References")]
    public GameObject fogOverlay;   // The "Fog of War" sprite
    public GameObject controlGroup; // Object holding the Dial and Lever

    [Header("Current Joint Output")]
    [Range(0f, 1f)] public float moveMagnitude; // Dial value
    public bool isArmMode;                      // Lever value (True=Arms, False=Legs)

    private bool _isPlayerInside = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) SetState(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) SetState(false);
    }

    private void SetState(bool active)
    {
        _isPlayerInside = active;
        if (fogOverlay != null) fogOverlay.SetActive(!active);
        if (controlGroup != null) controlGroup.SetActive(active);
    }

    public bool CanInteract() => _isPlayerInside;
}