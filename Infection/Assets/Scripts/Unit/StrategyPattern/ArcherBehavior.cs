using StatePatteren.State;
using UnityEngine;

namespace StrategyPatteren.Role
{
    public class ArcherBehavior : IRoleBehavior
    {
        UnitManager unitManager;

        public void Action(UnitController unit)
        {
            GetTargetSystem getTarget = new GetTargetSystem();
            var target = getTarget.GetTarget(unit.gameObject, "Enemy")?.GetComponent<UnitController>();     // UŒ‚‘ÎÛ‚Ìæ“¾
            if (target != null)
            {
                Debug.Log($"ArcherFUŒ‚‘ÎÛF{target}");
                target.TakeDamage(unit.unitStats.atk);     // UŒ‚—Í•ªƒ_ƒ[ƒW‚ğ—^‚¦‚é
            }
        }
    }
}
