using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {

    [SerializeField] GameObject player;

    [SerializeField] float horizontalSpeed = 1f;
    [SerializeField] float verticalSpeed = 1f;

    public float yaw = 0.0f;
    private float pitch = 0.0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // TODO move the camera position up
        transform.position = player.GetComponent<Transform>().position;
        CameraRotation();

    }

    void CameraRotation()
    {
        // Limit the rotation for the camera
        if (pitch >= 90)
        {
            pitch = 90;
        }
        else if (pitch <= -90)
        {
            pitch = -90;
        }

        // Rotate the camera
        yaw += horizontalSpeed * Input.GetAxis("Mouse X");
        pitch -= verticalSpeed * Input.GetAxis("Mouse Y");
        transform.eulerAngles = new Vector3(pitch, yaw, 0);
    }
}
