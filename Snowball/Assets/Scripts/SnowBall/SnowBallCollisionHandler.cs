using UnityEngine;
using System.Collections;

//Component
public class SnowBallCollisionHandler : ICollideAble
{
    private GameObject gameObjectAttachedTo;
    private Subject subject;

    public SnowBallCollisionHandler(GameObject gameObjectAttachedTo, Subject subjectToSubscribe) {
        this.gameObjectAttachedTo = gameObjectAttachedTo;
        subject = subjectToSubscribe;
    }

    private void Start()
    {
        subject = GameObject.Find("GameManager").GetComponent<Subject>();
    }

    public void ReactToCollision(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Goal":
                Debug.Log(gameObjectAttachedTo.GetComponent<SnowBallStatusHolder>());
                Debug.Log(collision.gameObject.GetComponent<GoalStatusHolder>());
                gameObjectAttachedTo.GetComponent<ISnowBallStatusHolder>().GetSnowBallStatusHolder().LastContactedGoalID = collision.gameObject.GetComponent<GoalStatusHolder>().GoalID;
                subject.Notify("GoalEvent", new GameObject[] { gameObjectAttachedTo });
                break;
        }
    }
}
