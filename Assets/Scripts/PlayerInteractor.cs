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
        Interact();
	}

    void Interact()
    {
        if(Input.GetMouseButton(0))
        {
            RaycastHit hit;
            bool objectWasHit = Physics.Raycast(transform.position, camera.transform.forward, out hit, 2f);

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
}
