using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour {


    // Use this for initialization
    void Start () {
        //Set the pickup radius to 4
        GetComponent<SphereCollider>().radius = 4f;
	}
	
	// Update is called once per frame
	void Update () {

        if(!Physics.Raycast(transform.position, Vector3.down, .15f, 9))
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

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<PlayerInventoryManager>() != null)
        {
            // Check if the inventory is full. If it isn't, destroy the game object, and store it in the open slot
            if(!collider.GetComponent<PlayerInventoryManager>().IsInventoryFull())
            {
                collider.GetComponent<PlayerInventoryManager>().AddItemToInventory(GetComponent<ItemPickup>().newPickup);
                Destroy(gameObject);
            }
        }
    }

}
