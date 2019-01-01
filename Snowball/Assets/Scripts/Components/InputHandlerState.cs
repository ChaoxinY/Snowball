using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class InputHandlerState
{
    protected Vector3 lastMovementInput;
    protected InputStateFactory inputStateFactory;
    public Vector3 LastMovementInput { get { return lastMovementInput; } }
    protected List<ICommand> fixedUpdateCommands = new List<ICommand>();
    public List<ICommand> UnExecutedCommands { get { return fixedUpdateCommands; } }
    public abstract void HandleInput();
    public void ClearUnExecutedCommandsList() { UnExecutedCommands.Clear(); }
}

