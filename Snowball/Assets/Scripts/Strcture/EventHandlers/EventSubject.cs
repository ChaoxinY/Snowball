using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class EventSubject : MonoBehaviour
{
	private List<System.Object> eventPublishers = new List<System.Object>();

	public List<System.Object> EventPublishers
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
