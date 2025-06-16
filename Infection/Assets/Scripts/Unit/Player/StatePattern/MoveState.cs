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

            //moveVector = new Vector2(-1, 0);     // �f�o�b�O�p

            //�����^�[�Q�b�g�͏�ɐݒ�(�^�O�Ŏ擾)
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
            //�G���j�b�g���˒��ɓ����Ă��邩�m�F
            GameObject unitTarget = squadController.GetTarget("Enemy");

            if (unitTarget != null && squadController.IsTargetInRange(unitTarget))
            {
                squadController.currentTarget = unitTarget;
                squadController.StateMachine.TransitionTo(squadController.StateMachine.combatState);
                return;
            }

            //�G�邪�˒��ɓ����Ă��邩�m�F(�G���j�b�g�����Ȃ��ꍇ)
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
            //// �f�o�b�O�p
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

        // �������ړ�
        void Move()
        {
            //GameObject squad = squadController.gameObject;

            //                                                // �ړ����x�␳(���̂܂܎g���Ƒ�������)
            //Vector3 moveVelocity = moveVector * moveSpeed * 0.1f * Time.deltaTime;
            //squad.transform.position = squad.transform.position + moveVelocity;
            squadController.transform.position += (Vector3)moveVector * moveSpeed * Time.deltaTime;
        }

        // �ړ������ɉ�]
        void Rotiation()
        {

        }
    }
}