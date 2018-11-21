using UnityEngine;
using System.Collections;

//Keeping functionality and stored data seperated
//state
public class PlayerInputHandlerDefaultState : InputHandlerState
{
    private ICommand moveCommand;
    private GameObject gameobjectAttachedTo;
    private InputhandlerUpdater inputHandlerUpdaterAttachedTo; 

    public PlayerInputHandlerDefaultState(GameObject gameobjectAttachedTo, InputhandlerUpdater inputHandlerUpdaterAttachedTo) {       
        this.gameobjectAttachedTo = gameobjectAttachedTo;
        this.inputHandlerUpdaterAttachedTo = inputHandlerUpdaterAttachedTo;
        moveCommand = new AddForceToRigidbodyCommand(this);
    }

    public override void HandleInput()
    {
        lastMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (lastMovementInput != Vector3.zero)
        {
            moveCommand.Execute(gameobjectAttachedTo);
            //When changing to other inputhandler states
            //Syntax is fine needs to confirm actual functionality
            //with an another concrete inputhandler state
            //inputHandlerUpdaterAttachedTo.currentInputHandler = new PlayerInputHandlerDefaultState(gameobjectAttachedTo, inputHandlerUpdaterAttachedTo);
        }
    }
}
