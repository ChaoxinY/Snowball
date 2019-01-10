using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public abstract class InputHandlerState
{
    protected Vector3 lastMovementInput;
    public Vector3 LastMovementInput { get { return lastMovementInput; } }
    protected InputStateFactory inputStateFactory;
     protected List<ICommand> fixedUpdateCommands = new List<ICommand>();
    public List<ICommand> FixedUpdateCommands { get { return fixedUpdateCommands; } }
    public abstract void HandleInput();
    public void ClearUnExecutedCommandsList() { FixedUpdateCommands.Clear(); }
  
}

