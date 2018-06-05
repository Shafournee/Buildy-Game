using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Interact()
    {
        if(Input.GetMouseButton(0))
        {
            RaycastHit hit;
            bool objectWasHit = Physics.Raycast(transform.position, transform.forward, out hit, 2f);

            GameObject Block = hit.transform.gameObject;

            if(Block.GetComponent<BlockType>() != null)
            {
                Block.GetComponent<BlockType>().DestroyBlock();
            }

        }
    }
}
