using UnityEngine;
using System.Collections.Generic;

public class SnowBall : MonoBehaviour, ISnowBallStatusHolder
{
	[SerializeField]
	private ISubject eventSubject;
	private SnowBallCollisionHandler snowBallCollisionHandler;
	public SnowBallStatusHolder snowBallStatusHolder;
	private List<object> components = new List<object>();

	private void Start()
	{
		snowBallCollisionHandler = new SnowBallCollisionHandler(gameObject, eventSubject);
		snowBallStatusHolder.Snowball = gameObject;
		components.Add(snowBallCollisionHandler);
	}

	//OnDestroy component handle, prevent nullreference error in lists.
	private void OnDestroy()
	{
		foreach (object component in components)
		{
			if (component is IEventPublisher eventPublisher)
			{
				eventPublisher.UnSubscribeFromSubject();
			}
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		snowBallCollisionHandler.ReactToCollision(collision);
	}

	#region Interface variables
	public SnowBallStatusHolder GetSnowBallStatusHolder()
	{
		return snowBallStatusHolder;
	}

	public void SetSnowBallStatusHolder(SnowBallStatusHolder snowBallStatusHolder)
	{
		this.snowBallStatusHolder = snowBallStatusHolder;
	}
	#endregion
}
