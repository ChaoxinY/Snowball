using System.Collections.Generic;
using UnityEngine;

//Highest layer
public class Player : MonoBehaviour, IInputSchemeHolder,IMovementDataHolder
{
    private List<IUpdater> updaters = new List<IUpdater>();
    private List<IFixedUpdater> fixedUpdaters = new List<IFixedUpdater>();
    private List<ICollideAble> collideAbles = new List<ICollideAble>();
    private InputScheme inputScheme;
    private MovementDataHolder movementDataHolder;

    public IFactory InputStateFactory { get { return InputStateFactory; } internal set { InputStateFactory = value; } }
    public ICollideAble PlayerCollisionHandler { get { return PlayerCollisionHandler; } internal set { PlayerCollisionHandler = value; } }
	public InputHandlerUpdater InputHandlerUpdater { get { return InputHandlerUpdater; } internal set { InputHandlerUpdater = value; } }
	public PowerUpInventory PowerUpInventory { get { return PowerUpInventory; } internal set { PowerUpInventory = value; } }

    //Read scriptableobject for different configurations
    private void Start()
    {
        InputStateFactory = new InputStateFactory();
		InputHandlerUpdater = new InputHandlerUpdater(gameObject)
		{
			CurrentInputHandler = (InputHandlerState)InputStateFactory.CreateProduct(
			(int)FactoriesProducts.InputstateProducts.PlayerInputHandlerDefaultState, this, gameObject)
		};
		PowerUpInventory = new PowerUpInventory();
		PlayerCollisionHandler = new PlayerCollisionHandler(gameObject, PowerUpInventory);
        movementDataHolder = Resources.Load("Prefabs/DefaultMovementDataHolder") as MovementDataHolder;

        updaters.Add(InputHandlerUpdater);
        fixedUpdaters.Add(InputHandlerUpdater);
        collideAbles.Add(PlayerCollisionHandler);
    }

    private void Update()
    {
        SystemToolMethods.UpdateIUpdaters(updaters);
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

    public InputScheme GetInputScheme()
    {
        return inputScheme;
    }

    public void SetInputScheme(InputScheme inputScheme)
    {
        this.inputScheme = inputScheme;
    }

    public MovementDataHolder GetMovementDataHolder()
    {
        return movementDataHolder;
    }

    public void SetMovementDataHolder(MovementDataHolder movementDataHolder)
    {
        this.movementDataHolder = movementDataHolder;
    }
}
