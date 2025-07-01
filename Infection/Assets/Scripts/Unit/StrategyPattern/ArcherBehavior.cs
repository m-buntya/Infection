using StatePatteren.State;
using UnityEngine;

namespace StrategyPatteren.Role
{
    public class ArcherBehavior : IRoleBehavior
    {
        private float projectileSpeed = 10f;

        public void Action(UnitController unit)
        {
            GetTargetSystem getTarget = new GetTargetSystem();
            string targetTag = unit.GetUnitGroup() == UnitController.UNIT_GROUP.PLAYER ? "Enemy" : "Player";

            var target = getTarget.GetTarget(unit.gameObject, targetTag);
            if (target == null) return;

            if (ProjectilePool.Instance == null)
            {
                Debug.LogWarning("ProjectilePoolのインスタンスがありません！");
                return;
            }

            GameObject projectile = ProjectilePool.Instance.GetProjectile();

            projectile.transform.position = unit.transform.position;
            projectile.transform.rotation = Quaternion.identity;

            Vector2 direction = (target.transform.position - unit.transform.position).normalized;

            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = direction * projectileSpeed;  // velocityが正しい
            }

            Projectile proj = projectile.GetComponent<Projectile>();
            if (proj != null)
            {
                proj.damage = (int)unit.unitStats.atk;
                // 発射者の所属グループをセットするため、
                proj.shooterGroup = unit.GetUnitGroup() == UnitController.UNIT_GROUP.PLAYER ? UnitGroup.Player : UnitGroup.Enemy;
            }
        }
    }
}
