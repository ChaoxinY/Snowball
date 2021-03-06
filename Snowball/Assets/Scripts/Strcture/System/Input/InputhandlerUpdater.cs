﻿using UnityEngine;
using System.Collections;

public class InputHandlerUpdater : IUpdater, IFixedUpdater
{
    private InputHandlerState currentInputHandler;
    private GameObject gameObjectAttachedTo;

    public InputHandlerState CurrentInputHandler { get { return currentInputHandler; } set { currentInputHandler = value; } }

    public InputHandlerUpdater(GameObject gameAttachedTo)
    {
        this.gameObjectAttachedTo = gameAttachedTo;
    }

    public void UpdateComponent()
    {
        CurrentInputHandler.HandleInput();
    }

    public void FixedUpdateComponent()
    {
        if (CurrentInputHandler.FixedUpdateCommands.Count != 0)
        {
            foreach (ICommand command in CurrentInputHandler.FixedUpdateCommands)
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

