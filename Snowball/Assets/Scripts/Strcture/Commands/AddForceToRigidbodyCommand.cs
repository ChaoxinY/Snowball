using UnityEngine;

public class AddForceToRigidbodyCommand : ICommand
{
    InputHandlerState stateAttachedTo;

    public AddForceToRigidbodyCommand(InputHandlerState stateAttachedTo)
    {
        this.stateAttachedTo = stateAttachedTo;
    }

    public void Execute(GameObject actor)
    {
        MovementDataHolder movementDataHolder = actor.GetComponent<IMovementDataHolder>().GetMovementDataHolder();
        Vector3 forceToAdd = movementDataHolder.LastMovementInput;
        actor.GetComponent<Rigidbody>().AddForce(forceToAdd);
    }
}
