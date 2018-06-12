using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInventoryBar : MonoBehaviour {

    GameObject[] barSlots = new GameObject[9];

    GameObject lastSelect;

	// Use this for initialization
	void Start () {
        // Get each bar slot from the object
        for(int i = 0; i < 9; i++)
        {
            barSlots[i] = transform.GetChild(i).gameObject;
            lastSelect = new GameObject();
        }
	}
	
	// Update is called once per frame
	void Update () {

        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(lastSelect);
        }
        else
        {
            lastSelect = EventSystem.current.currentSelectedGameObject;
        }

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
}
