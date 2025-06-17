using StatePatteren.StateEnemy;
using StrategyPatteren.Role;
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
            atkSpd = squadController.unitStats.atkSpd;
            time = 0;
        }

        public void Update()
        {
            ActionTimer();
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

        // 行動速度
        void ActionTimer()
        {
            time += Time.deltaTime;

            if(time > atkSpd)
            {
                RoleAction(squadController.unitStats);
                time = 0;
            }
        }

        public void RoleAction(UnitStats stats)
        {
            IRoleBehavior behavior = RoleBehaviorFactory.Get(stats.role);
            behavior.Action(squadController);
        }
    }
}