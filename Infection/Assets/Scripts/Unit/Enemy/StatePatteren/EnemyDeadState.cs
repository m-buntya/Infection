using StatePatteren.StateEnemy;
using Unity.VisualScripting;
using UnityEngine;

namespace StatePatteren.StateEnemy
{
    public class EnemyDeadState : EnemyState
    {
        private EnemyController enemyController;

        public EnemyDeadState(EnemyController enemyController)
        {
            this.enemyController = enemyController;
        }

        public void Enter()
        {

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
                    enemyController.EnemyStateMachine.TransitionTo(enemyController.EnemyStateMachine.attackState);
                }
                else if (Input.GetKeyDown(KeyCode.Q))
                {
                    enemyController.EnemyStateMachine.TransitionTo(enemyController.EnemyStateMachine.moveState);
                }
            }
        }
    }
}