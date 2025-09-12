using UnityEngine;

public class DraggableSprite : MonoBehaviour
{
    private SpawnDraggable spawner;
    private bool isDraggingWhileHolding = false;
    private bool isInsideDeleteZone = false;
    private bool hasDropped = false;

    public UnitInfection infectionTarget;

    public void BeginDragWhileHolding(SpawnDraggable source)
    {
        spawner = source;
        isDraggingWhileHolding = true;
    }

    void Update()
    {
        if (!hasDropped)
        {
            Debug.Log("infectionTarget = " + infectionTarget);
        }
        if (isDraggingWhileHolding && spawner != null && spawner.IsHolding())
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;
            transform.position = mousePos;
        }

        if (isDraggingWhileHolding && !spawner.IsHolding() && !hasDropped)
        {
            hasDropped = true;
            isDraggingWhileHolding = false;

            if (isInsideDeleteZone)
            {
                Debug.Log("削除ゾーン内でドロップ → 感染処理開始");
                if (infectionTarget != null)
                {
                    infectionTarget.StartProgress(); 
                }
                Destroy(gameObject);
            }
        
            else
            {
                Debug.Log("削除ゾーン外でドロップ → スプライトを削除");
            }

            Destroy(gameObject);
        }
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
        if (hasDropped) return; // すでに削除済みなら無視

        if (other.CompareTag("DeleteZone"))
        {
            isInsideDeleteZone = false;
            Debug.Log("削除ゾーンから出ました");
        }
    }
}