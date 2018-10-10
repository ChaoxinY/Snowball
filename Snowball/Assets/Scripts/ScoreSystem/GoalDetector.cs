using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalDetector : MonoBehaviour
{
    private GoalScoreObserver goalObserver;
    private Subject subject;

    private void Start()
    {
        goalObserver = GetComponent<GoalScoreObserver>();
        subject = GameObject.Find("GameManager").GetComponent<Subject>();
        subject.AddObserver(goalObserver);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "SnowBall")
        {
            subject.Notify("GoalEvent", new GameObject[] { collision.gameObject } );
        }
    }
}
