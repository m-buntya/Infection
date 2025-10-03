using StatePatteren.State;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace StrategyPatteren.Role
{
    public class AttackerBehavior : IRoleBehavior
    {
        UnitManager unitManager;
        UnitController.UNIT_GROUP targetGroup;

        CastleManager castleManager;
        GameObject targetCastle;

        GameObject playerCastleObj;
        GameObject enemyCastleObj;

        public void Action(UnitController unit)
        {
            castleManager = GameObject.Find("CastleManager").GetComponent<CastleManager>();
            playerCastleObj = GameObject.Find("Player_Castle");
            enemyCastleObj = GameObject.Find("Enemy_Castle");

            GetTargetSystem getTarget = new GetTargetSystem();

            if (unit.GetUnitGroup() == UnitController.UNIT_GROUP.PLAYER)
            {
                targetGroup = UnitController.UNIT_GROUP.ENEMY;
                targetCastle = enemyCastleObj;
            }

            if (unit.GetUnitGroup() == UnitController.UNIT_GROUP.ENEMY)
            {
                targetGroup = UnitController.UNIT_GROUP.PLAYER;
                targetCastle = playerCastleObj;
            }
            
            var target = getTarget.GetTarget(unit.gameObject, targetGroup)?.GetComponent<UnitController>();     // UŒ‚‘ÎÛ‚Ìæ“¾

            if (target != null)
            {
                var targetDis = Vector2.Distance(unit.gameObject.transform.position, target.gameObject.transform.position);
                var castleDis = Vector2.Distance(unit.gameObject.transform.position, targetCastle.transform.position);

                // é‚ª“G‚æ‚è‹ß‚¯‚ê‚Îé‚ğUŒ‚
                if (castleDis < targetDis)
                {
                    var castle = castleManager.GetCastle(targetCastle.gameObject);
                    castle.TakeDamage(unit.unitStats.atk);
                }
                else
                {
                    Debug.Log($"AttackerFUŒ‚‘ÎÛF{target}");
                    target.TakeDamage(unit.unitStats.atk);     // UŒ‚—Í•ªƒ_ƒ[ƒW‚ğ—^‚¦‚é
                }
            }
            else
            {
                var castle = castleManager.GetCastle(targetCastle.gameObject);
                castle.TakeDamage(unit.unitStats.atk);
            }
        }
    }
}

