using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InputButton
{
    //User input is this button
    public string buttonName;
    //What string is this button actually going to broadcast
    public ButtonStringValues buttonStringValue;

    public enum ButtonStringValues
    {
        ButtonA,
        ButtonB,
    }
}

public class ControllerInformation
{
    public string controllerOrder = null;
    public ControllerType controller = ControllerType.None;
    public enum ControllerType
    {
        None,
        Controller,
        Keyboard
    }
    public void ResetControllerInformation()
    {
        controllerOrder = null;
        controller = ControllerType.None;
    }
}

//Switching between multiple tabs can be nauseating hence putting correlated classes together.
[CreateAssetMenu(fileName = "NewInputBlock", menuName = "Dataholders/InputBlock")]
public class InputScheme : ScriptableObject
{
    private ControllerInformation controllerInformation;

    [System.NonSerialized]
    public List<InputButton> controllerButtons = new List<InputButton>() { };
    public InputButton buttonA;
    //joystickLeftHorizontal, joystickLeftVertical,
    //So you can rearrange controller order
    public string ControllerOrder
    {
        get
        {
            return controllerInformation.controllerOrder;
        }

        set
        {
            controllerInformation.controllerOrder = value;
            foreach (InputButton button in controllerButtons)
            {
                button.buttonName = controllerInformation.controllerOrder + button.buttonStringValue.ToString();
            }
        }
    }

    public ControllerInformation.ControllerType ControllerType
    {

        get { return controllerInformation.controller; }
    }


    private void OnEnable()
    {
        controllerButtons.AddRange(new List<InputButton>() { buttonA });
    }
}

//Detect controller connection
public class InputSchemeAssigner : IUpdater
{
    private List<ControllerInformation> connectedControllers = new List<ControllerInformation>();
    private InitializePanelAdapter initializePanelAdapter;

    //Pass the adapter instead the object class to seperate functionality
    public InputSchemeAssigner(InitializePanelAdapter initializePanelAdapter, List<ControllerInformation> connectedControllers)
    {
        // currentConnectedControllers = connectedControllers;
        this.initializePanelAdapter = initializePanelAdapter;
        this.connectedControllers = connectedControllers;
    }

    public void UpdateComponent()
    {
        if (InputToolMethod.ReturnInputString() != null)
        {
            string inputString = InputToolMethod.ReturnInputString();
            bool keyBoardInput = inputString.Contains("Submit");
            bool controllerInput = inputString.Contains(InputButton.ButtonStringValues.ButtonA.ToString());
            if (keyBoardInput)
            {
                AddKeyboardControllerInformation();
            }
            else if (controllerInput)
            {
                AddControllerInformation(inputString);
            }
        }
    }

    //Code duplication
    private void AddControllerInformation(string inputString)
    {
        string controllerOrder = InputToolMethod.ReturnJoyStickOrder(inputString);
        //check controllers thats already been added to see if this is a duplicate.
        for (int i = 0; i < connectedControllers.Count; i++)
        {
            //Controllers should be unique and not added through one controller
            //and controller order isnt any of the previous
            bool controlleOrderRepeated = false;
            for (int j = 0; j < connectedControllers.Count; j++)
            {
                if (controllerOrder == connectedControllers[j].controllerOrder)
                {
                    controlleOrderRepeated = true;
                }
            }
            if (connectedControllers[i].controller == ControllerInformation.ControllerType.None && !controlleOrderRepeated)
            {
                connectedControllers[i] = new ControllerInformation();
                connectedControllers[i].controllerOrder = controllerOrder;
                connectedControllers[i].controller = ControllerInformation.ControllerType.Controller;
                initializePanelAdapter.RefreshPanel();
                break;
            }
        }
    }

    private void AddKeyboardControllerInformation()
    {
        for (int i = 0; i < connectedControllers.Count; i++)
        {
            bool controlleOrderRepeated = false;
            for (int j = 0; j < connectedControllers.Count; j++)
            {
                if (connectedControllers[j].controller == ControllerInformation.ControllerType.Keyboard)
                {
                    controlleOrderRepeated = true;
                }
            }
            if (connectedControllers[i].controller == ControllerInformation.ControllerType.None && !controlleOrderRepeated)
            {
                connectedControllers[i] = new ControllerInformation();
                connectedControllers[i].controller = ControllerInformation.ControllerType.Keyboard;
                initializePanelAdapter.RefreshPanel();
                break;
            }
        }
    }
}

public class InputSchemeRevoker : IUpdater
{
    private List<ControllerInformation> connectedControllers = new List<ControllerInformation>();
    private InitializePanelAdapter initializePanelAdapter;

    public InputSchemeRevoker(InitializePanelAdapter initializePanelAdapter, List<ControllerInformation> connectedControllers)
    {
        this.initializePanelAdapter = initializePanelAdapter;
        this.connectedControllers = connectedControllers;
    }

    public void UpdateComponent()
    {
        if (InputToolMethod.ReturnInputString() != null)
        {
            string inputString = InputToolMethod.ReturnInputString();
            bool KeyBoardControllerCanceled = inputString.Contains("Cancel");
            bool ControllerCanceled = inputString.Contains(InputButton.ButtonStringValues.ButtonB.ToString());

            if (ControllerCanceled)
            {
                RemoveControllerInformation(inputString);
                //controllerInformationToRemove = CheckIfThisControllerInformationExsist(inputString);
            }
            else if (KeyBoardControllerCanceled)
            {
                RemoveKeyboardControllerInformation();
            }
        }
    }

    private void RemoveControllerInformation(string inputString)
    {
        string controllerOrder = InputToolMethod.ReturnJoyStickOrder(inputString);
        for (int i = 0; i < connectedControllers.Count; i++)
        {
            if (connectedControllers[i].controllerOrder == controllerOrder && connectedControllers[i].controller == ControllerInformation.ControllerType.Controller)
            {
                connectedControllers[i].ResetControllerInformation();
                initializePanelAdapter.RefreshPanel();
                break;
            }
        }
    }

    private void RemoveKeyboardControllerInformation()
    {
        for (int i = 0; i < connectedControllers.Count; i++)
        {
            if (connectedControllers[i].controller == ControllerInformation.ControllerType.Keyboard)
            {
                connectedControllers[i].ResetControllerInformation();
                initializePanelAdapter.RefreshPanel();
                break;
            }
        }
    }
}

