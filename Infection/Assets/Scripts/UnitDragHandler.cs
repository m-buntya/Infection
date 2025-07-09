using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject unitPrefab;
    public Image dragIconPrefab;

    private Image currentIcon;
    private RectTransform iconRect;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        currentIcon = Instantiate(dragIconPrefab, transform.root);
        iconRect = currentIcon.GetComponent<RectTransform>();
        currentIcon.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (iconRect != null)
        {
            iconRect.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (currentIcon != null) Destroy(currentIcon.gameObject);

        Vector3 worldPos = cam.ScreenToWorldPoint(Input.mousePosition);
        worldPos.z = 0f;

        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);
        if (hit.collider != null && hit.collider.CompareTag("DropField"))
        {
            Instantiate(unitPrefab, hit.collider.transform.position, Quaternion.identity);
        }
    }
}
