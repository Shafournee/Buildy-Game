using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockInfo{

    // This provides info about the chunk being generated
    // This is the tile info that's stored

    PickupPath blockType;

    public int x { get; private set; }
    public int y { get; private set; }
    public int z { get; private set; }

    // Block constructor
    public BlockInfo(int x, int y, int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        blockType = PickupPath.Stone;
    }
}
