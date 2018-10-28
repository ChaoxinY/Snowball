using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField]
    private float speed;

    private Rigidbody myRigidbody;
    private Vector3 dir;

	void Start () {
        myRigidbody = GetComponent<Rigidbody>();
        dir = Vector3.zero;
	}
	
	void Update () {
        // Get player input
        dir = new Vector3(Input.GetAxis("JoystickLeftHorizontal"), 0, Input.GetAxis("JoystickLeftVertical"));
    }

    private void FixedUpdate()
    {
        // Move player.
        myRigidbody.velocity = dir.normalized * speed;
    }
}
