using UnityEngine;
using System.Collections;

//Can be moved to backend
public class GoalStatusHolder : MonoBehaviour
{
    [SerializeField]
    private int goalID;

    public int GoalID
    {
        get
        {
            return goalID;
        }

        set
        {
            goalID = value;
        }
    }
}
