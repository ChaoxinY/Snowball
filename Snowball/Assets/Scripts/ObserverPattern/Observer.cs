using UnityEngine;
using System.Collections;

public abstract class Observer : MonoBehaviour
{
    private Subject subject;
    private void Start()
    {
        subject = GameObject.Find("GameManager").GetComponent<Subject>();
        subject.AddObserver(this);
    }
    public abstract void OnNotify(string eventName, GameObject[] associatedGameObjects);
}
