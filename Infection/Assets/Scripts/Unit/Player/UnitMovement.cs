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
        if (attackTarget == null) // ✅ ターゲットがない場合はランダム移動
        {
            timer += Time.deltaTime;
            if (timer >= changeDirectionInterval)
            {
                timer = 0f;
                ChangeDirection();
            }
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }
        else // ✅ 村へ向かって移動
        {
            Vector3 dir = (attackTarget.position - transform.position).normalized;
            transform.position += dir * moveSpeed * Time.deltaTime;

            if (Vector3.Distance(transform.position, attackTarget.position) < 1f)
            {
                StopMovement(); // ✅ 移動停止
                StartCoroutine(AttackVillage()); // ✅ 攻撃を続ける
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
    }

    public IEnumerator AttackVillage()
    {
        while (attackTarget != null) // ✅ 村が存在する限り攻撃を続ける
        {
            attackTarget.GetComponent<VillageController>()?.ReceiveDamage(unitData.attackPower);
            yield return new WaitForSeconds(unitData.attackInterval); // ✅ UnitData の攻撃間隔を適用
        }
    }
}