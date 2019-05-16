using UnityEngine;

//Component
//EventSender

public class SnowBallCollisionHandler : ICollideAble, IEventPublisher
{
	private GameObject gameObjectAttachedTo;
	private ISubject subject;

	public event System.EventHandler<GameObjectEventArgs> CollidedWithGoal;

	public SnowBallCollisionHandler(GameObject gameObjectAttachedTo, ISubject subjectToSubscribe)
	{
		this.gameObjectAttachedTo = gameObjectAttachedTo;
		subject = subjectToSubscribe;
		subject.Subscribe(this);
	}

	public void ReactToCollision(Collision collision)
	{
		switch (collision.gameObject.tag)
		{
			case "Goal":
				gameObjectAttachedTo.GetComponent<ISnowBallStatusHolder>().SnowBallStatusHolder.LastContactedGoalID = collision.gameObject.GetComponent<GoalStatusHolder>().GoalID;
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

	public void UnSubscribeFromSubject()
	{
		subject.UnSubscribe(this);
	}
}
