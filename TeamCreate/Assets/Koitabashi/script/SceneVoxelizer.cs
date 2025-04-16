using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneVoxelizer : MonoBehaviour
{
    void Start()
    {
        MeshVoxelizer[] voxelizers = FindObjectsOfType<MeshVoxelizer>();
        foreach (var voxelizer in voxelizers)
        {
            voxelizer.Voxelize(); // Voxelizeメソッドがpublicである必要あり
        }
    }
}