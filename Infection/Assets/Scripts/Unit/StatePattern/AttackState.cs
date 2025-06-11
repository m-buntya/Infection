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
            // �f�o�b�O�p
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