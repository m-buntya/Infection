using StrategyPatteren.Role;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace StatePatteren.State
{
    public class CombatState : UnitState
    {
        UnitController unitController;
        GameObject target;
        GameObject allyCastle;
        GameObject enemyCastle;

        float atkSpd = 0;

        float time = 0;

        public CombatState(UnitController unitController)
        {
            this.unitController = unitController;
        }

        public void Enter()
        {
            allyCastle = GameObject.Find("Ally_Castle").gameObject;
            enemyCastle = GameObject.Find("Enemy_Castle").gameObject;

            atkSpd = unitController.unitStats.atkSpd;
            time = atkSpd;
        }

        public void Update()
        {
            ActionTimer();

            GetTargetSystem getTargetSystem = new GetTargetSystem();

            if (unitController.GetUnitGroup() == UnitController.UNIT_GROUP.PLAYER)
            {
                target = getTargetSystem.GetTarget(unitController.gameObject, "Enemy");
                CastleTarget(enemyCastle.transform.position);
            }
            else if (unitController.GetUnitGroup() == UnitController.UNIT_GROUP.ENEMY)
            {
                target = getTargetSystem.GetTarget(unitController.gameObject, "Player");
                CastleTarget(allyCastle.transform.position);
            }
        }

        public void Exit()
        {

        }

        public void Transition()
        {
            unitController.StateMachine.TransitionTo(unitController.StateMachine.moveState);            
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

        void CastleTarget(Vector3 targetCastle)
        {
            if (target == null)
            {
                float dist = Vector3.Distance(unitController.transform.position, targetCastle);
                if (dist > unitController.unitStats.range)
                {
                    Transition();
                }
            }
        }
    }
}