using StatePatteren.State;
using UnityEngine;

namespace StrategyPatteren.Role
{
    public class HealerBehavior : IRoleBehavior
    {
        public void Action(UnitController unit)
        {
            GetTargetSystem getTarget = new GetTargetSystem();
            var target = getTarget.GetTarget("Squad", unit.gameObject)?.GetComponent<UnitController>();     // xΞΫΜζΎ
            if(target != null)
            {
                Debug.Log($"HealerFxΞΫF{target}");
                target.CareHp(unit.unitStats.atk);     // UΝͺHPπρ³Ήι
            }
        }
    }
}
