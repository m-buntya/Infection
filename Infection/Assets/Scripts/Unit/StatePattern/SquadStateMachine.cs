using Unity.VisualScripting;
using UnityEngine;

namespace StatePatteren.State
{
    [SerializeField]
    public class SquadStateMachine
    {
        public SquadState CurrenState { get; private set; }

        public MoveState moveState;
        public AttackState attackState;
        public DeadState deadState;
        
        public SquadStateMachine(SquadController squadController)
        {
           this.moveState = new MoveState(squadController);
           this.attackState = new AttackState(squadController);
           this.deadState = new DeadState(squadController);
        }

        public void Initialize(SquadState state)
        {
            CurrenState = state;
            state.Enter();
        }

        public void TransitionTo(SquadState nextState)
        {
            Debug.Log($"{CurrenState}Ç©ÇÁ{nextState}Ç÷à⁄çsÇµÇ‹Ç∑");

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
