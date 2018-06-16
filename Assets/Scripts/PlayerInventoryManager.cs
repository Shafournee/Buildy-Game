using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryManager : MonoBehaviour {

    public ItemPickups[] itemsStored = new ItemPickups[36];
    GameObject canvas;
    public ItemPickups temporaryItemStored;
    public ItemPickups swappingStorage;

    // Use this for initialization
    void Start () {
        canvas = GameObject.FindGameObjectWithTag("Canvas");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool IsInventoryFull()
    {
        for(int i = 0; i < itemsStored.Length; i++)
        {
            // If there are empty slots, return true
            if(itemsStored[i] == null)
            {
                return false;
            }
        }

        // If all slots are full return true
        return true;
    }

    public void AddItemToInventory(ItemPickups pickup)
    {
        // If the item is stackable run this loop
        if (pickup.isStackable == IsStackable.Stackable)
        {
            int emptySpot = -1;
            // Store an empty slot incase there is already a full stack, or no stacks at all
            for (int i = 0; i < itemsStored.Length; i++)
            {
                if (emptySpot == -1 && itemsStored[i] == null)
                {
                    emptySpot = i;
                }
                // Ignore empty slots from this point on
                else if (itemsStored[i] == null)
                {

                }
                // If the item in the slot is the same as the pickup and it's not at a full stack, add another to the stack
                else if (itemsStored[i].path == pickup.path && itemsStored[i].stackAmount + pickup.stackAmount <= 99)
                {
                    itemsStored[i].stackAmount += pickup.stackAmount;
                    emptySpot = -1;
                    break;
                }
            }
            if(emptySpot != -1)
            {
                itemsStored[emptySpot] = pickup;
            }

        }
        else
        {
            // Find the empty slot, and store it
            for(int i = 0; i < itemsStored.Length; i++)
            {
                if (itemsStored[i] == null)
                {
                    itemsStored[i] = pickup;
                    break;
                }
            }
        }
        // Tell the inventory to update when you pickup an item
        canvas.GetComponent<PlayerInventoryUI>().UpdateTheUI();
    }
}
