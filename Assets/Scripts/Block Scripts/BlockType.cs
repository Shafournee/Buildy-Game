using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockType : MonoBehaviour {

    // This is what the current progress in being destroyed is
    protected float destroyCounter = 0f;

    // This is the limit that tells you how much the block needs to be hit to be destroyed in seconds
    public float destroyLimit = 2f;

    //This is what the block drops when destroyed
    public GameObject dropBlock;

	// Use this for initialization
	protected virtual void Start () {
        // This adds counters that will destroy the block
        StartCoroutine(destroyCounterCheck());
	}
	
	// Update is called once per frame
	protected virtual void Update () {
        // This checks if the block should be destroyed yet
        Destroyer();
	}

    // Probably every left click will behave like this so it probably doesn't need to be overridden
    // May need to take into account tool types to see how much you destroy it each tick?
    public virtual void OnPlayerLeftClick()
    {
        destroyCounter += Time.deltaTime;
        print(destroyCounter);
    }

    // This is what destroys the block when you keep holding click on it
    protected virtual void Destroyer()
    {
        if(destroyCounter >= destroyLimit)
        {
            Instantiate(dropBlock, transform.GetComponent<Renderer>().bounds.center, transform.rotation);
            Destroy(gameObject);
        }
    }


    // This is what resets the timer on destroying the block
    protected virtual IEnumerator destroyCounterCheck()
    {
        while(true)
        {
            float counterLastFrame = destroyCounter;
            yield return null;

            if(counterLastFrame == destroyCounter)
            {
                destroyCounter = 0;
            }
        }
    }
}
