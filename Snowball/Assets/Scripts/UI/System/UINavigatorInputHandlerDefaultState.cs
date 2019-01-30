using UnityEngine;
using System.Collections;

public class UINavigatorInputHandlerDefaultState : InputHandlerState
{
    private UIPageHolder uIPageHolder;

    public UINavigatorInputHandlerDefaultState(InputHandlerUpdater inputHandlerUpdater)
    {
        uIPageHolder = GameObject.Find("UICanvas").GetComponent<UIPageHolder>();
        InputHandlerUpdater = inputHandlerUpdater;
    }

    public override void HandleInput()
    {
        //If current ui element is no longer being edited
        //Check for focused ui element which is is an interface bool that becomes true when on edit event is received
        //and false when oncancel event is recevied.
        //The page stores all focusable ui elements and the currently focused element.

        if (Input.GetAxis("Cancel") != 0)
        {
            //Close current panel
            bool returnToStartPage = true;
            foreach (UIPage uiPage in uIPageHolder.initializedUIPages)
            {
                if (uiPage.isActive)
                {
                    foreach (bool focused in uiPage.FocusedUIElements)
                    {
                        if (focused)
                        {
                            returnToStartPage = false;
                            break;
                        }
                    }
                }
            }
            if (returnToStartPage)
            {
                foreach (UIPage uiPage in uIPageHolder.initializedUIPages)
                {
                    uiPage.gameObject.SetActive(false);
                }
                Debug.Log("Called");
                uIPageHolder.startPage.gameObject.SetActive(true);
            }
        }
        // Change to In game state
        //Condition
    //    inputHandlerUpdaterAttachedTo.CurrentInputHandler = (InputHandlerState)inputStateFactory.CreateProduct(
    //(int)FactoriesProducts.InputstateProducts.UINavigatorInGameState,this );
    }
}

public class UINavigatorInputHandlerInGameState : InputHandlerState
{
    private UIPageHolder uIPageHolder;

    public UINavigatorInputHandlerInGameState(InputHandlerUpdater inputHandlerUpdater)
    {
        uIPageHolder = GameObject.Find("UICanvas").GetComponent<UIPageHolder>();
        InputHandlerUpdater = inputHandlerUpdater;
    }

    public override void HandleInput()
    {
        if (Input.GetAxis("Pause") != 0)
        {
            //Open menu panel
            //Change to Default state
        }
    }
}