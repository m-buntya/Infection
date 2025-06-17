using StatePatteren.State;
using UnityEngine;

namespace StrategyPatteren.Role
{
    public class HealerBehavior : IRoleBehavior
    {
        public void Action(UnitController unit)
        {
            GetTargetSystem getTarget = new GetTargetSystem();
            var target = getTarget.GetTarget("Squad", unit.gameObject)?.GetComponent<UnitController>();     // x‰‡‘ÎÛ‚Ìæ“¾
            if(target != null)
            {
                Debug.Log($"HealerFx‰‡‘ÎÛF{target}");
                target.CareHp(unit.unitStats.atk);     // UŒ‚—Í•ªHP‚ğ‰ñ•œ‚³‚¹‚é
            }
        }
    }
}
