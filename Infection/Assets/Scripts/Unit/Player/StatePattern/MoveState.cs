using UnityEngine;

namespace StatePatteren.State
{
    public class MoveState : SquadState
    {
        private SquadController squadController;

        float moveSpeed = 0f;
        float rotationSpeed = 0f;
        Vector2 moveVector = Vector2.zero;

        public MoveState(SquadController squadController)
        {
            this.squadController = squadController;
        }

        public void Enter()
        {
            moveSpeed = squadController.unitStats.spd;

            moveVector = new Vector2(-1, 0);     // デバッグ用
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
                    squadController.StateMachine.TransitionTo(squadController.StateMachine.combatState);
                }
                else if(Input.GetKeyDown(KeyCode.E))
                {
                    squadController.StateMachine.TransitionTo(squadController.StateMachine.deadState);
                }
            }
        }

        // 部隊を移動
        void Move()
        {
            GameObject squad = squadController.gameObject;

                                                            // 移動速度補正(そのまま使うと速すぎる)
            Vector3 moveVelocity = moveVector * moveSpeed * 0.1f * Time.deltaTime;
            squad.transform.position = squad.transform.position + moveVelocity;
        }

        // 移動方向に回転
        void Rotiation()
        {

        }
    }
}