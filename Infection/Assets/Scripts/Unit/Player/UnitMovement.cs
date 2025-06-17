using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float changeDirectionInterval = 3f;
    [SerializeField] private Vector2 screenBounds; // 画面の境界（設定）

    private Vector3 moveDirection;
    private float timer = 0f;

    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        ChangeDirection();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= changeDirectionInterval)
        {
            timer = 0f;
            ChangeDirection();
        }

        transform.position += moveDirection * moveSpeed * Time.deltaTime;
        KeepUnitInsideScreen(); // ✅ 画面の外に出ないように制限
    }

    void ChangeDirection()
    {
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);
        moveDirection = new Vector3(randomX, randomY, 0f).normalized;
    }

    void KeepUnitInsideScreen()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -screenBounds.x, screenBounds.x);
        pos.y = Mathf.Clamp(pos.y, -screenBounds.y, screenBounds.y);
        transform.position = pos;
    }
}