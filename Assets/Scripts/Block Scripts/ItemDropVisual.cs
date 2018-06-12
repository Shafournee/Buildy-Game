using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropVisual : MonoBehaviour {

    public Vector3 startPosition;

    //adjust this to change speed
    float speed = 2f;
    //adjust this to change how high it goes
    float height = 0.1f;

    public bool onGround = true;

    // Use this for initialization
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the object 60 degrees per second
        transform.RotateAround(transform.GetComponent<Renderer>().bounds.center, transform.up, Time.deltaTime * 60f);

        if (onGround)
        {
            //calculate what the new Y position will be
            float newY = Mathf.Cos(Time.time * speed);
            //set the object's Y to the new calculated Y
            transform.position = new Vector3(transform.position.x, startPosition.y + newY * height, transform.position.z);
        }

    }

}
