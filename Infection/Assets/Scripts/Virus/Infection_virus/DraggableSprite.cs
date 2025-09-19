using UnityEngine;

public class DraggableSprite : MonoBehaviour
{
    private SpawnDraggable spawner;
    private bool isDraggingWhileHolding = false;
    private bool isInsideDeleteZone = false;
    private bool hasDropped = false;

    UnitInfection infectionTarget;

    public void BeginDragWhileHolding(SpawnDraggable source)
    {
        spawner = source;
        isDraggingWhileHolding = true;

        // すでに設定されていなければ、シーン上から探す
        if (infectionTarget == null)
        {
            GameObject targetObj = GameObject.Find("virus(Clone)"); // ← シーン上のオブジェクト名に合わせて変更
            if (targetObj != null)
            {
                infectionTarget = targetObj.GetComponent<UnitInfection>();
                Debug.Log($"感染ターゲットをシーンから取得しました → {infectionTarget.gameObject.name}");
            }
            else
            {
                Debug.LogWarning("感染ターゲットがシーン上に見つかりませんでした");
            }
        }
    }

    void Update()
    {
        if (!hasDropped)
        {
            Debug.Log("infectionTarget(Clone) = " + infectionTarget);
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
                    Debug.Log("aaa");
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