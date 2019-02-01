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
    public ControllerType controller = ControllerType.None;
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
    private string lastInputString;

    //Pass the adapter instead the object class to seperate functionality
    public InputSchemeAssigner(InitializePanelAdapter initializePanelAdapter,List<ControllerInformation> connectedControllers)
    {
        // currentConnectedControllers = connectedControllers;
        this.initializePanelAdapter = initializePanelAdapter;
        this.connectedControllers = connectedControllers;
    }

    public void UpdateComponent()
    {
        if (InputToolMethod.ReturnInputString() == null)
        {
            lastInputString = null;
        }
        if (InputToolMethod.ReturnInputString() != null && InputToolMethod.ReturnInputString() != lastInputString)
        {
            string inputString = InputToolMethod.ReturnInputString();
            Debug.Log(inputString);
            lastInputString = inputString;
            bool keyBoardInput = inputString.Contains("Submit");
            bool controllerInput = inputString.Contains(InputButton.ButtonStringValues.ButtonA.ToString());
            if (keyBoardInput)
            {               
                AddKeyboardControllerInformation();
            }
            else if (controllerInput)
            {
                Debug.Log("Called");
                AddControllerInformation(inputString);
            }
        }
    }

    //Code duplication
    private void AddControllerInformation(string inputString)
    {
        string controllerOrder = inputString.Substring(0, 3);
        //check controllers thats already been added to see if this is a duplicate.
        for (int i = 0; i < connectedControllers.Count; i++)
        {
            //Controllers should be unique and not added through one controller
            //and controller order isnt any of the previous
            bool controlleOrderRepeated = false;
            for (int j = 0; j < connectedControllers.Count; j++) {
                if (controllerOrder == connectedControllers[j].controllerOrder) {
                    controlleOrderRepeated = true;
                }
            }
            Debug.Log(controlleOrderRepeated + controllerOrder);
            if (connectedControllers[i].controller == ControllerInformation.ControllerType.None&& !controlleOrderRepeated)
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
            if (connectedControllers[i].controller == ControllerInformation.ControllerType.None&& !controlleOrderRepeated)
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

    public InputSchemeRevoker(List<ControllerInformation> connectedControllers)
    {
        this.connectedControllers = connectedControllers;
    }

    public void UpdateComponent()
    {
        if (InputToolMethod.ReturnInputString() != null)
        {
            string inputString = InputToolMethod.ReturnInputString();
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
