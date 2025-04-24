using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_ChunkData
{
    public int width = 32;
    public int height = 32;
    public int depth = 32;
    public float[,,] densityMap;

    public MC_ChunkData(int width, int height, int depth)
    {
        this.width = width;
        this.height = height;
        this.depth = depth;
        densityMap = new float[width + 1, height + 1, depth + 1];
    }
}
