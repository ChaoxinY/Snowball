using UnityEngine;
using System.Collections;

public abstract class Observer : MonoBehaviour
{
    public abstract void OnNotify(string eventName, GameObject[] associatedGameObjects);
}
