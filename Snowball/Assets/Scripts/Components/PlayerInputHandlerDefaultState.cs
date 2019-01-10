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
    private InputScheme stringBlock;

    public PlayerInputHandlerDefaultState(System.Object objectAttachedTo, GameObject gameobjectAttachedTo, InputHandlerUpdater inputHandlerUpdaterAttachedTo)
    {
        this.objectAttachedTo = objectAttachedTo;
        this.gameobjectAttachedTo = gameobjectAttachedTo;
        this.inputHandlerUpdaterAttachedTo = inputHandlerUpdaterAttachedTo;
        inputStateFactory = new InputStateFactory();
        moveCommand = new SetRigidbodyVelocityCommand(this);
        rotateCommand = new RotateRigidbodyCommand(this);
        stringBlock = gameobjectAttachedTo.GetComponent<InputScheme>();
        stringBlock.ControllerOrder = 1.ToString();
    }

    public override void HandleInput()
    {
        UpdateLastMovementInput();
        string userInput = EditorToolMethod.ReturnInputString();
        foreach (InputButton button in stringBlock.controllerButtons) {
            if (button.buttonName == userInput) {
                switch (button.buttonStringValue) {
                    case InputButton.ButtonStringValues.ButtonA:                    
                        break;
                }
            }
        }
    }
    //When changing to other inputhandler states
    //Syntax is fine needs to confirm actual functionality
    //with an another concrete inputhandler state
    //    Debug.Log("Running");
    //    inputHandlerUpdaterAttachedTo.CurrentInputHandler = (InputHandlerState)inputStateFactory.CreateProduct(
    //FactoriesProducts.InputstateProducts.PlayerInputHandlerDefaultState.ToString(), objectAttachedTo, gameobjectAttachedTo);

    private void UpdateLastMovementInput()
    {
        lastMovementInput = new Vector3(Input.GetAxis(stringBlock.ControllerOrder + InputButton.ButtonStringValues.JoystickLeftHorizontal.ToString()), 0, Input.GetAxis(stringBlock.ControllerOrder
            + InputButton.ButtonStringValues.JoystickLeftVertical.ToString()));
        fixedUpdateCommands.Add(moveCommand);
        fixedUpdateCommands.Add(rotateCommand);
    } 
}
