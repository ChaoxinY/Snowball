using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Subject : MonoBehaviour
{
    List<Observer> observers = new List<Observer>();

    public void Notify(string eventName, GameObject[] associatedGameObjects)
    {
        foreach (Observer observer in observers)
        {
            observer.OnNotify(eventName, associatedGameObjects);
        }
    }

    public void AddObserver(Observer observer)
    {
        observers.Add(observer);
    }

    public void RemoveObserver(Observer observer)
    {
        observers.Remove(observer);
    }
}
