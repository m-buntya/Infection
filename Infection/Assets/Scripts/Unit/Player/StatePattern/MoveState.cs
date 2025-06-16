using UnityEngine;

namespace StatePatteren.State
{
    public class MoveState : SquadState
    {
        private SquadController squadController;

        //float moveSpeed = 0f;
        //float rotationSpeed = 0f;
        private float moveSpeed = 0f;
        private Vector2 moveVector = Vector2.zero;

        public MoveState(SquadController controller)
        {
            squadController = controller;
        }

        public void Enter()
        {
            moveSpeed = squadController.squadStats.leaderUnit.spd;

            //moveVector = new Vector2(-1, 0);     // デバッグ用

            //初期ターゲットは城に設定(タグで取得)
            GameObject castle = GameObject.FindWithTag("EnemyCastle");
            if (castle != null)
            {
                Vector3 direction = (castle.transform.position - squadController.transform.position).normalized;
                moveVector = new Vector2(direction.x, direction.y);
                squadController.currentTarget = castle;
            }
        }

        public void Update()
        {
            //敵ユニットが射程に入っているか確認
            GameObject unitTarget = squadController.GetTarget("Enemy");

            if (unitTarget != null && squadController.IsTargetInRange(unitTarget))
            {
                squadController.currentTarget = unitTarget;
                squadController.StateMachine.TransitionTo(squadController.StateMachine.combatState);
                return;
            }

            //敵城が射程に入っているか確認(敵ユニットがいない場合)
            if (squadController.currentTarget != null && squadController.IsTargetInRange(squadController.currentTarget))
            {
                squadController.StateMachine.TransitionTo(squadController.StateMachine.combatState);
                return;
            }
            Move();
        }

        public void Exit()
        {

        }

        public void Transition()
        {
            //// デバッグ用
            //if(Input.GetKey(KeyCode.LeftShift))
            //{
            //    if (Input.GetKeyDown(KeyCode.W))
            //    {
            //        squadController.StateMachine.TransitionTo(squadController.StateMachine.combatState);
            //    }
            //    else if(Input.GetKeyDown(KeyCode.E))
            //    {
            //        squadController.StateMachine.TransitionTo(squadController.StateMachine.deadState);
            //    }
            //}
        }

        // 部隊を移動
        void Move()
        {
            //GameObject squad = squadController.gameObject;

            //                                                // 移動速度補正(そのまま使うと速すぎる)
            //Vector3 moveVelocity = moveVector * moveSpeed * 0.1f * Time.deltaTime;
            //squad.transform.position = squad.transform.position + moveVelocity;
            squadController.transform.position += (Vector3)moveVector * moveSpeed * Time.deltaTime;
        }

        // 移動方向に回転
        void Rotiation()
        {

        }
    }
}