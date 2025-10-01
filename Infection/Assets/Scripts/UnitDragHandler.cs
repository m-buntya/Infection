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
    [SerializeField] CostManager costManager;
    UnitGenerater unitGenerator;

    [Header("配置するユニットプレハブ")]
    public GameObject unitPrefab;

    [Header("禁止エリアのLayer")]
    [SerializeField] private LayerMask blockAreaLayer;

    // グレーアウト用UIオブジェクト
    [SerializeField] GameObject GrayOutObj;

    private GameObject dragPreviewObject;
    private Camera cam;

    private const float SNAP_THRESHOLD = 1.0f;

    public static TaskCompletionSource<PointerEventData> dragEndTcs;

    bool isDrag = false;

    // 最後に合法だった位置を記録
    private Vector3? lastValidPosition = null;

    private void Start()
    {
        cam = Camera.main;
        isDrag = false;
        unitGenerator = GameObject.Find("UnitGenerater").GetComponent<UnitGenerater>(); 
    }

    void Update()
    {
        CostCheck();
    }

    // コストが足りているか
    public void CostCheck()
    {
        if (unitGenerator.GetStats(gameObject).cost <= 0)
        {
            isDrag = false;
        }
        else if (costManager.CanAfford(unitGenerator.GetStats(gameObject).cost))
        {
            isDrag = true;
        }
        else
        {
            isDrag = false;
        }

        GrayOut();
    }

    // グレーアウト処理
    void GrayOut()
    {
        if(isDrag)
        {
            GrayOutObj.SetActive(false);
        }
        else
        {
            GrayOutObj.SetActive(true);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!isDrag) return;

        dragEndTcs = new TaskCompletionSource<PointerEventData>();

        dragPreviewObject = Instantiate(unitPrefab);
        SetDragPreviewAlpha(dragPreviewObject, 0.5f);
        lastValidPosition = null;

        _ = WaitEndDrag.WaitDragEndAsync();

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (dragPreviewObject == null) return;

        Vector3 worldPos = cam.ScreenToWorldPoint(Input.mousePosition);
        worldPos.z = 0f;

        // 禁止エリアにマウスがあるとき
        if (IsPointerOverBlockedArea(worldPos))
        {
            if (lastValidPosition.HasValue)
            {
                // 最後の合法マスに表示
                dragPreviewObject.SetActive(true);
                dragPreviewObject.transform.position = lastValidPosition.Value;
            }
            else
            {
                // 合法マスがまだ見つかってない → 非表示
                dragPreviewObject.SetActive(false);
            }

            return;
        }

        // 合法な位置 → 仮ユニットをスナップまたは追従
        dragPreviewObject.SetActive(true);

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

        if (nearest != null && minDist <= SNAP_THRESHOLD)
        {
            dragPreviewObject.transform.position = nearest.position;
            lastValidPosition = nearest.position;
        }
        else
        {
            dragPreviewObject.transform.position = worldPos;
            lastValidPosition = null; // 無効にしておく
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //UnitCost unitCost = dragPreviewObject?.GetComponent<UnitCost>();

        //Debug.Log($"現在のコスト: {CostManager.Instance?.GetCurrentCost()}");
        //Debug.Log($"UnitCost: {unitCost}");

        //if (unitCost != null && !unitCost.TryConsumeCost())
        //{
        //    dragEndTcs?.TrySetResult(eventData);
        //    Destroy(dragPreviewObject);
        //    return; // コスト不足 → 配置キャンセル
        //}
        if (!isDrag) return;
        if (dragPreviewObject != null)
        {
            Destroy(dragPreviewObject);
        }

        UnitGenerater ug = GameObject.Find("UnitGenerater").GetComponent<UnitGenerater>();

        Vector3 worldPos = cam.ScreenToWorldPoint(Input.mousePosition);
        worldPos.z = 0f;

        if (IsPointerOverBlockedArea(worldPos))
        {
            if (lastValidPosition.HasValue)
            {
                ug.UnitGenerate(gameObject, lastValidPosition.Value);
            }
            dragEndTcs?.TrySetResult(eventData);
            return;
        }
        
        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);
        if (hit.collider != null && hit.collider.CompareTag("DropField"))
        {
            ug.UnitGenerate(gameObject, hit.collider.transform.position);
        }
        else if (lastValidPosition.HasValue)
        {
            ug.UnitGenerate(gameObject, lastValidPosition.Value);
        }

        dragEndTcs?.TrySetResult(eventData);
    }

    private bool IsPointerOverBlockedArea(Vector3 worldPos)
    {
        return Physics2D.OverlapPoint(worldPos, blockAreaLayer) != null;
    }

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
