using StatePatteren.State;
using UnityEngine;

public class MoveSystem
{
    // �ړ�
    public void Move(GameObject myObj, string targetTag, float moveSpeed, Vector3 vector)
    {
        GetTargetSystem getTarget = new GetTargetSystem();
        //GameObject target = getTarget.GetTarget(targetTag, myObj);
        GameObject target = null;

        if (target != null) // �Ώۂ̕������v�Z
        {
            vector = (target.transform.position - myObj.transform.position).normalized;
            Debug.Log($"�Ώۂւ̕����F{vector}");
        }
        else                // �Ώۂ����Ȃ��Ȃ�^�������i��
        {
            vector = new Vector3(-1, 0);
        }

        // �ړ�
        Vector3 moveVelocity = vector * moveSpeed * 0.1f * Time.deltaTime;
        myObj.transform.position += moveVelocity;
    }
}
