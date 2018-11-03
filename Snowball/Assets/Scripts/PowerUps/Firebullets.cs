using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firebullets : PowerUp
{
    public override void Activate()
    {   
        Debug.Log(SnowBallStatusHolder.SnowBallSizeMultiplier + " "+ SnowBallStatusHolder.SnowBallPointValue);
    }
}
