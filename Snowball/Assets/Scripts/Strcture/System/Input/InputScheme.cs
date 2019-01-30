using System.Collections;
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
    public string controllerOrder;
    public ControllerType controller;
    public enum ControllerType
    {
        None,
        Controller,
        Keyboard
    }
}

//Switching between multiple tabs can be nauseating hence putting correlated classes together.
[CreateAssetMenu(fileName = "NewInputBlock", menuName = "Dataholders/InputBlock")]
public class InputScheme : ScriptableObject
{
    private ControllerInformation controllerInformation;

    [System.NonSerialized]
    public List<InputButton> controllerButtons = new List<InputButton>() { };
    public InputButton  buttonA;
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
            controllerInformation.controllerOrder =  value;
            foreach (InputButton button in controllerButtons)
            {
                button.buttonName = controllerInformation.controllerOrder + button.buttonStringValue.ToString();
            }
        }
    }

    public ControllerInformation.ControllerType ControllerType {

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

    public InputSchemeAssigner(List<ControllerInformation> connectedControllers) {
        this.connectedControllers = connectedControllers;
    }

    public void UpdateComponent()
    {
        string inputString = EditorToolMethod.ReturnInputString();
        bool KeyBoardInput = Input.GetKey(KeyCode.KeypadEnter);
        bool ControllerInput = inputString.Contains(InputButton.ButtonStringValues.ButtonA.ToString());
        ControllerInformation newControllerInformation = null;
        if (KeyBoardInput)
        {
            newControllerInformation = CheckIfKeyboardIsConnected();
        }
        else if (ControllerInput)
        {
            newControllerInformation = CheckIfControllerIsConnected(inputString);
        }
        if (newControllerInformation != null)
        {
            ControllerInformation emptyInformationSpotToAssign;
            foreach (ControllerInformation c in connectedControllers) {
                if (c == null) {
                    emptyInformationSpotToAssign = c;
                    break;
                }
            }
            emptyInformationSpotToAssign = newControllerInformation;
           // connectedControllers.Add(newControllerInformation);
            //Change the assign panel to character select panel
        }
    }

    //Code duplication
    private ControllerInformation CheckIfControllerIsConnected(string inputString)
    {
        ControllerInformation controllerInformation = null;
        string controllerOrder = inputString.Substring(0, 3);
        //check controllers thats already been added to see if this is a duplicate.
        foreach (ControllerInformation c in connectedControllers)
        {
            if (c.controllerOrder == controllerOrder&&  c.controller == ControllerInformation.ControllerType.Controller)
            {
                break;
            }
            else
            {
                controllerInformation = new ControllerInformation();
                controllerInformation.controllerOrder = controllerOrder;
                controllerInformation.controller = ControllerInformation.ControllerType.Controller;
            }
        }
        return controllerInformation;
    }

    private ControllerInformation CheckIfKeyboardIsConnected()
    {
        ControllerInformation controllerInformation = null;
        foreach (ControllerInformation c in connectedControllers)
        {
            if (c.controller == ControllerInformation.ControllerType.Keyboard)
            {
                break;
            }
            else
            {
                controllerInformation = new ControllerInformation();
                controllerInformation.controller = ControllerInformation.ControllerType.Keyboard;
            }
        }
        return controllerInformation;
    }
}

public class InputSchemeRevoker : IUpdater
{
    private List<ControllerInformation> connectedControllers = new List<ControllerInformation>();

    public InputSchemeRevoker(List<ControllerInformation> connectedControllers)
    {
        this.connectedControllers = connectedControllers;
    }

    public void UpdateComponent()
    {
        string inputString = EditorToolMethod.ReturnInputString();
        bool KeyBoardControllerCanceled = (Input.GetAxis("Cancel") != 0) ? true : false;
        bool ControllerCanceled = inputString.Contains(InputButton.ButtonStringValues.ButtonB.ToString());
        ControllerInformation controllerInformationToRemove = null;

        if (KeyBoardControllerCanceled || ControllerCanceled)
        {
            controllerInformationToRemove = CheckIfThisControllerInformationExsist(inputString);
        }

        if (controllerInformationToRemove != null)
        {
            //Expected result: The referenced information in the main class is supposed to null and 
            //not the local variable.
            controllerInformationToRemove.controller = ControllerInformation.ControllerType.None;
           // connectedControllers.Remove(controllerInformationToRemove);
        }
    }

    private ControllerInformation CheckIfThisControllerInformationExsist(string inputString)
    {
        ControllerInformation controllerInformation = null;
        string controllerOrder = inputString.Substring(0, 3);
        foreach (ControllerInformation c in connectedControllers)
        {
            if (c.controllerOrder == controllerOrder)
            {
                controllerInformation = c;
                break;
            }
            else if (c.controller == ControllerInformation.ControllerType.Keyboard)
            {
                controllerInformation = c;
                break;
            }
        }
        return controllerInformation;
    }
}
