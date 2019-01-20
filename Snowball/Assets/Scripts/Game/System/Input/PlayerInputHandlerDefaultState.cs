using UnityEngine;
using System.Collections;

//Keeping functionality and stored data seperated
//state
public class PlayerInputHandlerDefaultState : InputHandlerState
{
    private ICommand moveCommand, rotateCommand;
    private System.Object objectAttachedTo;
    private GameObject gameobjectAttachedTo;
    private InputHandlerUpdater inputHandlerUpdaterAttachedTo;
    private InputScheme inputScheme;

    public PlayerInputHandlerDefaultState(System.Object objectAttachedTo, GameObject gameobjectAttachedTo, InputHandlerUpdater inputHandlerUpdaterAttachedTo)
    {
        this.objectAttachedTo = objectAttachedTo;
        this.gameobjectAttachedTo = gameobjectAttachedTo;
        this.inputHandlerUpdaterAttachedTo = inputHandlerUpdaterAttachedTo;
        inputStateFactory = new InputStateFactory();
        moveCommand = new SetRigidbodyVelocityCommand(this);
        rotateCommand = new RotateRigidbodyCommand(this);
        inputScheme = gameobjectAttachedTo.GetComponent<IInputSchemeHolder>().GetInputScheme();
        //stringBlock.ControllerOrder = 1.ToString();
    }

    public override void HandleInput()
    {
        string userInput = EditorToolMethod.ReturnInputString();
        if (userInput != null && inputScheme.ControllerType == ControllerInformation.ControllerType.Keyboard)
        {
            HandleKeyBoardMouseInput(userInput);
        }
        else if (userInput != null && inputScheme.ControllerType == ControllerInformation.ControllerType.Controller)
        {
            HandleControllerInput(userInput);
        }
    }

    //When changing to other inputhandler states
    //Syntax is fine needs to confirm actual functionality
    //with an another concrete inputhandler state
    //    Debug.Log("Running");
    //    inputHandlerUpdaterAttachedTo.CurrentInputHandler = (InputHandlerState)inputStateFactory.CreateProduct(
    //FactoriesProducts.InputstateProducts.PlayerInputHandlerDefaultState.ToString(), objectAttachedTo, gameobjectAttachedTo);

    //how is this going to handle multiple button presses
    protected override void HandleControllerInput(string inputString)
    {
        UpdateLastMovementInput(inputScheme.ControllerOrder +"JoystickLeftHorizontal",inputScheme.ControllerOrder+"JoystickLeftVertical");
        foreach (InputButton button in inputScheme.controllerButtons)
        {
            if (button.buttonName == inputString)
            {
                switch (button.buttonStringValue)
                {
                    case InputButton.ButtonStringValues.ButtonA:
                        break;
                }
            }        
        }
    }
    protected override void HandleKeyBoardMouseInput(string inputString) {
        UpdateLastMovementInput("Horizontal","Vertical");
        switch (inputString)
        {
        }
    }

    private void UpdateLastMovementInput(string horizontalAxisString,string verticalAxisString)
    {
        lastMovementInput = new Vector3(Input.GetAxis(horizontalAxisString), 0, Input.GetAxis(verticalAxisString));
        fixedUpdateCommands.Add(moveCommand);
        fixedUpdateCommands.Add(rotateCommand);
    }
}
