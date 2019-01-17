using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSnowBallStatusHolder", menuName = "Dataholders/SnowBallStatusHolder")]
public class SnowBallStatusHolder : ScriptableObject
{
    private int lastContactedGoalID; 
    [SerializeField]
    private int snowBallPointValue;
    [SerializeField]
    private float snowBallSizeMultiplier;
    private GameObject snowball,snowballOwner;
     
    public int SnowBallPointValue
    {
        get
        {
            return snowBallPointValue;
        }

        set
        {   
            snowBallPointValue = value;
        }
    }

    public int LastContactedGoalID
    {
        get
        {
            return lastContactedGoalID;
        }

        set
        {
            lastContactedGoalID = value;
        }
    }

    public GameObject Snowball
    {
        get
        {
            return snowball;
        }

        set
        {
            snowball = value;
        }
    }

    public float SnowBallSizeMultiplier
    {
        get
        {
            return snowBallSizeMultiplier;
        }

        set
        {
            snowBallSizeMultiplier = value;
            if (snowBallSizeMultiplier > 3) {
                snowBallSizeMultiplier = 3;
            }
            //Make a resizer component from this
            //Changing local scale has nothing to do with storing or accessing data
            snowball.transform.localScale = Vector3.one * snowBallSizeMultiplier;
        }
    }

    public GameObject SnowballOwner
    {
        get
        {
            return snowballOwner;
        }

        set
        {
            snowballOwner = value;
        }
    }
}
