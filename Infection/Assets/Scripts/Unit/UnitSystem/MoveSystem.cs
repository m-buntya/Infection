using StatePatteren.State;
using UnityEngine;

public class MoveSystem
{
    enum STEP
    {
        GO_BRIDGE,
        GO_CASTLE,
    }

    STEP step;
    bool toPoint = false;
    Vector3 myPos;

    float oldDist = 100.0f;
    Vector3 nearPos = Vector3.zero;
    Vector3 nextPos = Vector3.zero;

    Vector3[] point = new Vector3[4];
    Vector3 allyCastle = Vector3.zero;
    Vector3 enemyCastle = Vector3.zero;

    Vector3 targetDireciton = Vector3.zero;

    public MoveSystem()
    {
        step = STEP.GO_BRIDGE;

        point[0] = GameObject.Find("BridgePoint_Down_R").transform.position;
        point[1] = GameObject.Find("BridgePoint_Down_L").transform.position;
        point[2] = GameObject.Find("BridgePoint_Up_R").transform.position;
        point[3] = GameObject.Find("BridgePoint_Up_L").transform.position;

        allyCastle = GameObject.Find("Ally_Castle").transform.position;
        enemyCastle = GameObject.Find("Enemy_Castle").transform.position;
    }

    // 移動
    public Vector3 Move(GameObject myObj, string targetTag, float moveSpeed)
    {
        GetTargetSystem getTarget = new GetTargetSystem();
        GameObject target = getTarget.GetTarget(myObj, targetTag);
        myPos = myObj.transform.position;
        
        if (target == null)
        {
            switch (step)
            {
                case STEP.GO_BRIDGE:
                    targetDireciton = ToBridge(myObj);
                    break;
                case STEP.GO_CASTLE:
                    targetDireciton = ToCastle(myObj, targetTag);
                    break;
            }
        }
        else
        {
            targetDireciton = target.transform.position;
        }

        // 移動
        Vector3 moveVelocity = (targetDireciton - myPos).normalized * moveSpeed * 0.1f * Time.deltaTime;
        return myPos += moveVelocity;
    }

    // 橋まで移動
    Vector3 ToBridge(GameObject myObj)
    {
        // 上下の橋で近い方に向かう
        if (nearPos == Vector3.zero && nextPos == Vector3.zero)
        {
            foreach(var p in point)
            {
                var dist = Vector3.Distance(myPos, p);
                if (dist < oldDist)
                {
                    oldDist = dist;
                    nearPos = p;

                    foreach(var np in point)
                    {
                        if (nearPos.y == np.y && nearPos != np)
                        {
                            nextPos = np;
                        }
                    }
                }
            }
        }
                
        var pointDist = Vector3.Distance(myPos, nearPos);
        if(pointDist <= 0.1f || toPoint)
        {
            toPoint = true;

            var nextDist = Vector3.Distance(myPos, nextPos);

            // 橋を渡り切ったら城へ向かうステップに移行
            if (nextDist <= 0.1f)
            {
                step = STEP.GO_CASTLE;
            }

            return nextPos;
        }
        else
        {
            return nearPos;
        }
    }

    // 城まで移動
    Vector3 ToCastle(GameObject myObj, string targetTag)
    {
        return targetTag == "Enemy" ? enemyCastle : allyCastle;
    }
}
