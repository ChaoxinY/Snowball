using System.Collections.Generic;
using UnityEngine;

//Highest layer
public class Player : MonoBehaviour
{
    private IFactory inputStateFactory;
    private ICollideAble playerCollisionHandler;
    private InputHandlerUpdater inputhandlerUpdater;
    private PowerUpInventory powerUpInventory;
    private List<IUpdater> updaters = new List<IUpdater>();
    private List<IFixedUpdater> fixedUpdaters = new List<IFixedUpdater>();
    private List<ICollideAble> collideAbles = new List<ICollideAble>();

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

        updaters.Add(inputhandlerUpdater);
        fixedUpdaters.Add(inputhandlerUpdater);
        collideAbles.Add(playerCollisionHandler);
    }

    private void Update()
    {
        foreach (IUpdater updater in updaters)
        {
            updater.UpdateComponent();
        }
    }

    private void FixedUpdate()
    {
        foreach (IFixedUpdater fixedUpdater in fixedUpdaters)
        {
            fixedUpdater.FixedUpdateComponent();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        foreach (ICollideAble collideAble in collideAbles)
        {
            collideAble.ReactToCollision(collision);
        }
    }
}
