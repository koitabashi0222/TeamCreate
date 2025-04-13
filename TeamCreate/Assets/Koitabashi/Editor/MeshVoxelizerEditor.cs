using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(MeshVoxelizer))]
public class MeshVoxelizerEditor : Editor
{
    // Start is called before the first frame update
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MeshVoxelizer voxelizer = (MeshVoxelizer)target;
        if (GUILayout.Button("Voxelize Mesh"))
        {
            voxelizer.Voxelize();
        }
    }
}
