using StatePatteren.State;
using UnityEngine;

namespace StrategyPatteren.Role
{
    public class TankBehavior : IRoleBehavior
    {
        UnitController.UNIT_GROUP targetGroup;

        public void Action(UnitController unit)
        {
            GetTargetSystem getTarget = new GetTargetSystem();

            if (unit.GetUnitGroup() == UnitController.UNIT_GROUP.PLAYER)
            {
                targetGroup = UnitController.UNIT_GROUP.ENEMY;
            }

            if (unit.GetUnitGroup() == UnitController.UNIT_GROUP.ENEMY)
            {
                targetGroup = UnitController.UNIT_GROUP.PLAYER;
            }

            var target = getTarget.GetTarget(unit.gameObject, targetGroup)?.GetComponent<UnitController>();     // UŒ‚‘ÎÛ‚Ìæ“¾
            if (target != null)
            {
                Debug.Log($"AttackerFUŒ‚‘ÎÛF{target}");
                target.TakeDamage(unit.unitStats.atk);     // UŒ‚—Í•ªƒ_ƒ[ƒW‚ğ—^‚¦‚é
            }
        }
    }
}
