using System.Collections.Generic;
using UnityEngine;

public class InputStateFactory : IFactory
{
	public List<string> requirementPointerNames = new List<string> { "InputHandlerUpdater" };

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
