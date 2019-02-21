using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventSubject : MonoBehaviour
{
	private List<IEventPublisher> eventPublishers = new List<IEventPublisher>();

	public List<IEventPublisher> EventPublishers
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

	public void SubscribeEventPublisher(IEventPublisher eventPublisher)
	{
		EventPublishers.Add(eventPublisher);
		Debug.Log(eventPublisher);
	}

	public void RemoveEventPublisher(IEventPublisher eventPublisher)
	{
		EventPublishers.Remove(eventPublisher);
	}
}
