using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBall : MonoBehaviour {

    private Subject eventSubject;
    private SnowBallCollisionHandler snowBallCollisionHandler;

    private void Start()
    {
        eventSubject = GameObject.Find("GameManager").GetComponent<Subject>();
        snowBallCollisionHandler = new SnowBallCollisionHandler(gameObject, eventSubject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        snowBallCollisionHandler.ReactToCollision(collision);
    }

}
