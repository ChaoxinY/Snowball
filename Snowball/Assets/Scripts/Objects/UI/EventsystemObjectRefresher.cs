using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventsystemObjectRefresher : MonoBehaviour
{
	private List<IUpdater> updaters = new List<IUpdater>();
	private EventsystemCurrentGameObjectRefresher objectRefresher;

	void Start()
	{
		objectRefresher = new EventsystemCurrentGameObjectRefresher(EventSystem.current);
		updaters.Add(objectRefresher);
	}

	private void Update()
	{
		SystemToolMethods.UpdateIUpdaters(updaters);
	}
}