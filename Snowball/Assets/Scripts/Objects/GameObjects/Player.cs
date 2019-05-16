using System.Collections.Generic;
using UnityEngine;

//Highest layer
public class Player : MonoBehaviour, IInputSchemeHolder,IMovementDataHolder
{
	#region Variables
	private List<IUpdater> updaters = new List<IUpdater>();
    private List<IFixedUpdater> fixedUpdaters = new List<IFixedUpdater>();
    private List<ICollideAble> collideAbles = new List<ICollideAble>();

	public IFactory InputStateFactory { get; private set; }
	public ICollideAble PlayerCollisionHandler { get; private set; }
	public InputHandlerUpdater InputHandlerUpdater { get; private set; }
	public PowerUpInventory PowerUpInventory { get; private set; }
	public InputScheme InputScheme { get; set; }
	public MovementDataHolder MovementDataHolder { get; set; }
	#endregion Variables

	#region Initialization
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
        MovementDataHolder = Resources.Load("Prefabs/DefaultMovementDataHolder") as MovementDataHolder;

        updaters.Add(InputHandlerUpdater);
        fixedUpdaters.Add(InputHandlerUpdater);
        collideAbles.Add(PlayerCollisionHandler);
    }
	#endregion

	#region Functionality
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
	#endregion
}
