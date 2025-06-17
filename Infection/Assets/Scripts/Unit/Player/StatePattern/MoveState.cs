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

            moveVector = new Vector2(-1, 0);     // �f�o�b�O�p
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
            // �f�o�b�O�p
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

        // �������ړ�
        void Move()
        {
            GameObject squad = squadController.gameObject;

                                                            // �ړ����x�␳(���̂܂܎g���Ƒ�������)
            Vector3 moveVelocity = moveVector * moveSpeed * 0.1f * Time.deltaTime;
            squad.transform.position = squad.transform.position + moveVelocity;
        }

        // �ړ������ɉ�]
        void Rotiation()
        {

        }
    }
}