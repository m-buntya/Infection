using StatePatteren.State;
using UnityEngine;

namespace StrategyPatteren.Role
{
    public class HealerBehavior : IRoleBehavior
    {
        public void Action(UnitController unit)
        {
            GetTargetSystem getTarget = new GetTargetSystem();
            var target = getTarget.GetTarget("Squad", unit.gameObject)?.GetComponent<UnitController>();     // �x���Ώۂ̎擾
            if(target != null)
            {
                Debug.Log($"Healer�F�x���ΏہF{target}");
                target.CareHp(unit.unitStats.atk);     // �U���͕�HP���񕜂�����
            }
        }
    }
}
