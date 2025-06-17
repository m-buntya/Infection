using StatePatteren.State;
using Unity.VisualScripting;
using UnityEngine;

namespace StatePatteren.State
{
    public class DeadState : UnitState
    {
        private UnitController unitController;

        public DeadState(UnitController unitController)
        {
            this.unitController = unitController;
        }

        public void Enter()
        {
            unitController.Dead();
        }

        public void Update()
        {

        }

        public void Exit()
        {

        }

        public void Transition()
        {
            // デバッグ用
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    unitController.StateMachine.TransitionTo(unitController.StateMachine.combatState);
                }
                else if (Input.GetKeyDown(KeyCode.Q))
                {
                    unitController.StateMachine.TransitionTo(unitController.StateMachine.moveState);
                }
            }
        }
    }
}