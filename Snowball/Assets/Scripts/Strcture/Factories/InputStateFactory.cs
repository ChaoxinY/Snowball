using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class InputStateFactory : ComponentFactory, IFactory
{
    public InputStateFactory()
    {
        //String search is slow(when the class has many properties) and goes wrong easily(all properties in the object class have to have
        //the same name)
        requirementPointerNames = new List<string> { "InputHandlerUpdater"};
    }

    public System.Object CreateProduct(int productType, System.Object referenceObject, GameObject referenceObjectGameObject = null)
    {
        System.Object product = null;

        //Is going to be repeated in other classes.
        //1.Put this in constructor?(Forced to use a larger scope, might create overhead)
        //This structure is fragile
        List<System.Object> requirementPointers = SystemToolMethods.ReturnObjectPointers(referenceObject, requirementPointerNames);
        InputHandlerUpdater inputHandlerUpdater = (InputHandlerUpdater)requirementPointers[0];

        switch (productType)
        {
            case (int)FactoriesProducts.InputstateProducts.PlayerInputHandlerDefaultState:
                product = new PlayerInputHandlerDefaultState(referenceObject, referenceObjectGameObject, inputHandlerUpdater);
                break;
            case (int)FactoriesProducts.InputstateProducts.UINavigatorDefaultState:
                product = new UINavigatorInputHandlerDefaultState(inputHandlerUpdater);
                break;
            case (int)FactoriesProducts.InputstateProducts.UINavigatorInGameState:
                product = new UINavigatorInputHandlerInGameState(inputHandlerUpdater);
                break;
        }
        return product;
    }
}
