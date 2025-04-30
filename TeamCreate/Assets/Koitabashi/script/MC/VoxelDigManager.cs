using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelDigManager : MonoBehaviour
{
    public float digRadius = 2f;
    public MC_ChunkManager chunkManager;

    public void DigAt(Vector3 worldPos)
    {
        Vector3Int centerCoord = chunkManager.GetChunkCoordFromWorldPos(worldPos);

        for (int dx = -1; dx <= 1; dx++)
            for (int dy = -1; dy <= 1; dy++)
                for (int dz = -1; dz <= 1; dz++)
                {
                    Vector3Int coord = centerCoord + new Vector3Int(dx, dy, dz);
                    MC_Chunk chunk = chunkManager.GetChunkAt(coord);
                    if (chunk != null)
                    {
                        chunk.ModifyDensity(worldPos, digRadius, 0f);
                        chunk.GenerateMesh();
                    }
                }
    }
}