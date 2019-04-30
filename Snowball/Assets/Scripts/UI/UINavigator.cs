using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UINavigator : MonoBehaviour
{
	private List<IUpdater> updaters = new List<IUpdater>();
	private EventsystemCurrentGameObjectRefresher objectRefresher;

	public IFactory InputStateFactory { get; private set; }
	public InputHandlerUpdater InputHandlerUpdater { get; private set; }

	void Start()
	{
		InputStateFactory = new InputStateFactory();
		InputHandlerUpdater = new InputHandlerUpdater(gameObject);
		InputHandlerUpdater.CurrentInputHandler = (InputHandlerState)InputStateFactory.CreateProduct(
			(int)FactoriesProducts.InputstateProducts.UINavigatorDefaultState, this, gameObject);
		objectRefresher = new EventsystemCurrentGameObjectRefresher(EventSystem.current);
		updaters.AddRange(new List<IUpdater>() { InputHandlerUpdater, objectRefresher });
	}

	private void Update()
	{
		SystemToolMethods.UpdateIUpdaters(updaters);
	}
}

public class EventsystemCurrentGameObjectRefresher : IUpdater
{
	private EventSystem eventSystem;
	private Stack<GameObject> lastSelectedGameObject = new Stack<GameObject>();
	private UIPanelHolder uIPanelHolder;

	public EventsystemCurrentGameObjectRefresher(EventSystem eventSystem)
	{
		this.eventSystem = eventSystem;
		uIPanelHolder = GameObject.Find("MainCanvas").GetComponent<UIPanelHolder>();
		GameObject selectableUIElement = SearchForActiveUIElement(uIPanelHolder.initializedUIPanels);
		lastSelectedGameObject.Push(selectableUIElement);
	}

	public void UpdateComponent()
	{
		Debug.Log(lastSelectedGameObject.Peek());
		if (eventSystem.currentSelectedGameObject == null)
		{
			//Incase reference is destroyed or missing 
			if (lastSelectedGameObject.Peek() == null)
			{
				//get rid of the null one
				lastSelectedGameObject.Pop();
			}
			eventSystem.SetSelectedGameObject(lastSelectedGameObject.Peek());
		}
		//this or == false
		else if (!eventSystem.currentSelectedGameObject.activeInHierarchy)
		{
			GameObject newCurrentGameObject = SearchForActiveUIElement(uIPanelHolder.initializedUIPanels);
			lastSelectedGameObject.Push(newCurrentGameObject);
			eventSystem.SetSelectedGameObject(lastSelectedGameObject.Peek());
		}
		//If current isnt the same as the first in stack and currently selected object isnt null.
		//Add this new reference
		if (eventSystem.currentSelectedGameObject != lastSelectedGameObject.Peek() && eventSystem.currentSelectedGameObject != null)
		{
			lastSelectedGameObject.Push(eventSystem.currentSelectedGameObject);
		}
	}

	private GameObject SearchForActiveUIElement(List<UIPanel> uIPanels)
	{
		GameObject selectableUIElement = null;
		foreach (UIPanel uIPanel in uIPanels)
		{
			if (uIPanel.gameObject.activeInHierarchy && uIPanel.SelectableUIElements.Count != 0)
			{
				foreach (Selectable selectablesUIElement in uIPanel.SelectableUIElements)
				{
					GameObject selectableGameObject = selectablesUIElement.gameObject;
					if (selectableGameObject.activeInHierarchy)
					{
						selectableUIElement = selectableGameObject;
					}
				}
			}
		}
		return selectableUIElement;
	}
}