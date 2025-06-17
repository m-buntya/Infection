using StatePatteren.State;
using UnityEngine;

namespace StrategyPatteren.Role
{
    public class TankBehavior : IRoleBehavior
    {
        public void Action(UnitController unit)
        {
            var target = unit.GetComponent<UnitController>();     // ���g�̃R���|�[�l���g�擾
            if (target != null)
            {
                Debug.Log($"Tank�F���G��");
                target.Guard();     // ���g�𖳓G��
            }
        }
    }
}
