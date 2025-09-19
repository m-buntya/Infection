using UnityEngine;

public class DraggableSprite : MonoBehaviour
{
    private SpawnDraggable spawner;
    private bool isDraggingWhileHolding = false;
    private bool isInsideDeleteZone = false;
    private bool hasDropped = false;

    private UnitInfection infectionTarget;

    public void BeginDragWhileHolding(SpawnDraggable source)
    {
        spawner = source;
        isDraggingWhileHolding = true;

        // 最も近い感染ターゲットを探す
        if (infectionTarget == null)
        {
            infectionTarget = FindClosestUnit(transform.position);
            if (infectionTarget != null)
            {
                Debug.Log($"感染ターゲットを取得しました → {infectionTarget.gameObject.name}");
            }
            else
            {
                Debug.LogWarning("感染ターゲットが見つかりませんでした");
            }
        }
    }

    void Update()
    {
        if (isDraggingWhileHolding && spawner != null && spawner.IsHolding())
        {
            FollowMouse();
        }
        else if (isDraggingWhileHolding && !spawner.IsHolding() && !hasDropped)
        {
            DropSprite();
        }
    }

    void FollowMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        transform.position = mousePos;
    }

    void DropSprite()
    {
        hasDropped = true;
        isDraggingWhileHolding = false;

        if (isInsideDeleteZone && infectionTarget != null)
        {
            infectionTarget.StartProgress();
            Debug.Log("削除ゾーン内でドロップ → 感染処理開始");
        }
        else
        {
            Debug.Log("削除ゾーン外でドロップ → スプライトを削除");
        }

        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DeleteZone"))
        {
            isInsideDeleteZone = true;
            Debug.Log("削除ゾーンに入りました");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (hasDropped) return;

        if (other.CompareTag("DeleteZone"))
        {
            isInsideDeleteZone = false;
            Debug.Log("削除ゾーンから出ました");
        }
    }

    UnitInfection FindClosestUnit(Vector3 fromPosition)
    {
        UnitInfection[] units = GameObject.FindObjectsOfType<UnitInfection>();
        UnitInfection closest = null;
        float minDistance = Mathf.Infinity;

        foreach (UnitInfection unit in units)
        {
            float dist = Vector3.Distance(fromPosition, unit.transform.position);
            if (dist < minDistance)
            {
                minDistance = dist;
                closest = unit;
            }
        }

        return closest;
    }
}