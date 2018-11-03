using UnityEngine;
using System.Collections;

public class PlayerCollisionHandler : MonoBehaviour, ICollideAble
{
    public PowerUpInventory PowerUpInventory;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Called");
            PowerUpInventory.UsePowerUp(0);
        }
    }


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
