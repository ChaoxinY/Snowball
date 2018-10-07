using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBallStatusManager : MonoBehaviour
{
    private int snowBallPointValue;

    private void Start()
    {
        SnowBallPointValue = 50;
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
}
