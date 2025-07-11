using UnityEngine;

/// マウスが進入できないブロックエリア
[RequireComponent(typeof(BoxCollider2D))]
public class MouseBlockArea : MonoBehaviour
{
    private void Reset()
    {
        // 自動的にトリガーON
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnDrawGizmos()
    {
        // シーン上で分かりやすく表示
        Gizmos.color = new Color(1f, 0f, 0f, 0.3f);
        if (TryGetComponent(out Collider2D col))
        {
            Gizmos.DrawCube(col.bounds.center, col.bounds.size);
        }
    }
}
