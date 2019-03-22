using UnityEngine;

public class RotateRigidbodyCommand : ICommand
{
    InputHandlerState stateAttachedTo;

    public RotateRigidbodyCommand(InputHandlerState stateAttachedTo)
    {
        this.stateAttachedTo = stateAttachedTo;
    }
    public void Execute(GameObject actor)
    {
        MovementDataHolder movementDataHolder = actor.GetComponent<IMovementDataHolder>().GetMovementDataHolder();
        Vector3 movementDirection = movementDataHolder.LastMovementInput;
        Quaternion newRotaion = Quaternion.LookRotation(movementDirection);
        Transform actorTransform = actor.GetComponent<Transform>();
        actorTransform.rotation = Quaternion.Slerp(actorTransform.rotation, newRotaion, Time.deltaTime * movementDataHolder.RotationSpeed);
    }
}
