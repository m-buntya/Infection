using UnityEngine;
using System.Collections;
using StatePatteren.State; // UnitController の名前空間

public class UnitMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float changeDirectionInterval = 3f;

    private Vector3 moveDirection;
    private float timer = 0f;
    private Transform attackTarget;

    private UnitController unitController;

    void Start()
    {
        unitController = GetComponent<UnitController>();
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

            // 画面内に制限
            Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
            viewPos.x = Mathf.Clamp01(viewPos.x);
            viewPos.y = Mathf.Clamp01(viewPos.y);
            viewPos.z = Mathf.Abs(viewPos.z);
            transform.position = Camera.main.ViewportToWorldPoint(viewPos);
        }
        else
        {
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
        moveSpeed = 0f;
        StartCoroutine(AttackVillage());
    }

    IEnumerator AttackVillage()
    {
        VillageController village = attackTarget.GetComponent<VillageController>();
        if (village != null)
        {
            int power = Mathf.RoundToInt(unitController.unitStats.atk);         // ← attackPower の代わり
            float interval = unitController.unitStats.atkSpd;   // ← attackInterval の代わり

            village.ReceiveDamage(power);
            while (attackTarget != null)
            {
                village.ReceiveDamage(power);
                yield return new WaitForSeconds(interval);
            }
        }
    }

}
