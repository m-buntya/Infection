using StatePatteren.State;
using UnityEngine;

namespace StrategyPatteren.Role
{
    public class HealerBehavior : IRoleBehavior
    {
        UnitController.UNIT_GROUP targetGroup;

        public void Action(UnitController unit)
        {
            GetTargetSystem getTarget = new GetTargetSystem();

            if (unit.GetUnitGroup() == UnitController.UNIT_GROUP.PLAYER)
            {
                targetGroup = UnitController.UNIT_GROUP.PLAYER;
            }

            if (unit.GetUnitGroup() == UnitController.UNIT_GROUP.ENEMY)
            {
                targetGroup = UnitController.UNIT_GROUP.ENEMY;
            }

            var target = getTarget.GetTarget(unit.gameObject, targetGroup)?.GetComponent<UnitController>();     // x‰‡‘ÎÛ‚Ìæ“¾
            if(target != null && target != unit.gameObject)
            {
                Debug.Log($"HealerFx‰‡‘ÎÛF{target}");
                target.CareHp(unit.unitStats.atk);     // UŒ‚—Í•ªHP‚ğ‰ñ•œ‚³‚¹‚é
            }
        }
    }
}
