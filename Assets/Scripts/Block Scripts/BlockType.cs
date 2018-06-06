using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockType : MonoBehaviour {

    // This is what the current progress in being destroyed is
    protected float destroyCounter = 0f;

    // This is the limit that tells you how much the block needs to be hit to be destroyed in seconds
    public float destroyLimit = 2f;

    MeshRenderer mesh;
    Material[] materials = new Material[3];

	// Use this for initialization
	protected virtual void Start () {
        StartCoroutine(destroyCounterCheck());
        mesh = GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	protected virtual void Update () {
        Destroyer();
	}

    // Probably every left click will behave like this so it probably doesn't need to be overridden
    // May need to take into account tool types to see how much you destroy it each tick?
    public virtual void OnPlayerLeftClick()
    {
        destroyCounter += Time.deltaTime;
        print(destroyCounter);
    }

    // This is what destroys the block when you keep clicking on it
    protected virtual void Destroyer()
    {
        if(destroyCounter >= destroyLimit)
        {
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
