using StrategyPatteren.Role;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace StatePatteren.State
{
    public class CombatState : UnitState
    {
        UnitController unitController;

        float atkSpd = 0;

        float time = 0;

        public CombatState(UnitController unitController)
        {
            this.unitController = unitController;
        }

        public void Enter()
        {
            atkSpd = unitController.unitStats.atkSpd;
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
                    unitController.StateMachine.TransitionTo(unitController.StateMachine.moveState);
                }
                else if (Input.GetKeyDown(KeyCode.E))
                {
                    unitController.StateMachine.TransitionTo(unitController.StateMachine.deadState);
                }
            }
        }

        // 行動速度
        void ActionTimer()
        {
            time += Time.deltaTime;

            if(time > atkSpd)
            {
                RoleAction(unitController.unitStats);
                time = 0;
            }
        }

        public void RoleAction(UnitStats stats)
        {
            IRoleBehavior behavior = RoleBehaviorFactory.Get(stats.role);
            behavior.Action(unitController);
        }
    }
}