using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public static class WaitEndDrag
{
    // ドラッグ終了まで待機
    public static async Task WaitDragEndAsync()
    {
        await UnitDragHandler.dragEndTcs.Task;
    }
}

public class UnitDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("配置するユニットプレハブ")]
    public GameObject unitPrefab;

    private GameObject dragPreviewObject;
    private Camera cam;

    // スナップする距離のしきい値
    private const float SNAP_THRESHOLD = 1.0f;

    public static TaskCompletionSource<PointerEventData> dragEndTcs;

    private void Start()
    {
        cam = Camera.main;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        dragEndTcs = new TaskCompletionSource<PointerEventData>();

        // 仮の表示用ユニットを生成（半透明）
        dragPreviewObject = Instantiate(unitPrefab);
        SetDragPreviewAlpha(dragPreviewObject, 0.5f);

        _ = WaitEndDrag.WaitDragEndAsync();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (dragPreviewObject == null) return;

        Vector3 worldPos = cam.ScreenToWorldPoint(Input.mousePosition);
        worldPos.z = 0f;

        // 最も近い DropField を探す
        GameObject[] dropFields = GameObject.FindGameObjectsWithTag("DropField");

        float minDist = float.MaxValue;
        Transform nearest = null;

        foreach (var field in dropFields)
        {
            float dist = Vector2.Distance(worldPos, field.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = field.transform;
            }
        }

        // 一定距離内ならスナップ、それ以外は通常追従
        if (nearest != null && minDist <= SNAP_THRESHOLD)
        {
            dragPreviewObject.transform.position = nearest.position;
        }
        else
        {
            dragPreviewObject.transform.position = worldPos;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (dragPreviewObject != null)
        {
            Destroy(dragPreviewObject); // 仮オブジェクト削除
        }

        Vector3 worldPos = cam.ScreenToWorldPoint(Input.mousePosition);
        worldPos.z = 0f;

        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);
        if (hit.collider != null && hit.collider.CompareTag("DropField"))
        {
            // 本物のユニットをマスに生成
            UnitFormation u = GameObject.Find("UnitFormation").GetComponent<UnitFormation>();
            u.UnitGenerate(gameObject, hit.collider.transform.position);
        }

        dragEndTcs?.TrySetResult(eventData);
    }

    // 半透明表示にする（仮オブジェクト用）
    private void SetDragPreviewAlpha(GameObject obj, float alpha)
    {
        foreach (var sr in obj.GetComponentsInChildren<SpriteRenderer>())
        {
            Color c = sr.color;
            c.a = alpha;
            sr.color = c;
        }
    }


}
