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
    public bool playerCanMove = true;

    public GameObject camera;

    // Use this for initialization
    void Start () {
        controller = GetComponent<CharacterController>();
        moveDirection = new Vector3(0f, 0f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
        if(playerCanMove)
        {
            // Rotate this relative to the camera
            transform.rotation = Quaternion.Euler(0f, camera.GetComponent<PlayerCamera>().yaw, 0f);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        // Call the movement function
        Movement();

    }

    void Movement()
    {
        if (controller.isGrounded)
        {
            if (playerCanMove)
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
                movement.x = 0;
                movement.z = 0;
            }


            
        }

        else
        {
            if(playerCanMove)
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
            else
            {
                movement.x = 0;
                movement.z = 0;
            }


        }

        // applying movement
        movement.y -= gravity * Time.deltaTime;
        controller.Move(movement * Time.deltaTime);
    }

    
}
