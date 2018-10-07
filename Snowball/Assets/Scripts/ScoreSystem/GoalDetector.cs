using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalDetector : MonoBehaviour
{
    private GoalScoreCounter goalScoreCounter;

    private void Start()
    {
        goalScoreCounter = GetComponent<GoalScoreCounter>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "SnowBall")
        {
            goalScoreCounter.AddScorePoints(collision.gameObject.GetComponent<SnowBallStatusManager>().SnowBallPointValue);
        }
    }
}
