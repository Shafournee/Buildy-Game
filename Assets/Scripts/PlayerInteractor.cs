using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour {

    public GameObject camera;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(GetComponent<PlayerController>().playerCanMove)
        {
            Interact();
        }
        
	}

    void Interact()
    {
        //If you're pressing the mouse down raycast
        if(Input.GetMouseButton(0))
        {
            RaycastHit hit;
            bool objectWasHit = Physics.Raycast(transform.position, camera.transform.forward, out hit, 2f);

            // If an object was hit, check if it has the block script, and if so call the left click function
            if(objectWasHit == true)
            {
                GameObject Block = hit.transform.gameObject;

                if (Block.GetComponent<BlockType>() != null)
                {
                    Block.GetComponent<BlockType>().OnPlayerLeftClick();
                }
            }
            

            

        }
    }

    public void ItemDropper(ItemPickups drop, int dropAmount)
    {
        GameObject droppedItem = Instantiate(Resources.Load("ItemDropPrefabs/" + drop.path + "Pickup", typeof(GameObject))) as GameObject;
        droppedItem.GetComponent<ItemPickup>().newPickup.stackAmount = dropAmount;
        droppedItem.transform.position = transform.position + transform.forward * 3f;
    }
}
