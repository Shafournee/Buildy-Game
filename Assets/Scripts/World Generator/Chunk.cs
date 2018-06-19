using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour {

    public BlockInfo[, ,] chunkBlocks;
    public int size = 16;

    public GameObject stone;

    // Use this for initialization
    private void Awake()
    {
        size = 16;
        chunkBlocks = new BlockInfo[size, size, size];

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                for (int k = 0; k < size; k++)
                {
                    // Construct a 3D array of blocks. This will tell where to instantiate blocks into the world
                    chunkBlocks[i, j, k] = new BlockInfo(i + (int)transform.position.x, j + (int)transform.position.y, k + (int)transform.position.z);
                    Vector3 blockPosition = new Vector3(chunkBlocks[i, j, k].x, chunkBlocks[i, j, k].y, chunkBlocks[i, j, k].z);
                    GameObject newBlock = Instantiate(stone, blockPosition, transform.rotation);
                    newBlock.transform.SetParent(transform);
                    newBlock.name = blockPosition.x + " " + blockPosition.y + " " + blockPosition.z;
                }
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
