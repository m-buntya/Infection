using UnityEngine;

namespace StatePatteren.State
{
    public class MoveState : UnitState
    {
        private UnitController unitController;
        MoveSystem moveSystem;

        float moveSpeed = 0f;
        Vector3 moveVector = Vector3.zero;

        public MoveState(UnitController unitController)
        {
            this.unitController = unitController;
        }

        public void Enter()
        {
            moveSpeed = unitController.unitStats.spd;
            moveSystem = new MoveSystem();
        }

        public void Update()
        {
            if(unitController.GetUnitGroup() == UnitController.UNIT_GROUP.PLAYER)
            {
                moveSystem.Move(unitController.gameObject, "Enemy", moveSpeed, moveVector);
            }
            if (unitController.GetUnitGroup() == UnitController.UNIT_GROUP.ENEMY)
            {
                moveSystem.Move(unitController.gameObject, "Player", moveSpeed, moveVector);
            }
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
                if(target != null)
                {
                    float distance = Vector2.Distance(unitController.gameObject.transform.position, target.transform.position);
                    if (distance <= unitController.unitStats.range)
                    {
                        unitController.StateMachine.TransitionTo(unitController.StateMachine.combatState);
                    }
                }
            }
            else
            {
                GameObject target = getTargetSystem.GetTarget(unitController.gameObject, "Player");
                if (target != null)
                {
                    float distance = Vector2.Distance(unitController.gameObject.transform.position, target.transform.position);
                    if (distance <= unitController.unitStats.range)
                    {
                        unitController.StateMachine.TransitionTo(unitController.StateMachine.combatState);
                    }
                }
            }
        }
    }
}