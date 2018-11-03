using UnityEngine;
using System.Collections;

public class PlayerCollisionHandler : MonoBehaviour, ICollideAble
{
    public PowerUpInventory PowerUpInventory;

    public void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {

            case "Snowball":
                break;
            case "Snowpile":
                collision.gameObject.GetComponent<Snowpile>().PileUpSnowBall();
                break;
            case "Player":
                break;
            case "PowerUp":
                gameObject.GetComponent<IPowerUpHolder>().CollectPowerUp(collision.gameObject.GetComponent<PowerUp>());
                break;
        }
    }
}
