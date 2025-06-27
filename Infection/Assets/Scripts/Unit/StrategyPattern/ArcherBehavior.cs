using StatePatteren.State;
using UnityEngine;

namespace StrategyPatteren.Role
{
    public class ArcherBehavior : IRoleBehavior
    {
        public void Action(UnitController unit)
        {
            GetTargetSystem getTarget = new GetTargetSystem();
            //var target = getTarget.GetTarget("UnitEnemy", unit.gameObject)?.GetComponent<EnemyController>();     // UŒ‚‘ÎÛ‚Ìæ“¾
            //if (target != null)
            //{
            //    Debug.Log($"ArcherFUŒ‚‘ÎÛF{target}");
            //    target.TakeDamage(unit.unitStats.atk);     // UŒ‚—Í•ªƒ_ƒ[ƒW‚ğ—^‚¦‚é
            //}
        }
    }
}
