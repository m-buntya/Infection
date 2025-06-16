using StatePatteren.State;
using UnityEngine;

namespace StrategyPatteren.Role
{
    public interface IRoleBehavior
    {
        void Action(SquadController squad);
    }
}

