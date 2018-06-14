using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerInventoryBar : MonoBehaviour {

    GameObject[] barSlots = new GameObject[9];

    GameObject lastBarSlotSelected;

    GameObject lastSelect;

    [SerializeField] GameObject player;

	// Use this for initialization
	void Start () {
        // Get each bar slot from the object
        for(int i = 0; i < 9; i++)
        {
            barSlots[i] = transform.GetChild(i).gameObject;
            
        }
        if (lastBarSlotSelected == null)
        {
            EventSystem.current.SetSelectedGameObject(barSlots[0]);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(lastBarSlotSelected);
        }
        lastSelect = new GameObject();
    }
	
	// Update is called once per frame
	void Update () {



        lastBarSlotSelected = EventSystem.current.currentSelectedGameObject;

        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(lastSelect);
        }
        else
        {
            lastSelect = EventSystem.current.currentSelectedGameObject;
        }

        changeInventorySlotWithNumberKey();
        changeInventorySlotWithScrollWheel();
    }

    void changeInventorySlotWithNumberKey()
    {
        // Change the selected slot to the corresponding number key
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EventSystem.current.SetSelectedGameObject(barSlots[0]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EventSystem.current.SetSelectedGameObject(barSlots[1]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            EventSystem.current.SetSelectedGameObject(barSlots[2]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            EventSystem.current.SetSelectedGameObject(barSlots[3]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            EventSystem.current.SetSelectedGameObject(barSlots[4]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            EventSystem.current.SetSelectedGameObject(barSlots[5]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            EventSystem.current.SetSelectedGameObject(barSlots[6]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            EventSystem.current.SetSelectedGameObject(barSlots[7]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            EventSystem.current.SetSelectedGameObject(barSlots[8]);
        }
    }

    void changeInventorySlotWithScrollWheel()
    {
        // If you scroll up, check if you're at the last slot. If so, set it to one. Else, find which slot you are and set it to
        // One plus of your current slot
        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if(EventSystem.current.currentSelectedGameObject == barSlots[8])
            {
                EventSystem.current.SetSelectedGameObject(barSlots[0]);
            }
            else
            {
                for (int i = 0; i < 8; i++)
                {
                    if (EventSystem.current.currentSelectedGameObject == barSlots[i])
                    {
                        EventSystem.current.SetSelectedGameObject(barSlots[i + 1]);
                        break;
                    }
                }
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if(EventSystem.current.currentSelectedGameObject == barSlots[0])
            {
                EventSystem.current.SetSelectedGameObject(barSlots[8]);
            }
            else
            {
                for (int i = 1; i < 9; i++)
                {
                    if (EventSystem.current.currentSelectedGameObject == barSlots[i])
                    {
                        EventSystem.current.SetSelectedGameObject(barSlots[i - 1]);
                        break;
                    }
                }
            }


            
        }
    }

    public void DisplayMenuBar()
    {
        StartCoroutine(UpdateSelectedMenuBarSlot());

    }

    public void DisplayMenuItems()
    {
        for (int i = 0; i < barSlots.Length; i++)
        {
            if (player.GetComponent<PlayerInventoryManager>().itemsStored[i] == null)
            {
                // If the item is not stored there, make sure the image is disabled
                barSlots[i].GetComponentsInChildren<Image>(true)[1].gameObject.SetActive(false);
            }
            else
            {
                // If the item is stored there, enable the image and set the sprite to it
                barSlots[i].GetComponentsInChildren<Image>(true)[1].gameObject.SetActive(true);
                barSlots[i].GetComponentsInChildren<Image>(true)[1].sprite = player.GetComponent<PlayerInventoryManager>().itemsStored[i].storedSprite;
            }
        }
    }

    IEnumerator UpdateSelectedMenuBarSlot()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return null;
        EventSystem.current.SetSelectedGameObject(lastSelect);
        DisplayMenuItems();
    }
}
