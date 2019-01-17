using UnityEngine;
using System.Collections;

public class AddForceToRigidbodyCommand : ICommand
{
    InputHandlerState stateAttachedTo;

    public AddForceToRigidbodyCommand(InputHandlerState stateAttachedTo)
    {
        this.stateAttachedTo = stateAttachedTo;
    }

    public void Execute(GameObject actor)
    {        
        Vector3 forceToAdd = stateAttachedTo.LastMovementInput;
        actor.GetComponent<Rigidbody>().AddForce(forceToAdd);
    }
}
