using UnityEngine;
using UnityEngine.UI;

public class Hand : MonoBehaviour
{
    [SerializeField] private new SpriteRenderer renderer;
    [SerializeField] private Toggle handClosed;
    public enum HandState
    {
        Open,
        Closed
    };
    private HandState _handState = HandState.Open;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _handState = HandState.Open;
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
    }

    public HandState GetHandState()
    {
        return _handState;
    }

}