using UnityEngine;

namespace StatePatteren.State
{
    public class MoveState : UnitState
    {
        private UnitController unitController;
        MoveSystem moveSystem;

        float moveSpeed = 0f;
        float rotationSpeed = 5f;
        Vector3 moveVector = Vector3.zero;

        public MoveState(UnitController unitController)
        {
            this.unitController = unitController;
        }

        public void Enter()
        {
            moveSpeed = unitController.unitStats.spd;
            moveSystem = new MoveSystem();

        }

        public void Update()
        {
            moveSystem.Move(unitController.gameObject, "UnitEnemy", moveSpeed, moveVector);
        }

        public void Exit()
        {

        }

        public void Transition()
        {
            // デバッグ用
            if(Input.GetKey(KeyCode.LeftShift))
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    unitController.StateMachine.TransitionTo(unitController.StateMachine.combatState);
                }
                else if(Input.GetKeyDown(KeyCode.E))
                {
                    unitController.StateMachine.TransitionTo(unitController.StateMachine.deadState);
                }
            }
        }
    }
}