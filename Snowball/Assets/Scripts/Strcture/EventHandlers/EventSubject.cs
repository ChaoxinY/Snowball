using UnityEngine;
using System.Collections.Generic;

public class EventSubject : MonoBehaviour
{
	private List<object> eventPublishers = new List<object>();

	public List<object> EventPublishers
	{
		get
		{
			return eventPublishers;
		}

		set
		{
			eventPublishers = value;
		}
	}

	public void SubscribeEventPublisher(System.Object eventPublisher)
	{
		EventPublishers.Add(eventPublisher);
		Debug.Log(eventPublisher);
	}

	public void RemoveEventPublisher(System.Object eventPublisher)
	{
		EventPublishers.Remove(eventPublisher);
	}
}
