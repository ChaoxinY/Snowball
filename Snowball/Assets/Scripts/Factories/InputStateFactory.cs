using System;
using System.Collections;
using System.Reflection;
using UnityEngine;

public class InputStateFactory : IFactory
{
    public System.Object CreateProduct(string productType, System.Object referenceObject, GameObject referenceObjectGameObject)
    {
        System.Object product = null;
        //Needs refactoring for string 
        if (SystemToolMethods.CheckIfPropertyExsists(referenceObject, "InputHandlerUpdater"))
        {
            PropertyInfo propertyInfo = SystemToolMethods.ReturnPropertyInfo(referenceObject, "InputHandlerUpdater");
           
            InputHandlerUpdater inputHandlerUpdater = (InputHandlerUpdater)SystemToolMethods.ReturnObjectComponent(referenceObject, propertyInfo, "InputHandlerUpdater");
            
            switch (productType)
            {
                case "PlayerInputHandlerDefaultState":
                    product = new PlayerInputHandlerDefaultState(referenceObject, referenceObjectGameObject, inputHandlerUpdater);
                    break;
            }
        }
        return product;
    }
}
