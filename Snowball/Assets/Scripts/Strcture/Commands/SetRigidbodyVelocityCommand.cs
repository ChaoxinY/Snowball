using UnityEngine;

public class SetRigidbodyVelocityCommand : ICommand
{
    InputHandlerState stateAttachedTo;

    public SetRigidbodyVelocityCommand(InputHandlerState stateAttachedTo)
    {
        this.stateAttachedTo = stateAttachedTo;
    }

    public void Execute(GameObject actor)
    {
        MovementDataHolder movementDataHolder = actor.GetComponent<IMovementDataHolder>().GetMovementDataHolder();
        Vector3 movementDirection = movementDataHolder.LastMovementInput;
        float movementSpeed = movementDataHolder.MovementSpeed;
        actor.GetComponent<Rigidbody>().velocity = movementDirection.normalized * movementSpeed;
    }
}
