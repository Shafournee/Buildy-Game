using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGenerator : MonoBehaviour
{

    public GameObject chunk;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                for (int k = 0; k < 2; k++)
                {
                    Instantiate(chunk, new Vector3(i * chunk.GetComponent<Chunk>().size, j * chunk.GetComponent<Chunk>().size, k * chunk.GetComponent<Chunk>().size), transform.rotation);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
