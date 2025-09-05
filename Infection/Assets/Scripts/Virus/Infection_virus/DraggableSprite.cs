using UnityEngine;

public class DraggableSprite : MonoBehaviour
{
    private SpawnDraggable spawner;
    private bool isDraggingWhileHolding = false;

    public void BeginDragWhileHolding(SpawnDraggable source)
    {
        spawner = source;
        isDraggingWhileHolding = true;
    }

    void Update()
    {
        if (isDraggingWhileHolding && spawner != null && spawner.IsHolding())
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;
            transform.position = mousePos;
        }
    }
}