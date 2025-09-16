using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnDraggable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject draggablePrefab;
    public RectTransform buttonRect;
    public UnitInfection infectionTarget;

    private bool isHolding = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        isHolding = true;

        var unitCost = draggablePrefab.GetComponent<UnitCost>();
        if (unitCost != null && !unitCost.TryConsumeCost())
        {
            isHolding = false;
            return;
        }

        Vector3 screenPos = buttonRect.position;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        worldPos.z = 0f;

        GameObject obj = Instantiate(draggablePrefab, worldPos, Quaternion.identity);
        var drag = obj.GetComponent<DraggableSprite>();
        drag.BeginDragWhileHolding(this);
        drag.infectionTarget = infectionTarget;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isHolding = false;
    }

    public bool IsHolding()
    {
        return isHolding;
    }
}