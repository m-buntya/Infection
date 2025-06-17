using StatePatteren.State;
using UnityEngine;

public class MoveSystem
{
    // 移動
    public void Move(GameObject myObj, string targetTag, float moveSpeed, Vector3 vector)
    {
        GetTargetSystem getTarget = new GetTargetSystem();
        //GameObject target = getTarget.GetTarget(targetTag, myObj);
        GameObject target = null;

        if (target != null) // 対象の方向を計算
        {
            vector = (target.transform.position - myObj.transform.position).normalized;
            Debug.Log($"対象への方向：{vector}");
        }
        else                // 対象がいないなら真っすぐ進む
        {
            vector = new Vector3(-1, 0);
        }

        // 移動
        Vector3 moveVelocity = vector * moveSpeed * 0.1f * Time.deltaTime;
        myObj.transform.position += moveVelocity;
    }
}
