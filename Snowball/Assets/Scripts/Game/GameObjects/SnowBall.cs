using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBall : MonoBehaviour, ISnowBallStatusHolder
 {
	[SerializeField]
    private EventSubject eventSubject;
    private SnowBallCollisionHandler snowBallCollisionHandler;
    public SnowBallStatusHolder snowBallStatusHolder;

    private void Start()
    {
        //eventSubject = GameObject.Find("GameManager").GetComponent<Subject>();
        snowBallCollisionHandler = new SnowBallCollisionHandler(gameObject, eventSubject);
       // snowBallStatusHolder = Resources.Load("Prefabs/DefaultSnowBallStatusHolder") as SnowBallStatusHolder;
        snowBallStatusHolder.Snowball = gameObject;
	}

    private void OnCollisionEnter(Collision collision)
    {
        snowBallCollisionHandler.ReactToCollision(collision);
    }

    public SnowBallStatusHolder GetSnowBallStatusHolder()
    {
		return snowBallStatusHolder;
    }

    public void SetSnowBallStatusHolder(SnowBallStatusHolder snowBallStatusHolder)
    {
		this.snowBallStatusHolder = snowBallStatusHolder;
    }
}
