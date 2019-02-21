using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Event Receiver
public class GoalScoreRegister : MonoBehaviour, IEventHandler
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
		SnowBallStatusHolder snowBallStatusManager = args.GameObject.GetComponent<ISnowBallStatusHolder>().GetSnowBallStatusHolder();
		int ballPointValue = snowBallStatusManager.SnowBallPointValue;
		int goalID = snowBallStatusManager.LastContactedGoalID;
		AddScorePoints(ballPointValue, goalID);
	}

	public void SubscribeEvent()
	{
		Debug.Log("Called");
		foreach (IEventPublisher eventPublisher in eventSubject.EventPublishers)
		{
			Debug.Log(eventPublisher);
			SnowBallCollisionHandler snowBallCollisionHandler = new SnowBallCollisionHandler(gameObject,eventSubject);
			
			if (typeof(IEventPublisher).IsAssignableFrom(snowBallCollisionHandler.GetType()))
			{
				Debug.Log("Called");
				 snowBallCollisionHandler = (SnowBallCollisionHandler)eventPublisher;
				snowBallCollisionHandler.CollidedWithGoal += OnCollisionWithGoal;			
			}
		}
	}
}
