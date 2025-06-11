using UnityEngine;

namespace StatePatteren.State
{
    public class AttackState : SquadState
    {
        private SquadController squadController;

        float atk = 0;
        float atkSpd = 0;

        float time = 0;

        public AttackState(SquadController squadController)
        {
            this.squadController = squadController;
        }

        public void Enter()
        {
            atk = squadController.squadFormation.squadStats.leaderUnit.atk;
            atkSpd = squadController.squadFormation.squadStats.leaderUnit.atkSpd;
            time = 0;
        }

        public void Update()
        {
            AttackTimer();
        }

        public void Exit()
        {

        }

        public void Transition()
        {
            // デバッグ用
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    squadController.StateMachine.TransitionTo(squadController.StateMachine.moveState);
                }
                else if (Input.GetKeyDown(KeyCode.E))
                {
                    squadController.StateMachine.TransitionTo(squadController.StateMachine.deadState);
                }
            }
        }

        // 攻撃
        void Attack()
        {
            Debug.Log($"敵に対して{atk}のダメージを与えた");
        }

        // 攻撃速度
        void AttackTimer()
        {
            time += Time.deltaTime;

            if(time > atkSpd)
            {
                Attack();

                time = 0;
            }
        }
    }
}