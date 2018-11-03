using UnityEngine;
using System.Collections;

public interface ICommand 
{
    void Execute(GameObject Actor);
}
