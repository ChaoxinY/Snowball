using UnityEngine;

public class PlayerCollisionHandler : ICollideAble
{
    private GameObject gameobjectAttachedTo;
    private PowerUpInventory powerUpInventory;

    public PlayerCollisionHandler(GameObject gameobjectAttachedTo, PowerUpInventory powerUpInventoryToStorePowerUp)
    {
        this.gameobjectAttachedTo = gameobjectAttachedTo;
        powerUpInventory = powerUpInventoryToStorePowerUp;
    }

    public void ReactToCollision(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Snowball":
                collision.gameObject.GetComponent<ISnowBallStatusHolder>().SnowBallStatusHolder.SnowballOwner = gameobjectAttachedTo;
                break;
            case "Snowpile":
                collision.gameObject.GetComponent<Snowpile>().PileUpSnowBall();
                break;
            case "Player":
                break;
            case "PowerUp":
                powerUpInventory.CollectPowerUp(collision.gameObject.GetComponent<PowerUp>());
                break;
        }
    }
}
