using Unity.VisualScripting;
using UnityEngine;

namespace StatePatteren.State
{
    [SerializeField]
    public class SquadStateMachine
    {
        public SquadState CurrenState { get; private set; }

        public MoveState moveState;
        public CombatState combatState;
        public DeadState deadState;
        
        public SquadStateMachine(SquadController squadController)
        {
           this.moveState = new MoveState(squadController);
           this.combatState = new CombatState(squadController);
           this.deadState = new DeadState(squadController);
        }

        public void Initialize(SquadState state)
        {
            CurrenState = state;
            state.Enter();
        }

        public void TransitionTo(SquadState nextState)
        {
            Debug.Log($"{CurrenState}から{nextState}へ移行します");

            CurrenState.Exit();
            CurrenState = nextState;
            nextState.Enter();
        }

        public void Update()
        {
            CurrenState?.Update();
        }

        public void Transition()
        {
            CurrenState?.Transition();
        }
    }
}
