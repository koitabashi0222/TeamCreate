using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class MC_Chunk : MonoBehaviour
{
    public MC_ChunkData chunkData;
    public int chunkSize = 32;

    public void Initialize(Vector3Int position)
    {
        chunkData = new MC_ChunkData(chunkSize, chunkSize, chunkSize);
        transform.position = position;
        GenerateDensity();  // テスト的に地形データを入れる
        GenerateMesh();
    }

    void GenerateDensity()
    {
        for (int x = 0; x <= chunkSize; x++)
            for (int y = 0; y <= chunkSize; y++)
                for (int z = 0; z <= chunkSize; z++)
                {
                    // 地形：y座標に応じたしきい値（地形の高さを決める）
                    float height = Mathf.PerlinNoise(
                        (transform.position.x + x) * 0.1f,
                        (transform.position.z + z) * 0.1f) * chunkSize;
                    chunkData.densityMap[x, y, z] = y < height ? 1f : 0f;
                }
    }

    void GenerateMesh()
    {
        Mesh mesh = MC_MeshGenerator.GenerateMesh(chunkData);
        GetComponent<MeshFilter>().mesh = mesh;
    }
}