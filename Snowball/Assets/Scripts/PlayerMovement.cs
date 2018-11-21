using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField]
    private float movementSpeed, rotationSpeed;

    private Rigidbody myRigidbody;
    private Vector3 direction;

	void Start () {
        myRigidbody = GetComponent<Rigidbody>();
        direction = Vector3.zero;
	}
	
	void Update () {
        // Get player input
        direction = new Vector3(Input.GetAxis("JoystickLeftHorizontal"), 0, Input.GetAxis("JoystickLeftVertical"));
    }

    private void FixedUpdate()
    {
        move();
    }

    private void move()
    {
        // If player isn't moving do nothing
        if (direction == Vector3.zero)
        {
            return;
        }

        // Move player.
        myRigidbody.velocity = direction.normalized * movementSpeed;

        // Player faces movement direction.
        Quaternion newRotaion = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotaion, Time.deltaTime * rotationSpeed);
    }
}
