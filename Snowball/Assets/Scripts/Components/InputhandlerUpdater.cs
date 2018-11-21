using UnityEngine;
using System.Collections;

public class InputhandlerUpdater
{
    public InputHandlerState currentInputHandler;

    public InputhandlerUpdater(InputHandlerState startingInputHandler)
    {
        currentInputHandler = startingInputHandler;
    }

    public void UpdateCurrentInputHandler()
    {
        currentInputHandler.HandleInput();
    }
}
