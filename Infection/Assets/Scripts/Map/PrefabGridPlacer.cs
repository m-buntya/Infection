using UnityEngine;


/// 指定範囲内にPrefabを重ならないようにグリッド状に配置するマネージャー
/// 出典：ChatGPT生成（Unity 2D Grid 配置）

public class PrefabGridManager : MonoBehaviour
{
    [Header("配置するプレハブ")]
    public GameObject prefab;

    [Header("配置範囲（ワールド座標）※順不同でもOK")]
    public Vector2 areaMin;
    public Vector2 areaMax;

    [Header("Prefab間のスペース（余白）")]
    public Vector2 padding = Vector2.zero;

    // プレハブのサイズ
    private Vector2 prefabSize;


    /// 初期化時にPrefab配置を実行
    private void Start()
    {
        if (prefab == null)
        {
            Debug.LogError("プレハブが設定されていません。");
            return;
        }

        SpriteRenderer spriteRenderer = prefab.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            prefabSize = spriteRenderer.bounds.size;
        }
        else
        {
            Debug.LogWarning("SpriteRendererがPrefabに見つかりません。1x1で仮定します。");
            prefabSize = Vector2.one;
        }

        PlacePrefabs();
    }


    /// 範囲内にPrefabをグリッド状に配置する
    private void PlacePrefabs()
    {
        Vector2 min = new Vector2(Mathf.Min(areaMin.x, areaMax.x), Mathf.Min(areaMin.y, areaMax.y));
        Vector2 max = new Vector2(Mathf.Max(areaMin.x, areaMax.x), Mathf.Max(areaMin.y, areaMax.y));

        float stepX = prefabSize.x + padding.x;
        float stepY = prefabSize.y + padding.y;

        float width = max.x - min.x;
        float height = max.y - min.y;

        int countX = Mathf.FloorToInt(width / stepX);
        int countY = Mathf.FloorToInt(height / stepY);

        for (int y = 0; y < countY; y++)
        {
            for (int x = 0; x < countX; x++)
            {
                float posX = min.x + stepX * x + prefabSize.x / 2f;
                float posY = min.y + stepY * y + prefabSize.y / 2f;

                Vector3 spawnPos = new Vector3(posX, posY, 0f);
                GameObject instance = Instantiate(prefab, spawnPos, Quaternion.identity, this.transform);
                instance.name = $"{prefab.name}_{x}_{y}";
            }
        }
    }


    /// シーンビューに範囲を常に表示する
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Vector2 min = new Vector2(Mathf.Min(areaMin.x, areaMax.x), Mathf.Min(areaMin.y, areaMax.y));
        Vector2 max = new Vector2(Mathf.Max(areaMin.x, areaMax.x), Mathf.Max(areaMin.y, areaMax.y));

        Vector3 size = new Vector3(max.x - min.x, max.y - min.y, 0f);
        Vector3 center = (Vector3)(min + (max - min) / 2f);

        Gizmos.DrawWireCube(center, size);
    }
}
