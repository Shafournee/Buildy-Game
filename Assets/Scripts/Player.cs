using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //KeyCodes
    KeyCode jump = KeyCode.Space;
    KeyCode up = KeyCode.W;
    KeyCode left = KeyCode.A;
    KeyCode right = KeyCode.D;
    KeyCode down = KeyCode.S;

    //Bools
    public bool playerCanMove = true;

    //Components
    Rigidbody rigidBody;
    Collider colliderBody;
    public GameObject camera;

    //variables
    [SerializeField] float speed = 500f;
    [SerializeField] float speedCap = 5f;

    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        colliderBody = GetComponent<BoxCollider>();
    }
	
	// Update is called once per frame
	void Update () {

        //Should stop movement during cutscenes
        if (playerCanMove)
        {
            //Place movement functions in here
            Movement();
            JumpMovement();
            //Set the player rotation to the rotation of the camera
            transform.rotation = Quaternion.Euler(0f, camera.GetComponent<PlayerCamera>().yaw, 0f);

            //Set a cap on how fast the player can move
            if (rigidBody.velocity.magnitude >= speedCap)
            {
                rigidBody.velocity = rigidBody.velocity.normalized * speedCap;
            }
            else if (rigidBody.velocity.magnitude <= -speedCap)
            {
                rigidBody.velocity = rigidBody.velocity.normalized * speedCap;
            }
        }
    }

    void Movement()
    {
        if (Input.GetKey(up) && Input.GetKey(right))
        {
            rigidBody.AddForce((transform.forward + transform.right).normalized * speed);
        }
        else if (Input.GetKey(up) && Input.GetKey(left))
        {
            rigidBody.AddForce((transform.forward - transform.right).normalized * speed);
        }
        else if (Input.GetKey(down) && Input.GetKey(right))
        {
            rigidBody.AddForce((-transform.forward + transform.right).normalized * speed);
        }
        else if (Input.GetKey(down) && Input.GetKey(left))
        {
            rigidBody.AddForce((-transform.forward - transform.right).normalized * speed);
        }
        else if (Input.GetKey(up))
        {
            rigidBody.AddForce(transform.forward * speed);
        }
        else if (Input.GetKey(down))
        {
            rigidBody.AddForce(transform.forward * -speed);
        }
        else if (Input.GetKey(right))
        {
            rigidBody.AddForce(transform.right * speed);
        }
        else if (Input.GetKey(left))
        {
            rigidBody.AddForce(transform.right * -speed);
        }
    }

    void JumpMovement()
    {
        if (Input.GetKeyDown(jump) /*&& CheckIfPlayerCanJump()*/)
        {
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, 8f, rigidBody.velocity.z);
        }
    }

    //TODO make it so it doesn't just check the center i.e. check the left and right side of the player
    bool CheckIfPlayerCanJump()
    {
        Vector3 position = new Vector3(transform.position.x, colliderBody.bounds.min.y, transform.position.z);
        bool canPlayerJump = Physics.Raycast(position, Vector3.down, .1f);
        print(canPlayerJump);
        return canPlayerJump;
    }
}
