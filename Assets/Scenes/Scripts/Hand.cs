using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.UI;

public class Hand : MonoBehaviour
{
    [SerializeField] private new SpriteRenderer renderer;
    [SerializeField] private JointLever handClosed;

    public enum HandState
    {
        Open,
        Closed
    };

    private HandState _handState = HandState.Open;
    private Vector3 offsetState;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _handState = HandState.Open;
        offsetState = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        switch (_handState)
        {
            case HandState.Open:
                if (handClosed.isOn)
                {
                    _handState = HandState.Closed;
                }
                ColorUtility.TryParseHtmlString("#ff7740", out Color baseColor);
                renderer.color = baseColor;
                break;
            case HandState.Closed:
                if (!handClosed.isOn)
                {
                    _handState = HandState.Open;
                }
                renderer.color = Color.blue;
                break;
        }
        offsetState = transform.position;
    }

    public HandState GetHandState()
    {
        return _handState;
    }

    public Vector3 GetOffset()
    {
        Vector3 currentPosition = transform.position;
        if (currentPosition != offsetState)
        {
            return currentPosition - offsetState;
        } else
        {
            return Vector3.zero;
        }
    }
}