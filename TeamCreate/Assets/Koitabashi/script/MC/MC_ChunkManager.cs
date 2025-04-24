using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_ChunkManager : MonoBehaviour
{
    public GameObject chunkPrefab;
    public int chunkCountX = 4;
    public int chunkCountY = 2;
    public int chunkCountZ = 4;
    public int chunkSize = 32;

    void Start()
    {
        for (int x = 0; x < chunkCountX; x++)
            for (int y = 0; y < chunkCountY; y++)
                for (int z = 0; z < chunkCountZ; z++)
                {
                    Vector3Int pos = new Vector3Int(x * chunkSize, y * chunkSize, z * chunkSize);
                    GameObject chunkObj = Instantiate(chunkPrefab, pos, Quaternion.identity, transform);
                    MC_Chunk chunk = chunkObj.GetComponent<MC_Chunk>();
                    chunk.chunkSize = chunkSize;
                    chunk.Initialize(pos);
                }
    }
}