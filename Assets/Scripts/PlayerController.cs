using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    //KeyCodes
    KeyCode jump = KeyCode.Space;
    KeyCode up = KeyCode.W;
    KeyCode left = KeyCode.A;
    KeyCode right = KeyCode.D;
    KeyCode down = KeyCode.S;


    CharacterController controller;

    // variables
    [SerializeField] float speed;
    [SerializeField] float jumpSpeed;
    [SerializeField] float gravity;

    Vector3 moveDirection;
    Vector3 movement;

    public GameObject camera;

    // Use this for initialization
    void Start () {
        controller = GetComponent<CharacterController>();
        moveDirection = new Vector3(0f, 0f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
        // Call the movement function
        Movement();
        // Rotate this relative to the camera
        transform.rotation = Quaternion.Euler(0f, camera.GetComponent<PlayerCamera>().yaw, 0f);

    }

    void Movement()
    {
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(0f, 0f, 0f);

            // forwards + backwards
            if (Input.GetKey(up))
            {
                moveDirection += transform.forward;
            }
            else if (Input.GetKey(down))
            {
                moveDirection += -transform.forward;
            }

            // right + left
            if (Input.GetKey(right))
            {
                moveDirection += transform.right;
            }
            else if (Input.GetKey(left))
            {
                moveDirection += -transform.right;
            }

            // Finish the vector
            movement = speed * moveDirection;

            // Jumping
            if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
            {
                movement.y = jumpSpeed;
            }
        }

        else
        {
            moveDirection = new Vector3(0f, movement.y, 0f);

            // forwards + backwards
            if (Input.GetKey(up))
            {
                moveDirection += transform.forward;
            }
            else if (Input.GetKey(down))
            {
                moveDirection += -transform.forward;
            }

            // right + left
            if (Input.GetKey(right))
            {
                moveDirection += transform.right;
            }
            else if (Input.GetKey(left))
            {
                moveDirection += -transform.right;
            }

            // Finish the vector
            movement.x = speed * moveDirection.x;
            movement.z = speed * moveDirection.z;
        }

        // applying movement
        movement.y -= gravity * Time.deltaTime;
        controller.Move(movement * Time.deltaTime);
    }
}
