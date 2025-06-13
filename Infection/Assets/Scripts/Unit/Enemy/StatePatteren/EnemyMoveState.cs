using StatePatteren.StateEnemy;
using UnityEngine;

namespace StatePatteren.StateEnemy
{
    public class EnemyMoveState : EnemyState
    {
        private EnemyController enemyController;

        float moveSpeed = 0f;
        float rotationSpeed = 0f;
        Vector2 moveVector = Vector2.zero;

        public EnemyMoveState(EnemyController enemyController)
        {
            this.enemyController = enemyController;
        }

        public void Enter()
        {

        }

        public void Update()
        {
            Move();
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
                    enemyController.EnemyStateMachine.TransitionTo(enemyController.EnemyStateMachine.combatState);
                }
                else if(Input.GetKeyDown(KeyCode.E))
                {
                    enemyController.EnemyStateMachine.TransitionTo(enemyController.EnemyStateMachine.deadState);
                }
            }
        }

        // 部隊を移動
        void Move()
        {
            
        }

        // 移動方向に回転
        void Rotiation()
        {

        }
    }
}