using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalScoreObserver : Observer 
{   
    [SerializeField]
    private Text[] uiGoalScoreTexts;
    private int[] currentScore = new int[2];

    public int[] CurrentScore
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

    public void AddScorePoints(int ballPointValue,int goalID)
    {
        CurrentScore[goalID] += ballPointValue;
        uiGoalScoreTexts[goalID].text = CurrentScore[goalID].ToString();
    }

    public override void OnNotify(string eventName, GameObject[] associatedGameObjects)
    {
        if (eventName == "GoalEvent")
        {
            SnowBallStatusHolder snowBallStatusManager = associatedGameObjects[0].GetComponent<SnowBallStatusHolder>();
            int ballPointValue = snowBallStatusManager.SnowBallPointValue;
            int goalID = snowBallStatusManager.LastContactedGoalID;
            AddScorePoints(ballPointValue, goalID);
        }
    }
}
