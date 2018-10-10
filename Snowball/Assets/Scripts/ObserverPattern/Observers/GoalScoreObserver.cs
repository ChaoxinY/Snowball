using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalScoreObserver : Observer 
{
    public Text uiGoalScoreText;
    private int currentScore;

    public int CurrentScore
    {
        get
        {
            return currentScore;
        }

        set
        {
            currentScore = value;
        }
    }

    public void AddScorePoints(int ballPointValue)
    {
        CurrentScore += ballPointValue;
        uiGoalScoreText.text = CurrentScore.ToString();
    }

    public override void OnNotify(string eventName, GameObject[] associatedGameObjects)
    {
        int ballPointValue = associatedGameObjects[0].GetComponent<SnowBallStatusManager>().SnowBallPointValue;
        AddScorePoints(ballPointValue);
    }
}
