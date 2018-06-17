using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerInventoryUI : MonoBehaviour {

    KeyCode inventoryKey = KeyCode.E;
    bool inventoryDisplay = true;

    [SerializeField] GameObject player;
    [SerializeField] GameObject inventory;
    [SerializeField] GameObject camera;
    [SerializeField] GameObject itemBar;
    [SerializeField] GameObject itemDraggedByMouse;

    [SerializeField] GameObject[] ItemDisplay;

    PlayerInventoryManager inventoryScript;

    int slotIndex = -1;

    // Use this for initialization
    void Start () {
        inventory.SetActive(false);
        player.GetComponent<PlayerController>().playerCanMove = true;
        itemBar.SetActive(true);
        itemBar.GetComponent<PlayerInventoryBar>().DisplayMenuBar();
        EventSystem.current.GetComponent<AltStandaloneInputModule>().mouseCanClick = false;
        inventoryScript = player.GetComponent<PlayerInventoryManager>();
    }

    // Update is called once per frame
    void Update() {
        ToggleTheInventoryScreen();
        itemDraggedByMouse.transform.position = Input.mousePosition;
        MoveItemsInInventory();
    }

    void ToggleTheInventoryScreen()
    {
        if (Input.GetKeyDown(inventoryKey))
        {
            if (inventoryDisplay)
            {
                // Activate the inventory
                inventory.SetActive(true);
                // Stop player movement
                player.GetComponent<PlayerController>().playerCanMove = false;
                // Disable the item bar
                itemBar.SetActive(false);
                // Enable mouse clicking
                EventSystem.current.GetComponent<AltStandaloneInputModule>().mouseCanClick = true;
                // Draw Sprites into the inventory
                DisplayTheInventory();
                slotIndex = -1;
            }
            else
            {
                // Disable the inventory
                inventory.SetActive(false);
                // Let the player move
                player.GetComponent<PlayerController>().playerCanMove = true;
                // Show the item bar
                itemBar.SetActive(true);
                // Update the sprites on the item bar
                itemBar.GetComponent<PlayerInventoryBar>().DisplayMenuBar();
                // Disable mouse clicks from effecting UI
                EventSystem.current.GetComponent<AltStandaloneInputModule>().mouseCanClick = false;
                if(slotIndex != -1)
                {
                    inventoryScript.itemsStored[slotIndex] = inventoryScript.temporaryItemStored;
                    inventoryScript.temporaryItemStored = null;
                    itemDraggedByMouse.GetComponent<Image>().sprite = null;
                    itemDraggedByMouse.SetActive(false);
                }
            }
            inventoryDisplay = !inventoryDisplay;
        }
    }

    void DisplayTheInventory()
    {
        for(int i = 0; i < ItemDisplay.Length; i++)
        {
            if(inventoryScript.itemsStored[i] == null)
            {
                // If the item is not stored there, make sure the image is disabled
                ItemDisplay[i].GetComponentsInChildren<Image>(true)[1].gameObject.SetActive(false);
                // If there's no stored item, don't display text either
                ItemDisplay[i].GetComponentsInChildren<RectTransform>(true)[2].gameObject.SetActive(false);
            }
            else
            {
                // If the item is stored there, enable the image and set the sprite to it
                ItemDisplay[i].GetComponentsInChildren<Image>(true)[1].gameObject.SetActive(true);
                ItemDisplay[i].GetComponentsInChildren<Image>(true)[1].sprite = inventoryScript.itemsStored[i].storedSprite;
                // Set the text too, if the stack amount is more than 1
                if(player.GetComponent<PlayerInventoryManager>().itemsStored[i].stackAmount > 1)
                {
                    ItemDisplay[i].GetComponentsInChildren<RectTransform>(true)[2].gameObject.SetActive(true);
                    ItemDisplay[i].GetComponentsInChildren<RectTransform>(true)[2].gameObject.GetComponent<Text>().text = inventoryScript.itemsStored[i].stackAmount.ToString();
                }
                else
                {
                    ItemDisplay[i].GetComponentsInChildren<RectTransform>(true)[2].gameObject.SetActive(false);
                    ItemDisplay[i].GetComponentsInChildren<RectTransform>(true)[2].gameObject.GetComponent<Text>().text = " ";
                }
            }

        }
        // Set the slot index back to -1
        slotIndex = -1;
        // If the item being dragged isn't being dragged anymore hid and deactivate it
        if (inventoryScript.temporaryItemStored == null)
        {
            itemDraggedByMouse.GetComponent<Image>().sprite = null;
            itemDraggedByMouse.SetActive(false);
            itemDraggedByMouse.GetComponentsInChildren<RectTransform>(true)[1].gameObject.GetComponent<Text>().text = "";
        }
        else
        {
            // Display the mouses dragging item
            itemDraggedByMouse.GetComponent<Image>().sprite = inventoryScript.temporaryItemStored.storedSprite;
            itemDraggedByMouse.SetActive(true);
            // If the item is in a stack that is more than 1, display that number
            if (inventoryScript.temporaryItemStored.stackAmount > 1)
            {
                itemDraggedByMouse.GetComponentsInChildren<RectTransform>(true)[1].gameObject.SetActive(true);
                itemDraggedByMouse.GetComponentsInChildren<RectTransform>(true)[1].gameObject.GetComponent<Text>().text = inventoryScript.temporaryItemStored.stackAmount.ToString();
            }
            else
            {
                itemDraggedByMouse.GetComponentsInChildren<RectTransform>(true)[1].gameObject.SetActive(false);
                itemDraggedByMouse.GetComponentsInChildren<RectTransform>(true)[1].gameObject.GetComponent<Text>().text = " ";
            }
        }
    }

    public void UpdateTheUI()
    {
        DisplayTheInventory();
        itemBar.GetComponent<PlayerInventoryBar>().DisplayMenuItems();
    }

    void MoveItemsInInventory()
    {
        // Left click behavior if the inventory is active
        if (inventory.activeInHierarchy && Input.GetMouseButtonDown(0))
        {
            GameObject selectedInventorySlot;
            // If we don't select a game object and we aren't dragging anything break out of this function
            if (EventSystem.current.currentSelectedGameObject == null && inventoryScript.temporaryItemStored == null)
            {
                return;
            }
            selectedInventorySlot = EventSystem.current.currentSelectedGameObject;
            // Find the index of the slot that the inventory and store it to access the same slot in the item manager
            for (int i = 0; i < ItemDisplay.Length; i++)
            {
                if (ItemDisplay[i] == selectedInventorySlot)
                {
                    slotIndex = i;
                    break;
                }
            }
            // If the slot index hasn't changed break out of this function, because they aren't selecting an inventory slot
            if (slotIndex == -1)
            {
                return;
            }
            // Only trigger this if you aren't carrying an item, and the space you're selecting has an item
            else if (inventoryScript.temporaryItemStored == null && inventoryScript.itemsStored[slotIndex] != null)
            {
                // Set your currently dragged item to your mouses dragging item, and put the item into temporary storage
                inventoryScript.temporaryItemStored = inventoryScript.itemsStored[slotIndex];
                inventoryScript.itemsStored[slotIndex] = null;

            }

            // This puts items into empty places
            else if (inventoryScript.temporaryItemStored != null && inventoryScript.itemsStored[slotIndex] == null)
            {
                inventoryScript.itemsStored[slotIndex] = inventoryScript.temporaryItemStored;
                inventoryScript.temporaryItemStored = null;

            }

            // If the two items are the same, just add all the items you can to it, until it's at the stack amount
            else if (inventoryScript.temporaryItemStored != null && inventoryScript.temporaryItemStored == inventoryScript.itemsStored[slotIndex])
            {
                if(inventoryScript.temporaryItemStored.stackAmount + inventoryScript.itemsStored[slotIndex].stackAmount <= 99)
                {
                    inventoryScript.itemsStored[slotIndex].stackAmount += inventoryScript.temporaryItemStored.stackAmount;
                }
                // Figure how to add the amount that gets the stored stack to 99, but not past that, and sets the temp stack to the right amount
            }

            // This function swaps two items
            else if (inventoryScript.temporaryItemStored != null && inventoryScript.itemsStored[slotIndex] != null)
            {
                // Put the item in temporary storage into swapping storage
                inventoryScript.swappingStorage = inventoryScript.temporaryItemStored;
                // Make the temporary item into the item you're swapping with
                inventoryScript.temporaryItemStored = inventoryScript.itemsStored[slotIndex];
                // Make the item in that index the item you were storing prior
                inventoryScript.itemsStored[slotIndex] = inventoryScript.swappingStorage;
                // nullify the swapping storage
                inventoryScript.swappingStorage = null;
            }
            // No matter what happens, update the UI
            UpdateTheUI();
        }

        // How to handle right clicking
        else if(inventory.activeInHierarchy && Input.GetMouseButtonDown(1))
        {
            // TODO, make right clicking a stack of items split them in half, and right clicking a single item just pick it up
            // Right click should only have behavior if you're holding something
            if(inventoryScript.temporaryItemStored != null)
            {
                // TODO, Figure out how to get right clicking working. Look into Graphic Raycasts
                GameObject selectedInventorySlot;
                selectedInventorySlot = EventSystem.current.currentSelectedGameObject;
                PointerEventData cursor = new PointerEventData(EventSystem.current);
                cursor.position = Input.mousePosition;
                List<RaycastResult> objectsHit = new List<RaycastResult>();
                EventSystem.current.RaycastAll(cursor, objectsHit);
                if(objectsHit.Count == 0)
                {
                    // If there are no objects in the list, that means that the list is empty, and they didn't select any game objects.
                    // In this case objects should be dropped on the ground
                    return;
                }
                for(int i = 0; i < objectsHit.Count; i++)
                {
                    //If it's an inventory button set it to the object we want to select and break out of the loop
                    if(objectsHit[i].gameObject.GetComponent<Button>())
                    {
                        selectedInventorySlot = objectsHit[i].gameObject;
                        break;
                    }
                    else
                    {

                    }
                }
                // Find the index of the slot that the inventory and store it to access the same slot in the item manager
                for (int i = 0; i < ItemDisplay.Length; i++)
                {
                    if (ItemDisplay[i] == selectedInventorySlot)
                    {
                        slotIndex = i;
                        break;
                    }
                }
                // If the inventory slot is empty, place only one of the objects into that slot
                if(inventoryScript.itemsStored[slotIndex] == null)
                {
                    // Remove one from the item you're holding
                    inventoryScript.temporaryItemStored.stackAmount--;
                    inventoryScript.itemsStored[slotIndex] = inventoryScript.temporaryItemStored;
                    inventoryScript.itemsStored[slotIndex].stackAmount = 1;
                }
                else if(inventoryScript.itemsStored[slotIndex] == inventoryScript.temporaryItemStored)
                {
                    inventoryScript.itemsStored[slotIndex].stackAmount++;
                    inventoryScript.temporaryItemStored.stackAmount--;
                }
                // If there's nothing left in the temp storage set temp storage to null
                if (inventoryScript.temporaryItemStored.stackAmount <= 0)
                {
                    inventoryScript.temporaryItemStored = null;
                }
                UpdateTheUI();
            }
        }
    }

}
