using UnityEngine;
using System.Collections;

//Component
public class SnowBallCollisionHandler : MonoBehaviour, ICollideAble
{
    private Subject subject;
  
    private void Start()
    {
        subject = GameObject.Find("GameManager").GetComponent<Subject>();
    }

    public void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Goal":
                gameObject.GetComponent<SnowBallStatusHolder>().LastContactedGoalID = collision.gameObject.GetComponent<GoalStatusHolder>().GoalID;
                subject.Notify("GoalEvent", new GameObject[] { gameObject });
                break;
        }
    }

}
