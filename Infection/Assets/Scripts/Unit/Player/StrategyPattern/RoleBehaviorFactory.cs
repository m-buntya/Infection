using System.Collections.Generic;
using UnityEngine;

namespace StrategyPatteren.Role
{
    // インスタンス化して使用する(オブジェクトにアタッチしなくてよい)
    public static class RoleBehaviorFactory
    {
        // 各ロール毎の挙動を保存
        private static Dictionary<UnitStats.ROLE, IRoleBehavior> behaviors = new()      // ロール追加時にここにも追加する
        {
            { UnitStats.ROLE.Attacker, new AttackerBehavior() },
            { UnitStats.ROLE.Healer, new HealerBehavior()     },
            { UnitStats.ROLE.Tank, new TankBehavior()         },
        };

        // ロールを渡して呼び出すだけで適応した行動を行う
        public static IRoleBehavior Get(UnitStats.ROLE role) => behaviors[role];
    }
}

