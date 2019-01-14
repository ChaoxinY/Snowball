using UnityEngine;
using System.Collections;

/*
 *Responsibility : Adds values to snowball status holder
 */
public class Snowpile : MonoBehaviour 
{   
    [SerializeField]
    private int snowBallPointValueToAdd;
    [SerializeField]
    private float snowBallSizeMultiplier;

    public void PileUpSnowBall()
    {
        SnowBallStatusHolder ballStatusHolder = GameObject.Find("SnowBall").GetComponent<ISnowBallStatusHolder>().GetSnowBallStatusHolder();
        if (ballStatusHolder != null && ballStatusHolder.SnowballOwner != null)
        {
            ballStatusHolder.SnowBallPointValue += snowBallPointValueToAdd;
            ballStatusHolder.SnowBallSizeMultiplier += snowBallSizeMultiplier;
        }
        Destroy(gameObject);
    }
}
