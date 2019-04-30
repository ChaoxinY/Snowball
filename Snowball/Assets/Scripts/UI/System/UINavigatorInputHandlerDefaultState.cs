using UnityEngine;

public class UINavigatorInputHandlerDefaultState : InputHandlerState
{
    private UIPanelHolder uiPanelHolder;

    public UINavigatorInputHandlerDefaultState(InputHandlerUpdater inputHandlerUpdater)
    {
        uiPanelHolder = GameObject.Find("MainCanvas").GetComponent<UIPanelHolder>();
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
            foreach (UIPanel uIPanel in uiPanelHolder.initializedUIPanels)
            {
                if (uIPanel.gameObject.activeInHierarchy)
                {
                    foreach (IFocusUIElement focusElement in uIPanel.FocusUIElements)
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
                foreach (UIPanel uIPanel in uiPanelHolder.initializedUIPanels)
                {
                    if (uIPanel.GetComponent<IFocusUIElement>() == null)
                    {
						uIPanel.gameObject.SetActive(false);
                    }
                }
                UIToolMethods.OpenUIPanel(uiPanelHolder.canvasTransform, uiPanelHolder.StartPanel.gameObject.name);
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
    private UIPanelHolder uIPageHolder;

    public UINavigatorInputHandlerInGameState(InputHandlerUpdater inputHandlerUpdater)
    {
        uIPageHolder = GameObject.Find("UICanvas").GetComponent<UIPanelHolder>();
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