using UnityEngine;
using System.Collections;

public class UnitMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float changeDirectionInterval = 3f;
    public UnitData unitData; // ✅ ユニットのデータ
    private Vector3 moveDirection;
    private float timer = 0f;
    private Transform attackTarget;

    void Start()
    {
        ChangeDirection();
    }

    void Update()
    {
        if (attackTarget == null)
        {
            timer += Time.deltaTime;
            if (timer >= changeDirectionInterval)
            {
                timer = 0f;
                ChangeDirection();
            }

            transform.position += moveDirection * moveSpeed * Time.deltaTime;

            // 追加：画面の範囲内に制限
            Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
            viewPos.x = Mathf.Clamp01(viewPos.x);
            viewPos.y = Mathf.Clamp01(viewPos.y);
            viewPos.z = Mathf.Abs(Camera.main.WorldToViewportPoint(transform.position).z); // Z保持
            transform.position = Camera.main.ViewportToWorldPoint(viewPos);
        }
        else
        {
            // 村へ向かう処理はそのままでOK
            Vector3 dir = (attackTarget.position - transform.position).normalized;
            transform.position += dir * moveSpeed * Time.deltaTime;

            if (Vector3.Distance(transform.position, attackTarget.position) < 1f)
            {
                StopMovement();
                StartCoroutine(AttackVillage());
            }
        }
    }

    void ChangeDirection()
    {
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);
        moveDirection = new Vector3(randomX, randomY, 0f).normalized;
    }

    public void SetAttackTarget(Transform target)
    {
        attackTarget = target;
    }

    public void StopMovement()
    {
        moveSpeed = 0f; // ✅ ユニットの移動を止める
        StartCoroutine(AttackVillage());
    }

    IEnumerator AttackVillage()
    {
        VillageController village = attackTarget.GetComponent<VillageController>();
        if (village != null)
        {
            village.ReceiveDamage(unitData.attackPower);
        }
        while (attackTarget != null)
        {
            village.ReceiveDamage(unitData.attackPower); // ← 攻撃力2
            yield return new WaitForSeconds(unitData.attackInterval); // ← これがないと超連打
        }
    }
}