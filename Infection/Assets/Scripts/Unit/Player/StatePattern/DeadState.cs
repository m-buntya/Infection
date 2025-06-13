using StatePatteren.State;
using Unity.VisualScripting;
using UnityEngine;

namespace StatePatteren.State
{
    public class DeadState : SquadState
    {
        private SquadController squadController;

        public DeadState(SquadController squadController)
        {
            this.squadController = squadController;
        }

        public void Enter()
        {
            squadController.Dead();
        }

        public void Update()
        {

        }

        public void Exit()
        {

        }

        public void Transition()
        {
            // �f�o�b�O�p
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    squadController.StateMachine.TransitionTo(squadController.StateMachine.combatState);
                }
                else if (Input.GetKeyDown(KeyCode.Q))
                {
                    squadController.StateMachine.TransitionTo(squadController.StateMachine.moveState);
                }
            }
        }
    }
}