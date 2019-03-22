using UnityEngine;

[CreateAssetMenu(fileName = "NewMovementDataHolder", menuName = "Dataholders/MovementDataHolder")]
public class MovementDataHolder : ScriptableObject
{
	[SerializeField]
	private float movementSpeed, rotationSpeed;
	public float MovementSpeed { get { return movementSpeed; } }
	public float RotationSpeed { get { return rotationSpeed; } }
	public Vector3 LastMovementInput { get { return LastMovementInput; } set { LastMovementInput = value; } }
}
