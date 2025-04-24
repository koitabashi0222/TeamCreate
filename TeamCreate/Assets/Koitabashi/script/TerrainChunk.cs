using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class TerrainChunk : MonoBehaviour
{
    // Start is called before the first frame update
    public int size = 32;
    public float voxelScale = 1f;
    public float isoLevel = 0.0f;

    float[,,] density;

    void Start()
    {
        GenerateDensity();
        MarchingCubes.GenerateMesh(density, isoLevel, voxelScale, GetComponent<MeshFilter>());
    }

    void GenerateDensity()
    {
        density = new float[size + 1, size + 1, size + 1];

        for (int x = 0; x <= size; x++)
        {
            for (int y = 0; y <= size; y++)
            {
                for (int z = 0; z <= size; z++)
                {
                    float nx = x / (float)size;
                    float ny = y / (float)size;
                    float nz = z / (float)size;

                    float noise = PerlinNoise3D(nx * 4, ny * 4, nz * 4); // 3Dノイズ
                    float heightBias = ny * 1.2f; // Yが高いほどスカスカに
                    density[x, y, z] = noise - heightBias;
                }
            }
        }
    }

    float PerlinNoise3D(float x, float y, float z)
    {
        float xy = Mathf.PerlinNoise(x, y);
        float yz = Mathf.PerlinNoise(y, z);
        float xz = Mathf.PerlinNoise(x, z);
        float yx = Mathf.PerlinNoise(y, x);
        float zy = Mathf.PerlinNoise(z, y);
        float zx = Mathf.PerlinNoise(z, x);
        return (xy + yz + xz + yx + zy + zx) / 6f;
    }
}
