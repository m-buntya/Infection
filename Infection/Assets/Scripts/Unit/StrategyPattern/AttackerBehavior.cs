using StatePatteren.State;
using UnityEngine;

namespace StrategyPatteren.Role
{
    public class AttackerBehavior : IRoleBehavior
    {
        public void Action(UnitController unit)
        {
            GetTargetSystem getTarget = new GetTargetSystem();
            //var target = getTarget.GetTarget("UnitEnemy", unit.gameObject)?.GetComponent<EnemyController>();     // UΞΫΜζΎ
            //if (target != null)
            //{
            //    Debug.Log($"AttackerFUΞΫF{target}");
            //    target.TakeDamage(unit.unitStats.atk);     // UΝͺ_[Wπ^¦ι
            //}
        }
    }
}

