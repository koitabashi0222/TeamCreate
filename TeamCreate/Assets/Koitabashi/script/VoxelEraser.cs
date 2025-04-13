using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

public class VoxelEraser : MonoBehaviour
{
    public List<GameObject> collectedVoxels = new List<GameObject>(); // 保存リスト
    public Transform releasePoint; // 放出場所（右手の先とか）
    private GameObject voxelGroup; // 削った塊をまとめる親

    private void Start()
    {
        voxelGroup = new GameObject("CollectedVoxelGroup");
        voxelGroup.SetActive(false); // 最初は非表示
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("スペースキー押された！");
            ReleaseCollectedVoxels();
        }
    }

private void OnTriggerEnter(Collider other)
{
        if (other.CompareTag("Voxel") && other.gameObject.activeSelf)
        {
            collectedVoxels.Add(other.gameObject);

            // 一旦親を変えずに保持（位置計算のため）
            other.gameObject.SetActive(false);
        }
    }

    public void ReleaseCollectedVoxels()
    {
        if (collectedVoxels.Count == 0) return;

        // ?? ボクセルたちの重心を計算（ワールド座標）
        Vector3 center = Vector3.zero;
        foreach (var voxel in collectedVoxels)
        {
            center += voxel.transform.position;
        }
        center /= collectedVoxels.Count;

        // ?? voxelGroup を一度破棄して再生成（毎回新しく）
        if (voxelGroup != null)
        {
            Destroy(voxelGroup);
        }
        voxelGroup = new GameObject("CollectedVoxelGroup");
        voxelGroup.transform.position = center;

        // ?? 各ボクセルを voxelGroup の子にして、ローカル位置にする
        foreach (var voxel in collectedVoxels)
        {
            voxel.transform.SetParent(voxelGroup.transform);
            voxel.transform.localPosition = voxel.transform.position - center; // ローカル変換
            voxel.SetActive(true);
        }

        // ?? voxelGroup を放出地点に移動
        voxelGroup.transform.position = releasePoint.position;
        voxelGroup.SetActive(true);

        // ?? Rigidbodyで物理放出
        Rigidbody rb = voxelGroup.AddComponent<Rigidbody>();
        rb.mass = 1f;
        rb.velocity = Vector3.up + transform.forward * 2f;

        // ?? リストと状態をリセット
        collectedVoxels.Clear();
    }
}
