using UnityEngine;
using System.Collections.Generic;

public class SnowBall : MonoBehaviour, ISnowBallStatusHolder
{
	#region Variables
	[SerializeField]
	private ISubject eventSubject;
	private SnowBallCollisionHandler snowBallCollisionHandler;
	private List<object> components = new List<object>();

	public SnowBallStatusHolder SnowBallStatusHolder { get ; set ; }
	#endregion Variables

	#region Initialization
	private void Start()
	{
		snowBallCollisionHandler = new SnowBallCollisionHandler(gameObject, eventSubject);
		SnowBallStatusHolder.Snowball = gameObject;
		components.Add(snowBallCollisionHandler);
	}
	#endregion

	#region Functionality

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

	#endregion
}
