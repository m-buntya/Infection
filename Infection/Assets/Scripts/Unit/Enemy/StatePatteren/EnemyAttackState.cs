using StatePatteren.State;
using StatePatteren.StateEnemy;
using UnityEngine;

namespace StatePatteren.StateEnemy
{
    public class EnemyAttackState : EnemyState
    {
        private EnemyController enemyController;

        float atk = 0;
        float atkSpd = 0;

        float time = 0;

        public EnemyAttackState(EnemyController enemyController)
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
            // �f�o�b�O�p
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

        // �U��
        void Attack()
        {
            Debug.Log($"�G�ɑ΂���{atk}�̃_���[�W��^����");
        }

        // �U�����x
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