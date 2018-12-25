using UnityEngine;
using System.Collections;

public class InputHandlerUpdater
{
    private InputHandlerState currentInputHandler;
    private GameObject gameObjectAttachedTo;

    public InputHandlerState CurrentInputHandler { get { return currentInputHandler; } set { currentInputHandler = value; } }

    //Creates its own state based on client requirement
    public InputHandlerUpdater(GameObject gameAttachedTo)
    {
        this.gameObjectAttachedTo = gameAttachedTo;
    }

    public void UpdateCurrentInputHandler()
    {
        CurrentInputHandler.HandleInput();
    }

    public void FixedUpdateCurrentInputHandler()
    {
        if (CurrentInputHandler.UnExecutedCommands.Count != 0)
        {
            foreach (ICommand command in CurrentInputHandler.UnExecutedCommands)
                command.Execute(gameObjectAttachedTo);
        }
        currentInputHandler.ClearUnExecutedCommandsList();
    }
}

//public void SetDefaultInputHandlerState(InputStateFactory inputStateFactory, string defaultState, System.Object referenceObject)
//{
//    currentInputHandler = (InputHandlerState)inputStateFactory.CreateProduct(
//    defaultState, referenceObject, gameObjectAttachedTo);
//    Debug.Log(currentInputHandler);
//}

