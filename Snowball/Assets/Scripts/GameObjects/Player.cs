using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    private InputhandlerUpdater inputhandlerUpdater;
    private PlayerCollisionHandler playerCollisionHandler;
    private PowerUpInventory powerUpInventory;

    public InputhandlerUpdater InputhandlerUpdater { get { return inputhandlerUpdater; } }
    public PlayerCollisionHandler PlayerCollisionHandler { get { return playerCollisionHandler; } }
    public PowerUpInventory PowerUpInventory { get { return powerUpInventory; } }

    private void Start()
    {
        inputhandlerUpdater = new InputhandlerUpdater(new PlayerInputHandlerDefaultState(gameObject, InputhandlerUpdater));
        powerUpInventory = new PowerUpInventory();
        playerCollisionHandler = new PlayerCollisionHandler(gameObject,powerUpInventory);      
    }

    private void Update()
    {
        InputhandlerUpdater.UpdateCurrentInputHandler();
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayerCollisionHandler.ReactToCollision(collision);
    }
}
