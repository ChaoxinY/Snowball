using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class InputStateFactory : ComponentFactory, IFactory
{
    public InputStateFactory()
    {
        productRequirements = new List<string> { "InputHandlerUpdater" };
    }

    //Foreach automation
    //What happens if multiple components are needed, more if statements ?
    //You are ready know that you are making an inputhandler so you know the dependencies of all the products.
    //Set the methdoe to static?
   
    public System.Object CreateProduct(string productType, System.Object referenceObject, GameObject referenceObjectGameObject)
    {
        System.Object product = null;

        List<System.Object> requirements = SystemToolMethods.ReturnObjectPointers(referenceObject, productRequirements);
        InputHandlerUpdater inputHandlerUpdater = (InputHandlerUpdater)requirements[0];

        switch (productType)
        {
            case "PlayerInputHandlerDefaultState":
                product = new PlayerInputHandlerDefaultState(referenceObject, referenceObjectGameObject, inputHandlerUpdater);
                break;
        }

        return product;
    }
}
