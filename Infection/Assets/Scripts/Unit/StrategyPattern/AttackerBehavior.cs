using StatePatteren.State;
using UnityEngine;

namespace StrategyPatteren.Role
{
    public class AttackerBehavior : IRoleBehavior
    {
        public void Action(UnitController unit)
        {
            GetTargetSystem getTarget = new GetTargetSystem();
            //var target = getTarget.GetTarget("UnitEnemy", unit.gameObject)?.GetComponent<EnemyController>();     // �U���Ώۂ̎擾
            //if (target != null)
            //{
            //    Debug.Log($"Attacker�F�U���ΏہF{target}");
            //    target.TakeDamage(unit.unitStats.atk);     // �U���͕��_���[�W��^����
            //}
        }
    }
}

