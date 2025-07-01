using StatePatteren.State;
using UnityEngine;

public enum UnitGroup
{
    Player,
    Enemy,
}

public class Projectile : MonoBehaviour
{
    public int damage;

    // ���ˎ҂̏����O���[�v��ێ��i�Z�b�g�K�{�j
    public UnitGroup shooterGroup;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // UnitManager��T��
        UnitManager unitManager = GameObject.FindObjectOfType<UnitManager>();
        if (unitManager == null)
        {
            Debug.LogWarning("UnitManager��������܂���");
            return;
        }

        GameObject otherObj = other.gameObject;

        bool isEnemy = false;

        // ���ˎ҂̃O���[�v����G���ǂ�������
        if (shooterGroup == UnitGroup.Player)
        {
            isEnemy = unitManager.GetEnemyUnits().Contains(otherObj);
        }
        else if (shooterGroup == UnitGroup.Enemy)
        {
            isEnemy = unitManager.GetPlayerUnits().Contains(otherObj);
        }

        if (isEnemy)
        {
            var unit = other.GetComponent<UnitController>();
            if (unit != null)
            {
                unit.TakeDamage(damage);
            }

            if (ProjectilePool.Instance != null)
            {
                ProjectilePool.Instance.ReturnProjectile(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnEnable()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }
    }
}
