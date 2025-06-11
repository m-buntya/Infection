using StatePatteren.State;
using UnityEngine;

namespace StatePatteren.StateEnemy
{
    public class EnemyStateMachine
    {
        public EnemyState CurrenState { get; private set; }

        public EnemyMoveState moveState;
        public EnemyAttackState attackState;
        public EnemyDeadState deadState;

        public EnemyStateMachine(EnemyController enemyController)
        {
            this.moveState = new EnemyMoveState(enemyController);
            this.attackState = new EnemyAttackState(enemyController);
            this.deadState = new EnemyDeadState(enemyController);
        }

        public void Initialize(EnemyState state)
        {
            CurrenState = state;
            state.Enter();
        }

        public void TransitionTo(EnemyState nextState)
        {
            Debug.Log($"{CurrenState}Ç©ÇÁ{nextState}Ç÷à⁄çsÇµÇ‹Ç∑");

            CurrenState.Exit();
            CurrenState = nextState;
            nextState.Enter();
        }

        public void Update()
        {
            CurrenState?.Update();
        }

        public void Transition()
        {
            CurrenState?.Transition();
        }
    }
}

