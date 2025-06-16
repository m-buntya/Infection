using System.Collections.Generic;
using UnityEngine;

namespace StrategyPatteren.Role
{
    public static class RoleBehaviorFactory
    {
        private static Dictionary<UnitStats.ROLE, IRoleBehavior> behaviors = new()
        {
            { UnitStats.ROLE.Attacker, new AttackerBehavior() },
            { UnitStats.ROLE.Healer, new HealerBehavior()     },
            { UnitStats.ROLE.Tank, new TankBehavior()         },
        };

        public static IRoleBehavior Get(UnitStats.ROLE role) => behaviors[role];
    }
}

