using StatePatteren.State;
using StatePatteren.StateEnemy;
using UnityEngine;

namespace StrategyPatteren.Role
{
    public class AttackerBehavior : IRoleBehavior
    {
        public void Action(SquadController squad)
        {
            var target = squad.GetTarget("Enemy")?.GetComponent<EnemyController>();
            if (target != null)
            {
                Debug.Log($"Attacker�F�U���ΏہF{target}");
                target.TakeDamage(squad.squadStats.leaderUnit.atk);
            }
        }
    }
}

