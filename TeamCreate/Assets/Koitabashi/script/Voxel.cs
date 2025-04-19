using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voxel : MonoBehaviour
{
    public int depth = 0;  // 現在の分割深さ
    public int maxDepth = 2;  // 最大分割深さ
    public GameObject smallerVoxelPrefab;  // 小さいボクセルのPrefab
    public Transform splitTarget;  // 分割をトリガーする対象（プレイヤー等）
    public float baseThreshold = 3f;  // 分割する距離の閾値

    private Renderer voxelRenderer;

    void Start()
    {
        voxelRenderer = GetComponent<Renderer>();  // ボクセルのレンダラーを取得
        UpdateColor();  // 最初の色を設定
    }

    void Update()
    {
        if (splitTarget == null) return;

        // 分割判定：分割対象に近づくと分割
        float distance = Vector3.Distance(splitTarget.position, transform.position);
        float threshold = baseThreshold / (depth + 1);

        if (depth < maxDepth && distance < threshold)
        {
            Subdivide();
        }
    }

    // ボクセルを分割する処理
    void Subdivide()
    {
        float newSize = transform.localScale.x / 2f;  // 新しいサイズ
        Vector3 center = transform.position;
        Destroy(gameObject);  // 現在のボクセルを削除

        // 8個の小さいボクセルを配置
        for (int x = -1; x <= 1; x += 2)
        {
            for (int y = -1; y <= 1; y += 2)
            {
                for (int z = -1; z <= 1; z += 2)
                {
                    Vector3 offset = new Vector3(x, y, z) * newSize / 2f;
                    GameObject child = Instantiate(smallerVoxelPrefab, center + offset, Quaternion.identity);
                    child.transform.localScale = Vector3.one * newSize;

                    Voxel v = child.GetComponent<Voxel>();
                    v.depth = depth + 1;
                    v.maxDepth = maxDepth;
                    v.smallerVoxelPrefab = this.smallerVoxelPrefab;
                    v.splitTarget = this.splitTarget;
                    v.baseThreshold = this.baseThreshold;

                    v.UpdateColor();  // 新しく作成したボクセルに色を設定
                }
            }
        }
    }

    // 分割の段階に応じて色を変更する処理
    void UpdateColor()
    {
        if (voxelRenderer == null) return;

        // 分割の深さに応じて色を変える
        Color newColor;
        switch (depth)
        {
            case 0:
                newColor = Color.red;  // 最初のボクセル（深さ0）は赤
                break;
            case 1:
                newColor = Color.green;  // 深さ1は緑
                break;
            case 2:
                newColor = Color.blue;  // 深さ2は青
                break;
            case 3:
                newColor = Color.yellow;  // 深さ3は黄色
                break;
            case 4:
                newColor = Color.cyan;  // 深さ4はシアン
                break;
            case 5:
                newColor = Color.magenta;  // 深さ5はマゼンタ
                break;
            default:
                newColor = Color.white;  // 深さがそれ以上は白
                break;
        }

        voxelRenderer.material.color = newColor;  // 色を設定
    }
}
