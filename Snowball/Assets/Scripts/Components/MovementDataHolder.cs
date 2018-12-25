using UnityEngine;
using System.Collections;

public class MovementDataHolder : MonoBehaviour
{      
    [SerializeField]
    private float movementSpeed, rotationSpeed;
    public float MovementSpeed { get { return movementSpeed; } }
    public float RotationSpeed { get { return rotationSpeed; } }

    public MovementDataHolder(float movementSpeed, float rotationSpeed) {
        this.movementSpeed = movementSpeed;
        this.rotationSpeed = rotationSpeed;
    }
}
