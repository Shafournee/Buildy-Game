using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour {


    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

        if(!Physics.Raycast(transform.position, Vector3.down, .15f))
        {
            transform.Translate(Vector3.down * Time.deltaTime * 2f);
            GetComponentInChildren<ItemDropVisual>().onGround = false;
            
        }
        else
        {
            GetComponentInChildren<ItemDropVisual>().onGround = true;
            GetComponentInChildren<ItemDropVisual>().startPosition = transform.position;
        }

    }

}
