using StatePatteren.State;
using UnityEngine;

namespace StrategyPatteren.Role
{
    public class HealerBehavior : IRoleBehavior
    {
        public void Action(SquadController squad)
        {
            GetTargetSystem getTarget = new GetTargetSystem();
            var target = getTarget.GetTarget("Squad", squad.gameObject)?.GetComponent<SquadController>();     // �x���Ώۂ̎擾
            if(target != null)
            {
                Debug.Log($"Healer�F�x���ΏہF{target}");
                target.CareHp(squad.unitStats.atk);     // �U���͕�HP���񕜂�����
            }
        }
    }
}
