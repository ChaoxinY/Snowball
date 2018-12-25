using UnityEngine;
using System.Collections;

public class SetRigidbodyVelocityCommand : ICommand
{
    InputHandlerState stateAttachedTo;

    public SetRigidbodyVelocityCommand(InputHandlerState stateAttachedTo)
    {
        this.stateAttachedTo = stateAttachedTo;
    }

    public void Execute(GameObject actor)
    {
        Vector3 movementDirection = stateAttachedTo.LastMovementInput;
        float movementSpeed = actor.GetComponent<MovementDataHolder>().MovementSpeed;
        actor.GetComponent<Rigidbody>().velocity = movementDirection.normalized * movementSpeed;
    }
}
