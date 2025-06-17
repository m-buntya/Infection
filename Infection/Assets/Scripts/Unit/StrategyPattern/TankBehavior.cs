using StatePatteren.State;
using UnityEngine;

namespace StrategyPatteren.Role
{
    public class TankBehavior : IRoleBehavior
    {
        public void Action(UnitController unit)
        {
            var target = unit.GetComponent<UnitController>();     // 自身のコンポーネント取得
            if (target != null)
            {
                Debug.Log($"Tank：無敵化");
                target.Guard();     // 自身を無敵化
            }
        }
    }
}
