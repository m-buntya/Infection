using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace StatePatteren.State
{
    public class MoveState : UnitState
    {
        UnitController unitController;
        MoveSystem moveSystem;

        GameObject target;
        GameObject playerCastle;
        GameObject enemyCastle;

        Vector3 unitPos;

        public MoveState(UnitController unitController)
        {
            this.unitController = unitController;
            moveSystem = new MoveSystem(unitController.gameObject);
            unitPos = this.unitController.transform.position;
        }

        public void Enter()
        {
            playerCastle = GameObject.Find("Player_Castle").gameObject;
            enemyCastle = GameObject.Find("Enemy_Castle").gameObject;
        }

        public void Update()
        {
            if(unitController.GetUnitGroup() == UnitController.UNIT_GROUP.PLAYER)
            {
                unitPos = moveSystem.Move(unitController.unitStats.spd);
                TargetInRange(enemyCastle.transform.position);
            }
            else if (unitController.GetUnitGroup() == UnitController.UNIT_GROUP.ENEMY)
            {
                unitPos = moveSystem.Move(unitController.unitStats.spd);
                TargetInRange(playerCastle.transform.position);
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
                target = getTargetSystem.GetTarget(unitController.gameObject, UnitController.UNIT_GROUP.ENEMY);
            }
            else if (unitController.GetUnitGroup() == UnitController.UNIT_GROUP.ENEMY)
            {
                target = getTargetSystem.GetTarget(unitController.gameObject, UnitController.UNIT_GROUP.PLAYER);
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