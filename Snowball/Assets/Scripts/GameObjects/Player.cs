using UnityEngine;

public class Player : MonoBehaviour
{
    private IFactory inputStateFactory;
    private ICollideAble playerCollisionHandler;
    private InputHandlerUpdater inputhandlerUpdater;
    private PowerUpInventory powerUpInventory;

    public IFactory InputStateFactory { get { return inputStateFactory; } }
    public ICollideAble PlayerCollisionHandler { get { return playerCollisionHandler; } }
    public InputHandlerUpdater InputHandlerUpdater { get { return inputhandlerUpdater; } }
    public PowerUpInventory PowerUpInventory { get { return powerUpInventory; } }

    private void Start()
    {
        inputStateFactory = new InputStateFactory();
        inputhandlerUpdater = new InputHandlerUpdater(gameObject);
        inputhandlerUpdater.CurrentInputHandler = (InputHandlerState)InputStateFactory.CreateProduct
            (FactoriesProducts.InputstateProducts.PlayerInputHandlerDefaultState.ToString(), this, gameObject);
        powerUpInventory = new PowerUpInventory();
        playerCollisionHandler = new PlayerCollisionHandler(gameObject, powerUpInventory);
    }

    private void Update()
    {
        InputHandlerUpdater.UpdateCurrentInputHandler();
    }

    private void FixedUpdate()
    {
        InputHandlerUpdater.FixedUpdateCurrentInputHandler();
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayerCollisionHandler.ReactToCollision(collision);
    }
}
