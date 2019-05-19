using UnityEngine;
using System;

public class UINavigatorInputHandlerDefaultState : InputHandlerState
{
    private UIPanelHolder uiPanelHolder;

    public UINavigatorInputHandlerDefaultState(InputHandlerUpdater inputHandlerUpdater, object objectAttached)
    {
        uiPanelHolder = GameObject.Find("MainCanvas").GetComponent<UIPanelHolder>();
        InputHandlerUpdater = inputHandlerUpdater;
		ObjectAttached = objectAttached;
	}

    public override void HandleInput()
    {
		//If current ui element is no longer being edited
		//Check for focused ui element which is is an interface bool that becomes true when on edit event is received
		//and false when oncancel event is recevied.
		//The page stores all focusable ui elements and the currently focused element.
		if (!InputToolMethod.IsInputRepeated() && Input.GetAxis("Cancel") != 0)
		{
			bool noFocusedUI = true;
            foreach (UIPanel uIPanel in uiPanelHolder.initializedUIPanels)
            {
                if (uIPanel.gameObject.activeInHierarchy)
                {
                    foreach (IFocusUIElement focusElement in uIPanel.FocusUIElements)
                    {
                        bool focused = focusElement.IsThisElementInFocus();
                        if (focused)
                        {
                            noFocusedUI = false;
                            break;
                        }
                    }
                }
            }
            if (noFocusedUI)
            {
				//if there are no elements being focused close all panels and return to start
				foreach (UIPanel uIPanel in uiPanelHolder.initializedUIPanels)
				{
					if (uIPanel.GetComponent<IFocusUIElement>() == null)
					{
						uIPanel.gameObject.SetActive(false);
					}
				}
				InputHandlerUpdater.CurrentInputHandler = (InputHandlerState)inputStateFactory.CreateProduct(
				(int)FactoriesProducts.InputstateProducts.UINavigatorInGameState, ObjectAttached);
				Debug.Log(InputHandlerUpdater.CurrentInputHandler);
			}
        }
    }
}

public class UINavigatorInputHandlerInGameState : InputHandlerState
{
    private UIPanelHolder uiPanelHolder;
	private Transform mainCanvasTransform;

    public UINavigatorInputHandlerInGameState(InputHandlerUpdater inputHandlerUpdater, object objectAttached)
    {
		GameObject mainCanvas = GameObject.Find("MainCanvas");
		uiPanelHolder = mainCanvas.GetComponent<UIPanelHolder>();
		mainCanvasTransform = mainCanvas.transform;
		InputHandlerUpdater = inputHandlerUpdater;
		ObjectAttached = objectAttached;
	}

    public override void HandleInput()
    {
        if (!InputToolMethod.IsInputRepeated() && Input.GetAxis("Pause") != 0)
        {
			UIToolMethods.OpenUIPanel(mainCanvasTransform, "PauseMenu");
			InputHandlerUpdater.CurrentInputHandler = (InputHandlerState)inputStateFactory.CreateProduct(
			(int)FactoriesProducts.InputstateProducts.UINavigatorDefaultState, ObjectAttached);
			Debug.Log(InputHandlerUpdater.CurrentInputHandler);
		}
	}
}