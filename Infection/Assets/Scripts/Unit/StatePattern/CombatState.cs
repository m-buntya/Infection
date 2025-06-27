using StrategyPatteren.Role;
using UnityEngine;

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
            GetTargetSystem getTargetSystem = new GetTargetSystem();

            if (unitController.GetUnitGroup() == UnitController.UNIT_GROUP.PLAYER)
            {
                GameObject target = getTargetSystem.GetTarget(unitController.gameObject, "Enemy");
                if (target == null)
                {
                    unitController.StateMachine.TransitionTo(unitController.StateMachine.moveState);
                }
            }
            if (unitController.GetUnitGroup() == UnitController.UNIT_GROUP.ENEMY)
            {
                GameObject target = getTargetSystem.GetTarget(unitController.gameObject, "Player");
                if (target == null)
                {
                    unitController.StateMachine.TransitionTo(unitController.StateMachine.moveState);
                }
            }
        }

        // s“®‘¬“x
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