using StatePatteren.State;
using StatePatteren.StateEnemy;
using Unity.VisualScripting;
using UnityEngine;

namespace StatePatteren.StateEnemy
{
    public class EnemyCombatState : EnemyState
    {
        private EnemyController enemyController;

        float atk = 0;
        float atkSpd = 0;

        float time = 0;

        public EnemyCombatState(EnemyController enemyController)
        {
            this.enemyController = enemyController;
        }

        public void Enter()
        {
            atk = enemyController.enemyFormation.enemyStats.enemyUnit.atk;
            atkSpd = enemyController.enemyFormation.enemyStats.enemyUnit.atkSpd;
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
                    enemyController.EnemyStateMachine.TransitionTo(enemyController.EnemyStateMachine.moveState);
                }
                else if (Input.GetKeyDown(KeyCode.E))
                {
                    enemyController.EnemyStateMachine.TransitionTo(enemyController.EnemyStateMachine.deadState);
                }
            }
        }

        // 攻撃
        void Attack()
        {
            GetTargetSystem getTarget = new GetTargetSystem();
            var target = getTarget.GetTarget("Squad", enemyController.gameObject);
            SquadController squad = target.GetComponent<SquadController>();
            squad.TakeDamage(enemyController.enemyFormation.enemyStats.enemyUnit.atk);
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