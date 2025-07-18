using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace StatePatteren.State
{
    public class MoveState : UnitState
    {
        UnitController unitController;
        MoveSystem moveSystem;

        GameObject target;
        GameObject allyCastle;
        GameObject enemyCastle;

        float moveSpeed = 0f;
        Vector3 unitPos = Vector3.zero;

        public MoveState(UnitController unitController)
        {
            this.unitController = unitController;
        }

        public void Enter()
        {
            allyCastle = GameObject.Find("Ally_Castle").gameObject;
            enemyCastle = GameObject.Find("Enemy_Castle").gameObject;

            moveSpeed = unitController.unitStats.spd;
            moveSystem = new MoveSystem();
        }

        public void Update()
        {
            if(unitController.GetUnitGroup() == UnitController.UNIT_GROUP.PLAYER)
            {
                unitPos = moveSystem.Move(unitController.gameObject, "Enemy", moveSpeed);
                TargetInRange(enemyCastle.transform.position);
            }
            else if (unitController.GetUnitGroup() == UnitController.UNIT_GROUP.ENEMY)
            {
                unitPos = moveSystem.Move(unitController.gameObject, "Player", moveSpeed);
                TargetInRange(allyCastle.transform.position);
            }

            if (target != null)
            {
                TargetInRange(target.transform.position);
            }

            unitController.transform.position = unitPos;
            SetTarget();
        }

        public void Exit()
        {

        }

        public void Transition()
        {
            unitController.StateMachine.TransitionTo(unitController.StateMachine.combatState);
        }

        // çUåÇëŒè€ÇÃéÊìæ
        void SetTarget()
        {
            GetTargetSystem getTargetSystem = new GetTargetSystem();

            if (unitController.GetUnitGroup() == UnitController.UNIT_GROUP.PLAYER)
            {
                target = getTargetSystem.GetTarget(unitController.gameObject, "Enemy");
            }
            else if (unitController.GetUnitGroup() == UnitController.UNIT_GROUP.ENEMY)
            {
                target = getTargetSystem.GetTarget(unitController.gameObject, "Player");
            }
        }

        // ëŒè€Ç™çUåÇîÕàÕì‡ÇæÇ¡ÇΩÇÁèÛë‘ëJà⁄
        void TargetInRange(Vector3 target)
        {
            float dist = Vector3.Distance(unitController.transform.position, target);
            if (dist <= unitController.unitStats.range)
            {
                Transition();
            }
        }
    }
}