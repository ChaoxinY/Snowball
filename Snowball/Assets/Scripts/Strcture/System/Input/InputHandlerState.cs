using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public abstract class InputHandlerState
{
    protected InputStateFactory inputStateFactory = new InputStateFactory();
    private InputHandlerUpdater inputHandlerUpdater;
    protected List<ICommand> fixedUpdateCommands = new List<ICommand>();
    public List<ICommand> FixedUpdateCommands { get { return fixedUpdateCommands; } }

    protected InputHandlerUpdater InputHandlerUpdater
    {
        get
        {
            return inputHandlerUpdater;
        }

        set
        {
            inputHandlerUpdater = value;
        }
    }

    public abstract void HandleInput();
    public void ClearUnExecutedCommandsList() { FixedUpdateCommands.Clear(); }
 }

public interface IControllerInputHandler {
    void HandleControllerInput(string inputString);
}

public interface IKeyboardMouseInputHandler
{
    void HandleKeyBoardMouseInput(string inputString);
}
