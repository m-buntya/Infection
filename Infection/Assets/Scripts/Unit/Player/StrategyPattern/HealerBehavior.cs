using StatePatteren.State;
using UnityEngine;

namespace StrategyPatteren.Role
{
    public class HealerBehavior : IRoleBehavior
    {
        public void Action(SquadController squad)
        {
            var target = squad.GetTarget("Squad")?.GetComponent<SquadController>();
            if(target != null)
            {
                Debug.Log($"HealerFx‰‡‘ÎÛF{target}");
                target.CareHp(squad.squadStats.leaderUnit.atk);
            }
        }
    }
}
