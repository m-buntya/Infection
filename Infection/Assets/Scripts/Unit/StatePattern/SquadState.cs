using UnityEngine;

namespace StatePatteren.State
{
    public interface SquadState
    {
        public void Enter()
        {
            // ����State�Ɉڍs�����Ƃ��̏���
        }

        public void Update()
        {
            // ����State�̂Ƃ����t���[���Ăяo����鏈��
        }

        public void Exit()
        {
            // ����State�𔲂���Ƃ��̏���
        }

        // TransitionTo���Ăяo�����߂̏���
        public void Transition()
        {
            // ����State����ʂ�State�ֈڍs���鏈��
        }
    }
}
