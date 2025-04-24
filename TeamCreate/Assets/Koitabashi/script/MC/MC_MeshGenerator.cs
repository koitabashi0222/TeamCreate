using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_MeshGenerator : MonoBehaviour
{
    public int width = 16;
    public int height = 16;
    public int depth = 16;
    public float surfaceLevel = 0.5f;
    public float noiseScale = 0.1f;

    private float[,,] densityMap;
    private MeshFilter meshFilter;

    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        GenerateDensityMap();
        GenerateMesh();
    }

    void GenerateDensityMap()
    {
        densityMap = new float[width + 1, height + 1, depth + 1];

        for (int x = 0; x <= width; x++)
        {
            for (int y = 0; y <= height; y++)
            {
                for (int z = 0; z <= depth; z++)
                {
                    float noiseValue = Mathf.PerlinNoise(x * noiseScale, z * noiseScale) - (float)y / height;
                    densityMap[x, y, z] = noiseValue;
                }
            }
        }
    }

    void GenerateMesh()
    {
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                for (int z = 0; z < depth; z++)
                {
                    Vector3 position = new Vector3(x, y, z);
                    float[] cube = new float[8];

                    for (int i = 0; i < 8; i++)
                    {
                        Vector3 corner = position + MyMarchingCubes.CornerTable[i];
                        cube[i] = densityMap[(int)corner.x, (int)corner.y, (int)corner.z];
                    }

                    MyMarchingCubes.Polygonise(position, cube, surfaceLevel, vertices, triangles);
                }
            }
        }

        Mesh mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();

        meshFilter.mesh = mesh;
    }
}
