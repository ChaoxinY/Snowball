using UnityEngine;
using System.Collections;

public class SnowBallCollisionManager : MonoBehaviour
{
    private SnowBallStatusManager snowBallStatusManager;

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag) {
            case "Snowpile":
                SnowpileHitFeedBack();
                break;
        }
    }

    private void SnowpileHitFeedBack(){
        snowBallStatusManager.SnowBallPointValue += 30;
    }
}
