using StatePatteren.State;
using UnityEngine;

namespace StrategyPatteren.Role
{
    public class TankBehavior : IRoleBehavior
    {
        public void Action(SquadController squad)
        {
            var target = squad.GetComponent<SquadController>();
            if (target != null)
            {
                Debug.Log($"Tank�F���G��");
                target.Guard();
            }
        }
    }
}
