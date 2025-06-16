using StatePatteren.StateEnemy;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace StatePatteren.State
{
    public class CombatState : SquadState
    {
        SquadController squadController;
        EnemyController enemyController;

        float atkSpd = 0;

        float time = 0;

        public CombatState(SquadController squadController)
        {
            this.squadController = squadController;
        }

        public void Enter()
        {
            atkSpd = squadController.squadStats.leaderUnit.atkSpd;
            time = 0;
        }

        public void Update()
        {
          
            //�^�[�Q�b�g���˒��O�܂��͂��Ȃ��Ȃ�ړ��ɖ߂�
            if(squadController.currentTarget == null || !squadController.IsTargetInRange(squadController.currentTarget))
            {
                squadController.StateMachine.TransitionTo(squadController.StateMachine.moveState);
                return;
            }
             ActionTimer();
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

        // �s�����x
        void ActionTimer()
        {
            time += Time.deltaTime;

            if(time > atkSpd)
            {
                RoleAction(squadController.squadStats);
                time = 0;
            }
        }

        public void RoleAction(SquadStats stats)
        {
            switch (stats.leaderUnit.role)
            {
                case UnitStats.ROLE.Attacker:
                    Attack();
                    break;
                case UnitStats.ROLE.Healer:
                    Heal();
                    break;
                case UnitStats.ROLE.Tank:
                    Cover();
                    break;
            }
        }

        // �A�^�b�J�[
        void Attack()
        {
            GameObject targetObj = squadController.GetTarget("Enemy");
            if(targetObj == null)
            {
                Debug.LogError("�U���Ώۂ����܂���");
            }
            else
            {
                EnemyController target = targetObj.GetComponent<EnemyController>();
                Debug.Log($"�U���ΏہF{target}");
                target.TakeDamage(squadController.squadStats.leaderUnit.atk);
            }
        }

        // �q�[���[
        void Heal()
        {
            GameObject targetObj = squadController.GetTarget("Squad");
            if (targetObj == null)
            {
                Debug.LogError("�x���Ώۂ����܂���");
            }
            else
            {
                SquadController target = targetObj.GetComponent<SquadController>();
                Debug.Log($"�x���ΏہF{target}");
                target.CareHp(squadController.squadStats.leaderUnit.atk);
            }
        }

        // �^���N
        void Cover()
        {

        }
    }
}