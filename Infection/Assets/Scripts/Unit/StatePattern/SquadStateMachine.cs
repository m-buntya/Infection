using Unity.VisualScripting;
using UnityEngine;

namespace StatePatteren.State
{
    [SerializeField]
    public class SquadStateMachine
    {
        public UnitState CurrenState { get; private set; }

        public MoveState moveState;
        public CombatState combatState;
        public DeadState deadState;
        
        public SquadStateMachine(UnitController unitController)
        {
           this.moveState = new MoveState(unitController);
           this.combatState = new CombatState(unitController);
           this.deadState = new DeadState(unitController);
        }

        public void Initialize(UnitState state)
        {
            CurrenState = state;
            state.Enter();
        }

        public void TransitionTo(UnitState nextState)
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
