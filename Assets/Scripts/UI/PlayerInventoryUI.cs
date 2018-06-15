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

    bool itemIsBeingDragged = false;

    // Use this for initialization
    void Start () {
        inventory.SetActive(false);
        player.GetComponent<PlayerController>().playerCanMove = true;
        itemBar.SetActive(true);
        itemBar.GetComponent<PlayerInventoryBar>().DisplayMenuBar();
        EventSystem.current.GetComponent<AltStandaloneInputModule>().mouseCanClick = false;
    }

    // Update is called once per frame
    void Update() {
        ToggleTheInventoryScreen();

        if(itemDraggedByMouse.activeInHierarchy)
        {
            itemDraggedByMouse.transform.position = Input.mousePosition;
        }
        PickupItemInInventory();
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
            }
            inventoryDisplay = !inventoryDisplay;
        }
    }

    void DisplayTheInventory()
    {
        for(int i = 0; i < ItemDisplay.Length; i++)
        {
            if(player.GetComponent<PlayerInventoryManager>().itemsStored[i] == null)
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
                ItemDisplay[i].GetComponentsInChildren<Image>(true)[1].sprite = player.GetComponent<PlayerInventoryManager>().itemsStored[i].storedSprite;
                // Set the text too, if the stack amount is more than 1
                if(player.GetComponent<PlayerInventoryManager>().itemsStored[i].stackAmount > 1)
                {
                    ItemDisplay[i].GetComponentsInChildren<RectTransform>(true)[2].gameObject.SetActive(true);
                    ItemDisplay[i].GetComponentsInChildren<RectTransform>(true)[2].gameObject.GetComponent<Text>().text = player.GetComponent<PlayerInventoryManager>().itemsStored[i].stackAmount.ToString();
                }
            }
        }
    }

    public void UpdateTheUI()
    {
        DisplayTheInventory();
        itemBar.GetComponent<PlayerInventoryBar>().DisplayMenuItems();
    }

    void PickupItemInInventory()
    {
        if(!itemIsBeingDragged)
        {
            if(Input.GetMouseButtonDown(0))
            {
                GameObject selectedInventorySlot;
                selectedInventorySlot = EventSystem.current.currentSelectedGameObject;
            }
        }
    }
}
