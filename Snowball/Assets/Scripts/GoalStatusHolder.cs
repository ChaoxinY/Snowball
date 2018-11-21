using UnityEngine;
using System.Collections;

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
