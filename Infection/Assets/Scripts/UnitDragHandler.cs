using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject unitPrefab;

    private GameObject dragPreviewObject;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // ドラッグ中に見せる仮オブジェクトを生成
        dragPreviewObject = Instantiate(unitPrefab);
        SetDragPreviewAlpha(dragPreviewObject, 0.5f); // 半透明に
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (dragPreviewObject != null)
        {
            Vector3 worldPos = cam.ScreenToWorldPoint(Input.mousePosition);
            worldPos.z = 0f;
            dragPreviewObject.transform.position = worldPos;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (dragPreviewObject != null)
        {
            Destroy(dragPreviewObject); // 仮オブジェクトは削除
        }

        Vector3 worldPos = cam.ScreenToWorldPoint(Input.mousePosition);
        worldPos.z = 0f;

        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);
        if (hit.collider != null && hit.collider.CompareTag("DropField"))
        {
            // 本物を配置
            Instantiate(unitPrefab, hit.collider.transform.position, Quaternion.identity);
        }
    }

    /// 半透明表示用：SpriteRendererの色を調整
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
