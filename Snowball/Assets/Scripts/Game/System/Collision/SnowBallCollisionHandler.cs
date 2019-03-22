using UnityEngine;

//Component
//EventSender

public class SnowBallCollisionHandler : ICollideAble,IEventPublisher
{
	private GameObject gameObjectAttachedTo;
	private EventSubject subject;

	public event System.EventHandler<GameObjectEventArgs> CollidedWithGoal;

	public SnowBallCollisionHandler(GameObject gameObjectAttachedTo, EventSubject subjectToSubscribe)
	{
		this.gameObjectAttachedTo = gameObjectAttachedTo;
		subject = subjectToSubscribe;
		subject.SubscribeEventPublisher(this);
	}

	public void ReactToCollision(Collision collision)
	{
		switch (collision.gameObject.tag)
		{
			case "Goal":
				gameObjectAttachedTo.GetComponent<ISnowBallStatusHolder>().GetSnowBallStatusHolder().LastContactedGoalID = collision.gameObject.GetComponent<GoalStatusHolder>().GoalID;
				OnCollisionWithGoal(collision.gameObject);
				break;
		}
	}
	protected virtual void OnCollisionWithGoal(GameObject gameObject)
	{
		if (CollidedWithGoal == null)
		{
			Debug.Log("Null");
		}
		CollidedWithGoal(this, new GameObjectEventArgs(gameObjectAttachedTo));
	}
}
