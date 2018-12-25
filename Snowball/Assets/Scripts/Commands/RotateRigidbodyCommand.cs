using UnityEngine;
using System.Collections;
//
public class RotateRigidbodyCommand : ICommand
{
    InputHandlerState stateAttachedTo;

    public RotateRigidbodyCommand(InputHandlerState stateAttachedTo)
    {
        this.stateAttachedTo = stateAttachedTo;
    }
    public void Execute(GameObject actor)
    {
        Vector3 movementDirection = stateAttachedTo.LastMovementInput;
        Quaternion newRotaion = Quaternion.LookRotation(movementDirection);
        Transform actorTransform = actor.GetComponent<Transform>();
        actorTransform.rotation = Quaternion.Slerp(actorTransform.rotation, newRotaion, Time.deltaTime * actor.GetComponent<MovementDataHolder>().RotationSpeed);
    }
}
