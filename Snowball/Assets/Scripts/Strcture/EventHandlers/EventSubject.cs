using UnityEngine;
using System.Collections.Generic;

public class EventSubject : MonoBehaviour, ISubject
{
	private List<object> eventPublishers = new List<object>();
	public List<object> EventPublishers { get { return eventPublishers; } set { eventPublishers = value; } }

	public void Subscribe<T>(T item)
	{
		if (item is object eventPublisher)
		{
			EventPublishers.Add(eventPublisher);
		}
	}
	public void UnSubscribe<T>(T item)
	{
		if (item is object eventPublisher)
		{
			EventPublishers.Remove(eventPublisher);
		}
	}
}
