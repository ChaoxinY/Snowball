﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UINavigator : MonoBehaviour
{
	public FactoriesProducts.InputstateProducts startInputState;

	private List<IUpdater> updaters = new List<IUpdater>();
	private EventsystemCurrentGameObjectRefresher objectRefresher;

	public IFactory InputStateFactory { get; private set; }
	public InputHandlerUpdater InputHandlerUpdater { get; private set; }

	void Start()
	{
		InputStateFactory = new InputStateFactory();
		InputHandlerUpdater = new InputHandlerUpdater(gameObject);
		InputHandlerUpdater.CurrentInputHandler = (InputHandlerState)InputStateFactory.CreateProduct(
		(int)startInputState, this, gameObject);

		Debug.Log(InputHandlerUpdater.CurrentInputHandler.InputHandlerUpdater);
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
	private Stack<GameObject> lastSelectedGameObjects = new Stack<GameObject>();
	private UIPanelHolder uIPanelHolder;

	public EventsystemCurrentGameObjectRefresher(EventSystem eventSystem)
	{
		this.eventSystem = eventSystem;
		uIPanelHolder = GameObject.Find("MainCanvas").GetComponent<UIPanelHolder>();
		AddselectableUIElement(lastSelectedGameObjects);
	}

	public void UpdateComponent()
	{
		if (eventSystem.currentSelectedGameObject == null)
		{
			//The stack is empty try to find an activeElement
			if (lastSelectedGameObjects.Count == 0)
			{
				AddselectableUIElement(lastSelectedGameObjects);
				return;
			}
			//Incase reference is destroyed or missing 
			if (lastSelectedGameObjects.Peek() == null)
			{
				//get rid of the null one
				lastSelectedGameObjects.Pop();
			}
			eventSystem.SetSelectedGameObject(lastSelectedGameObjects.Peek());
		}
		//this or == false
		else if (!eventSystem.currentSelectedGameObject.activeInHierarchy)
		{
			GameObject newCurrentGameObject = SearchForActiveUIElement(uIPanelHolder.initializedUIPanels);
			lastSelectedGameObjects.Push(newCurrentGameObject);
			eventSystem.SetSelectedGameObject(lastSelectedGameObjects.Peek());
		}
		//If current isnt the same as the first in stack and currently selected object isnt null.
		//Add this new reference
		if (eventSystem.currentSelectedGameObject != lastSelectedGameObjects.Peek() && eventSystem.currentSelectedGameObject != null)
		{
			lastSelectedGameObjects.Push(eventSystem.currentSelectedGameObject);
		}
	}

	private void AddselectableUIElement(Stack<GameObject> stackToAdd)
	{
		GameObject selectableUIElement = SearchForActiveUIElement(uIPanelHolder.initializedUIPanels);
		if (selectableUIElement!= null)
		{
			stackToAdd.Push(selectableUIElement);
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