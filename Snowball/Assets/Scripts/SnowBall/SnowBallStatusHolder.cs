using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBallStatusHolder : MonoBehaviour
{
    private int lastContactedGoalID; 
    private int snowBallPointValue;
    private float snowBallSizeMultiplier;
    private GameObject snowballOwner;
     
    private void Start()
    {
        SnowBallPointValue = 50;
        snowBallSizeMultiplier = 1;
        SnowballOwner = GameObject.Find("Player1");
    }

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
            gameObject.transform.localScale = Vector3.one * snowBallSizeMultiplier;
        }
    }
}
