using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalScoreCounter : MonoBehaviour
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

    public void AddScorePoints(int ballPointValue) {
        CurrentScore += ballPointValue;
        uiGoalScoreText.text = CurrentScore.ToString();
    }
}
