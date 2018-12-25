using System;
using System.Collections;
using UnityEngine;

public interface IFactory
{
    System.Object CreateProduct(string productType, System.Object referenceObject, GameObject referncedGameObject);
}
