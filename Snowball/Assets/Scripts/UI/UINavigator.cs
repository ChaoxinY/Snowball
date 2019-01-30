using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UINavigator : MonoBehaviour
{
    private List<IUpdater> updaters = new List<IUpdater>();
    private IFactory inputStateFactory;
    private InputHandlerUpdater inputhandlerUpdater;
    public IFactory InputStateFactory { get { return inputStateFactory; } }

    public InputHandlerUpdater InputHandlerUpdater
    {
        get
        {
            return inputhandlerUpdater;
        }

        set
        {
            inputhandlerUpdater = value;
        }
    }

    void Start()
    {
        inputStateFactory = new InputStateFactory();
        InputHandlerUpdater = new InputHandlerUpdater(gameObject);
        InputHandlerUpdater.CurrentInputHandler = (InputHandlerState)InputStateFactory.CreateProduct(
            (int)FactoriesProducts.InputstateProducts.UINavigatorDefaultState, this, gameObject);
        updaters.Add(InputHandlerUpdater);
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
    private UIPageHolder uIPageHolder;

    public EventsystemCurrentGameObjectRefresher(EventSystem eventSystem)
    {
        this.eventSystem = eventSystem;
        uIPageHolder = GameObject.Find("UICanvas").GetComponent<UIPageHolder>();
        GameObject selectableUIElement = SearchForActiveUIElement(uIPageHolder.initializedUIPages);
        lastSelectedGameObject.Push(selectableUIElement);
    }

    public void UpdateComponent()
    {
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
            GameObject newCurrentGameObject = SearchForActiveUIElement(uIPageHolder.initializedUIPages);
            lastSelectedGameObject.Push(newCurrentGameObject);
        }
        //If current isnt the same as the first in stack and currently selected object isnt null.
        //Add this new reference
        if (eventSystem.currentSelectedGameObject != lastSelectedGameObject.Peek() && eventSystem.currentSelectedGameObject != null)
        {
            lastSelectedGameObject.Push(eventSystem.currentSelectedGameObject);
        }
    }

    private GameObject SearchForActiveUIElement(List<UIPage> uIPages)
    {
        GameObject selectableUIElement = null;
        foreach (UIPage uIPage in uIPages)
        {
            if (uIPage.isActive && uIPage.initializedUIElements.Count != 0)
            {
                selectableUIElement = uIPage.initializedUIElements[0];
            }
        }
        return selectableUIElement;
    }
}