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

    [SerializeField] GameObject[] ItemDisplay;
    // Use this for initialization
    void Start () {
        inventory.SetActive(false);
        player.GetComponent<PlayerController>().playerCanMove = true;
        itemBar.SetActive(true);
        itemBar.GetComponent<PlayerInventoryBar>().DisplayMenuBar();
        EventSystem.current.GetComponent<AltStandaloneInputModule>().mouseCanClick = false;
    }

    // TODO, SHOW OBJECT STACKING. MAYBE SEPERATE ITEM COUNT FROM THE ITEMPICKUPS CLASS
    // Update is called once per frame
    void Update() {
        ToggleTheInventoryScreen();

    }

    void ToggleTheInventoryScreen()
    {
        if (Input.GetKeyDown(inventoryKey))
        {
            if (inventoryDisplay)
            {
                inventory.SetActive(true);
                player.GetComponent<PlayerController>().playerCanMove = false;
                itemBar.SetActive(false);
                EventSystem.current.GetComponent<AltStandaloneInputModule>().mouseCanClick = true;
                DisplayTheInventory();

            }
            else
            {
                inventory.SetActive(false);
                player.GetComponent<PlayerController>().playerCanMove = true;
                itemBar.SetActive(true);
                itemBar.GetComponent<PlayerInventoryBar>().DisplayMenuBar();
                EventSystem.current.GetComponent<AltStandaloneInputModule>().mouseCanClick = false;
            }
            inventoryDisplay = !inventoryDisplay;
        }
        Input.GetKeyDown(inventoryKey);
    }

    void DisplayTheInventory()
    {
        for(int i = 0; i < ItemDisplay.Length; i++)
        {
            if(player.GetComponent<PlayerInventoryManager>().itemsStored[i] == null)
            {
                // If the item is not stored there, make sure the image is disabled
                ItemDisplay[i].GetComponentsInChildren<Image>(true)[1].gameObject.SetActive(false);
            }
            else
            {
                // If the item is stored there, enable the image and set the sprite to it
                ItemDisplay[i].GetComponentsInChildren<Image>(true)[1].gameObject.SetActive(true);
                ItemDisplay[i].GetComponentsInChildren<Image>(true)[1].sprite = player.GetComponent<PlayerInventoryManager>().itemsStored[i].storedSprite;
            }
        }
    }

    public void UpdateTheUI()
    {
        DisplayTheInventory();
        itemBar.GetComponent<PlayerInventoryBar>().DisplayMenuItems();
    }
}
