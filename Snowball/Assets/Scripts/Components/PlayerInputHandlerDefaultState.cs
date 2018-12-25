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

    public PlayerInputHandlerDefaultState(System.Object objectAttachedTo, GameObject gameobjectAttachedTo, InputHandlerUpdater inputHandlerUpdaterAttachedTo)
    {
        this.objectAttachedTo = objectAttachedTo;
        this.gameobjectAttachedTo = gameobjectAttachedTo;
        this.inputHandlerUpdaterAttachedTo = inputHandlerUpdaterAttachedTo;
        inputStateFactory = new InputStateFactory();
        moveCommand = new SetRigidbodyVelocityCommand(this);
        rotateCommand = new RotateRigidbodyCommand(this);
    }

    public override void HandleInput()
    {
        lastMovementInput = new Vector3(Input.GetAxis("J1 JoystickLeftHorizontal"), 0, Input.GetAxis("J1 JoystickLeftVertical"));
        if (lastMovementInput != Vector3.zero)
        {
            fixedUpdateCommands.Add(moveCommand);
            fixedUpdateCommands.Add(rotateCommand);
            //When changing to other inputhandler states
            //Syntax is fine needs to confirm actual functionality
            //with an another concrete inputhandler state
        //    Debug.Log("Running");
        //    inputHandlerUpdaterAttachedTo.CurrentInputHandler = (InputHandlerState)inputStateFactory.CreateProduct(
        //FactoriesProducts.InputstateProducts.PlayerInputHandlerDefaultState.ToString(), objectAttachedTo, gameobjectAttachedTo);

        }
    }
}
