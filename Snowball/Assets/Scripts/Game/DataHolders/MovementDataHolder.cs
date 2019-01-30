using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "NewMovementDataHolder", menuName = "Dataholders/MovementDataHolder")]
public class MovementDataHolder : ScriptableObject
{
    [SerializeField]
    private float movementSpeed, rotationSpeed;
    public float MovementSpeed { get { return movementSpeed; } }
    public float RotationSpeed { get { return rotationSpeed; } }
    private Vector3 lastMovementInput;
    public Vector3 LastMovementInput
    {
        get
        {
            return lastMovementInput;
        }

        set
        {
            lastMovementInput = value;
        }
    }

  
}
