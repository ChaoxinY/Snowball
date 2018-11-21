using UnityEngine;
using System.Collections;

public abstract class InputHandlerState
{ 
    protected Vector3 lastMovementInput;
    public Vector3 LastMovementInput { get { return lastMovementInput; } }
    public abstract void HandleInput();
}
