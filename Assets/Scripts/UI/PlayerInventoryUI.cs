using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryUI : MonoBehaviour {

    KeyCode inventoryKey = KeyCode.E;
    bool inventoryDisplay = true;

    [SerializeField] GameObject player;
    [SerializeField] GameObject inventory;
    [SerializeField] GameObject camera;
    [SerializeField] GameObject itemBar;
    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(inventoryKey))
        {
            if(inventoryDisplay)
            {
                inventory.SetActive(true);
                player.GetComponent<PlayerController>().playerCanMove = false;
                itemBar.SetActive(false);
            }
            else
            {
                inventory.SetActive(false);
                player.GetComponent<PlayerController>().playerCanMove = true;
                itemBar.SetActive(true);
            }
            inventoryDisplay = !inventoryDisplay;
        }
        Input.GetKeyDown(inventoryKey);

    }
}
