using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshVoxelizer : MonoBehaviour
{
    public GameObject voxelPrefab;
    public float voxelSize = 0.1f;
    public LayerMask targetLayer;

    [ContextMenu("Voxelize Mesh")]
    public void Voxelize()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer == null) return;
        meshRenderer.enabled = false;

        Bounds bounds = meshRenderer.bounds;
        Vector3 start = bounds.min;
        Vector3 end = bounds.max;

        for (float x = start.x; x < end.x; x += voxelSize)
        {
            for (float y = start.y; y < end.y; y += voxelSize)
            {
                for (float z = start.z; z < end.z; z += voxelSize)
                {
                    Vector3 point = new Vector3(x + voxelSize / 2, y + voxelSize / 2, z + voxelSize / 2);

                    // 中にあるかチェック（ボクセルサイズの小さなボックスで判定）
                    if (Physics.CheckBox(point, Vector3.one * voxelSize * 0.45f, Quaternion.identity, targetLayer))
                    {
                        Instantiate(voxelPrefab, point, Quaternion.identity, transform);
                    }
                }
            }
        }
    }
}

