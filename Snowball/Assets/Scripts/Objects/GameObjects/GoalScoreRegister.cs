using UnityEngine;
using UnityEngine.UI;

//Event Receiver
public class GoalScoreRegister : MonoBehaviour
{
	[SerializeField]
	private Text[] uiGoalScoreTexts;
	private int[] currentScore = new int[2];
	public EventSubject eventSubject;

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

	private void Start()
	{
		SubscribeEvent();
	}

	private void AddScorePoints(int ballPointValue, int goalID)
	{
		CurrentScore[goalID] += ballPointValue;
		uiGoalScoreTexts[goalID].text = CurrentScore[goalID].ToString();
	}

	public void OnCollisionWithGoal(object source, GameObjectEventArgs args)
	{
		Debug.Log("Called");
		SnowBallStatusHolder snowBallStatusManager = args.GameObject.GetComponent<ISnowBallStatusHolder>().SnowBallStatusHolder;
		int ballPointValue = snowBallStatusManager.SnowBallPointValue;
		int goalID = snowBallStatusManager.LastContactedGoalID;
		AddScorePoints(ballPointValue, goalID);
	}

	public void SubscribeEvent()
	{
		foreach (System.Object eventPublisher in eventSubject.EventPublishers)
		{
			if (eventPublisher.GetType()== typeof(SnowBallCollisionHandler))
			{
				SnowBallCollisionHandler snowBallCollisionHandler = (SnowBallCollisionHandler)eventPublisher;
				snowBallCollisionHandler.CollidedWithGoal += OnCollisionWithGoal;			
			}
		}
	}
}
