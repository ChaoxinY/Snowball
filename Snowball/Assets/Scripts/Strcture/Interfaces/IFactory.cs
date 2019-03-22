using UnityEngine;

public interface IFactory
{   
    //Changed product type parameter from string to int to prevent errors that might be caused when enum gets changed up
    //as well as the extra bloated information thats been created when creating these enums.
    System.Object CreateProduct(int productType, System.Object referenceObject, GameObject referncedGameObject);
}
