using StatePatteren.State;
using UnityEngine;

namespace StrategyPatteren.Role
{
    public interface IRoleBehavior
    {
        // ���[�����̌ŗL�̋���
        void Action(SquadController squad);
    }
}

