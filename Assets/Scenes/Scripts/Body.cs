using UnityEngine;
using UnityEngine.Serialization;

public class Body : MonoBehaviour
{
    [FormerlySerializedAs("left")] [SerializeField] private Hand leftHand;
    [FormerlySerializedAs("right")] [SerializeField] private Hand rightHand;
    [SerializeField] private Hand leftFoot;
    [SerializeField] private Hand rightFoot;
    [SerializeField] private GameObject wall;

    enum CombinedHandStates
    {
        LeftHand,
        RightHand,
        LeftFoot,
        RightFoot,
        Neither
    };

    private Vector3 wallOffsetLeft;
    private Vector3 wallOffsetRight;
    private Vector3 wallOffsetLeftFoot;
    private Vector3 wallOffsetRightFoot;
    private bool handAlreadyEngaged;

    void Start()
    {
        handAlreadyEngaged = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (GetCombinedStates())
        {
            case CombinedHandStates.Neither:
                wallOffsetLeft = wall.transform.position - leftHand.transform.position;
                wallOffsetRight = wall.transform.position - rightHand.transform.position;
                wallOffsetLeftFoot = wall.transform.position - leftFoot.transform.position;
                wallOffsetRightFoot = wall.transform.position - rightFoot.transform.position;
                break;
            case CombinedHandStates.LeftHand:
                wall.transform.position = leftHand.transform.position + wallOffsetLeft;
                wallOffsetRight = wall.transform.position - rightHand.transform.position;
                wallOffsetLeftFoot = wall.transform.position - leftFoot.transform.position;
                wallOffsetRightFoot = wall.transform.position - rightFoot.transform.position;
                break;
            case CombinedHandStates.RightHand:
                wall.transform.position = rightHand.transform.position + wallOffsetRight;
                wallOffsetLeft = wall.transform.position - leftHand.transform.position;
                wallOffsetLeftFoot = wall.transform.position - leftFoot.transform.position;
                wallOffsetRightFoot = wall.transform.position - rightFoot.transform.position;
                break;
            case CombinedHandStates.LeftFoot:
                wall.transform.position = leftFoot.transform.position + wallOffsetLeftFoot;
                wallOffsetLeft = wall.transform.position - leftHand.transform.position;
                wallOffsetRight = wall.transform.position - rightHand.transform.position;
                wallOffsetRightFoot = wall.transform.position - rightFoot.transform.position;
                break;
            case CombinedHandStates.RightFoot:
                wall.transform.position = rightFoot.transform.position + wallOffsetRightFoot;
                wallOffsetLeft = wall.transform.position - leftHand.transform.position;
                wallOffsetRight = wall.transform.position - rightHand.transform.position;
                wallOffsetLeftFoot = wall.transform.position - leftFoot.transform.position;
                break;
        }
    }

    CombinedHandStates GetCombinedStates()
    {
        if (rightHand.GetHandState() == leftHand.GetHandState() 
            && rightHand.GetHandState() == leftFoot.GetHandState() 
            && rightHand.GetHandState() == rightFoot.GetHandState() 
            && rightHand.GetHandState() == Hand.HandState.Open)
        {
            return CombinedHandStates.Neither;
        } 
        else if (rightHand.GetHandState() == Hand.HandState.Closed)
        {
            return CombinedHandStates.RightHand;
        }
        else if (leftHand.GetHandState() == Hand.HandState.Closed)
        {
            return CombinedHandStates.LeftHand;
        }
        else if (leftFoot.GetHandState() == Hand.HandState.Closed)
        {
            return CombinedHandStates.LeftFoot;
        } 
        else
        {
            return CombinedHandStates.RightFoot;
        }
    }

    public bool GetHandAlreadyEngaged()
    {
        return handAlreadyEngaged;
    }
}