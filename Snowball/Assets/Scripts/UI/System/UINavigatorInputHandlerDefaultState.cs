using UnityEngine;

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
        if (!InputToolMethod.IsInputRepeated() && Input.GetAxis("Cancel") != 0)
        {
            //Close current panel
            bool returnToStartPage = true;
            foreach (UIPage uiPage in uIPageHolder.initializedUIPages)
            {
                if (uiPage.isActive)
                {
                    foreach (IFocusUIElement focusElement in uiPage.FocusedUIElements)
                    {
                        bool focused = focusElement.IsThisElementInFocus();
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
                    if (uiPage.GetComponent<IFocusUIElement>() == null)
                    {
                        uiPage.gameObject.SetActive(false);
                    }
                }
                UIToolMethods.OpenUIPanel(uIPageHolder.startPage.canvasTransform, uIPageHolder.startPage.gameObject.name);
            }
        }
        // Change to In game state
        //Condition
        //    inputHandlerUpdaterAttachedTo.CurrentInputHandler = (InputHandle rState)inputStateFactory.CreateProduct(
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