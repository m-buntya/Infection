using NUnit.Framework.Interfaces;
using StatePatteren.State;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MoveSystem
{
    // 移動先の対象の状態
    enum MOVE_TARGET
    {
        BRIDGE,
        CASTLE,
        UNIT,
    }

    MOVE_TARGET moveTarget;

    UnitController unit;
    GetTargetSystem targetSystem;

    Vector3 targetPos;
    Vector3 unitPos; // 最終的にユニットに適用する座標

    Vector3[][] bridgePoints; // 橋の入口・出口の座標
    Vector3 enterPoint;
    Vector3 exitPoint;

    Vector3 playerCastle;   // プレイヤーの城の座標
    Vector3 enemyCastle;    // エネミーの城の座標

    bool isBridgeEnter = false;  // 橋を渡り始めたか
    bool isBridgeExit = false;   // 橋を渡り切ったか

    public MoveSystem(GameObject unitObj)
    {
        targetSystem = new GetTargetSystem();
        moveTarget = MOVE_TARGET.BRIDGE;

        unit = unitObj.GetComponent<UnitController>();

        unitPos = unitObj.transform.position;

        bridgePoints = new Vector3[2][];
        bridgePoints[0] = new Vector3[2];   // 上の橋
        bridgePoints[1] = new Vector3[2];   // 下の橋

        bridgePoints[0][0] = GameObject.Find("BridgePoint_Up_L").transform.position;
        bridgePoints[0][1] = GameObject.Find("BridgePoint_Up_R").transform.position;
        bridgePoints[1][0] = GameObject.Find("BridgePoint_Down_L").transform.position;
        bridgePoints[1][1] = GameObject.Find("BridgePoint_Down_R").transform.position;
        FindNearBridge();

        playerCastle = GameObject.Find("Ally_Castle").transform.position;
        enemyCastle  = GameObject.Find("Enemy_Castle").transform.position;
    }

    public Vector3 Move(float moveSpeed)
    {
        if(HasGoToEnemy() == true)
        {
            moveTarget = MOVE_TARGET.UNIT;
        }
        else
        {
            if (isBridgeExit)
            {
                moveTarget = MOVE_TARGET.CASTLE;
            }
            else
            {
                moveTarget = MOVE_TARGET.BRIDGE;
            }
        }

        switch(moveTarget)
        {
            case MOVE_TARGET.BRIDGE:
                MoveToBridge();
                break;
            case MOVE_TARGET.CASTLE:
                MoveToCastle();
                break;
            case MOVE_TARGET.UNIT:
                MoveToEnemy();
                break;
        }

        unitPos += (targetPos - unitPos).normalized * moveSpeed * 0.1f * Time.deltaTime;
        return unitPos;
    }

    // 近くの橋を探す
    void FindNearBridge()
    {
        float minDistance = Vector3.Distance(unitPos, bridgePoints[0][0]);

        for (int i = 0; i < bridgePoints.Length; i++)
        {
            for(int j = 0; j < bridgePoints[i].Length; j++)
            {
                float dist = Vector3.Distance(unitPos, bridgePoints[i][j]);
                if (dist <= minDistance)
                {
                    minDistance = dist;
                    enterPoint = bridgePoints[i][j];
                    exitPoint = j == 0 ? bridgePoints[i][j + 1] : bridgePoints[i][j - 1];
                    Debug.Log($"enterPoint {enterPoint} / exitPoint {exitPoint}");
                }
            }
        }
    }

    // 敵に向かうかどうか
    bool HasGoToEnemy()
    {
        GameObject target = null;
        var unitForward = Vector2.zero;

        if (unit.GetUnitGroup() == UnitController.UNIT_GROUP.PLAYER)
        {
            target = targetSystem.GetTarget(unit.gameObject, UnitController.UNIT_GROUP.ENEMY);
            unitForward = -unit.gameObject.transform.right;
        }
        else
        {
            target = targetSystem.GetTarget(unit.gameObject, UnitController.UNIT_GROUP.PLAYER);
            unitForward = unit.gameObject.transform.right;
        }

        if (target == null) return false; // 敵がいなければ false を返す

        var enemyPos = target.transform.position;
        var targetDis = Vector2.Distance(unitPos, targetPos);
        var enemyDis = Vector2.Distance(unitPos, enemyPos);

        float dot = Vector2.Dot(unitForward, (enemyPos - unitPos).normalized);

        if (dot < 0 && isBridgeExit)
        {
            return false;   // 敵が橋を渡った後、後方にいた場合、falseを返す
        }
        else
        {
            return enemyDis <= targetDis;
        }
    }

    // 橋を渡る
    void MoveToBridge()
    {
        if (!isBridgeEnter)
        {
            targetPos = enterPoint;
            if (Vector3.Distance(unitPos, enterPoint) <= 0.1f)
            {
                isBridgeEnter = true;
            }
        }

        if (!isBridgeExit && isBridgeEnter)
        {
            targetPos = exitPoint;
            if (Vector3.Distance(unitPos, exitPoint) <= 0.1f)
            {
                isBridgeExit = true;
            }
        }
    }

    // 城に向かう
    void MoveToCastle()
    {
        if(unit.GetUnitGroup() == UnitController.UNIT_GROUP.PLAYER)
        {
            targetPos = enemyCastle;
        }
        else
        {
            targetPos = playerCastle;
        }
    }

    // 敵(それぞれの)に向かう
    void MoveToEnemy()
    {
        var enemyPos = Vector2.zero;

        if (unit.GetUnitGroup() == UnitController.UNIT_GROUP.PLAYER)
        {
            enemyPos = targetSystem.GetTarget(unit.gameObject, UnitController.UNIT_GROUP.ENEMY).transform.position;
        }
        else
        {
            enemyPos = targetSystem.GetTarget(unit.gameObject, UnitController.UNIT_GROUP.PLAYER).transform.position;
        }

        targetPos = enemyPos;
    }
}
