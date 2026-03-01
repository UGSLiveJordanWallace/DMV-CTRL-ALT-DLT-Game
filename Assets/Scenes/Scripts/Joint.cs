using UnityEngine;

public class Joint : MonoBehaviour
{
    [SerializeField] private GameObject limb;
    [SerializeField] private JointDial dial;

    [SerializeField] private bool isRight = false;
    private float radius;

    void Start()
    {
        radius = GetLength(limb);
    }

    void Update()
    {
        UpdatePosition();
        UpdateAngle();
    }

    void UpdatePosition()
    {
        float angle = isRight ? dial.GetSliderValue() * 360 - 90f : -(dial.GetSliderValue() * 360) - 90f;
        float x = transform.position.x + radius * Mathf.Cos((Mathf.PI / 180) * angle);
        float y = transform.position.y + radius * Mathf.Sin((Mathf.PI / 180) * angle);
        limb.transform.position = new Vector3(x, y);
    }

    void UpdateAngle()
    {
        Vector3 pos = limb.transform.position - transform.position;
        float angle = 0;
        if (pos.x != 0)
        {
            angle = Mathf.Atan2(pos.y, pos.x) + (Mathf.PI / 2);
        }
        
        limb.transform.rotation = Quaternion.Euler(0, 0, (180 / Mathf.PI) * angle);
    }

    float GetLength(GameObject obj)
    {
        return (transform.position - obj.transform.position).magnitude;
    }
}