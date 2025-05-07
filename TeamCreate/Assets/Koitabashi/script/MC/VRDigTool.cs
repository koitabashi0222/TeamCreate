using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VRDigTool : MonoBehaviour
{
    public VoxelDigManager digManager;
    public InputActionProperty triggerAction; // MetaXRÇ≈ÇÃÉgÉäÉKÅ[ì¸óÕ

    private List<Collider> overlappingColliders = new List<Collider>();
    public float digRadius = 2f;

    void OnTriggerEnter(Collider other)
    {
        if (!overlappingColliders.Contains(other))
            overlappingColliders.Add(other);
    }

    void OnTriggerExit(Collider other)
    {
        overlappingColliders.Remove(other);
    }

    void Update()
    {
        if (triggerAction.action.IsPressed())
        {
            foreach (var col in overlappingColliders)
            {
                digManager.DigAt(col.ClosestPoint(transform.position));
            }
        }
    }
}