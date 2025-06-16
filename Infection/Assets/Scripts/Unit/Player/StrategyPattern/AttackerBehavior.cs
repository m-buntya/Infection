using StatePatteren.State;
using StatePatteren.StateEnemy;
using UnityEngine;

namespace StrategyPatteren.Role
{
    public class AttackerBehavior : IRoleBehavior
    {
        public void Action(SquadController squad)
        {
            GetTargetSystem getTarget = new GetTargetSystem();
            var target = getTarget.GetTarget("Enemy", squad.gameObject)?.GetComponent<EnemyController>();     // �U���Ώۂ̎擾
            if (target != null)
            {
                Debug.Log($"Attacker�F�U���ΏہF{target}");
                target.TakeDamage(squad.squadStats.leaderUnit.atk);     // �U���͕��_���[�W��^����
            }
        }
    }
}

