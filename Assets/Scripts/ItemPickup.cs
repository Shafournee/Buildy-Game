using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickupPath { Stone, Wood }

public enum IsStackable { Stackable, UnStackable };

// This is the class that defines the behavior of the items
public class ItemPickups
{
    // This is the sprite the inventory displays
    public Sprite storedSprite;
    // The path that the block is loaded from
    public PickupPath path;
    // Checks if block can stack
    public IsStackable isStackable;
    public int stackAmount;
    public int maxStack;
}

public class ItemPickup : MonoBehaviour {

    public ItemPickups newPickup;
    public Sprite inventorySprite;
    public PickupPath itemPickupPath;
    public IsStackable stackablity;
    public int stackCount;

    // Use this for initialization
    void Awake () {
        stackCount = 1;
        newPickup = new ItemPickups();
        newPickup.storedSprite = inventorySprite;
        newPickup.path = itemPickupPath;
        newPickup.isStackable = stackablity;
        newPickup.stackAmount = stackCount;

        if(stackablity == IsStackable.Stackable)
        {
            newPickup.maxStack = 99;
        }
        else
        {
            newPickup.maxStack = 1;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
