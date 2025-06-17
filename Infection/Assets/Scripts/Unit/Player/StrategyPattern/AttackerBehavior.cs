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
            var target = getTarget.GetTarget("Enemy", squad.gameObject)?.GetComponent<EnemyController>();     // UŒ‚‘ÎÛ‚Ìæ“¾
            if (target != null)
            {
                Debug.Log($"AttackerFUŒ‚‘ÎÛF{target}");
                target.TakeDamage(squad.squadStats.leaderUnit.atk);     // UŒ‚—Í•ªƒ_ƒ[ƒW‚ğ—^‚¦‚é
            }
        }
    }
}

