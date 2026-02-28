using UnityEditor.U2D.Animation;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.UI;

public class Joint : MonoBehaviour
{
    [SerializeField] private GameObject limb;
    public Slider slider;

    [SerializeField] private Joint subjoint;
    [SerializeField] private bool isRight = false;

    private bool rotateable = true;

    void Update()
    {
        limb.transform.position = transform.position;
        if (rotateable)
        {
            Rotate(slider.value);
        }
    }

    float GetLength(GameObject obj)
    {
        return (transform.position - obj.transform.position).magnitude;
    }

    public void Rotate(float degrees)
    {
        if (subjoint)
        {
            subjoint.SetRotateable(false);
        }
        limb.transform.rotation = Quaternion.Euler(0, 0, isRight ? 360 * degrees : -360 * degrees);
        if (subjoint)
        {
            subjoint.SetRotateable(true);
        }
    }
    public void SetRotateable(bool isRotateable)
    {
        rotateable = isRotateable;
    }
}