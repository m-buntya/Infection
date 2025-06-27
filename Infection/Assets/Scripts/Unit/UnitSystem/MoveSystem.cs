using StatePatteren.State;
using UnityEngine;

public class MoveSystem
{
    // ˆÚ“®
    public void Move(GameObject myObj, string targetTag, float moveSpeed, Vector3 vector)
    {
        GetTargetSystem getTarget = new GetTargetSystem();
        GameObject target = getTarget.GetTarget(myObj, targetTag);

        if (target == null)
        {
            if (targetTag == "Enemy")
            {
                vector = new Vector3(-1, 0);
            }
            else
            {
                vector = new Vector3(1, 0);
            }
        }
        else
        {
            vector = target.transform.position - myObj.transform.position;
        }

        // ˆÚ“®
        Vector3 moveVelocity = vector.normalized * moveSpeed * 0.1f * Time.deltaTime;
        myObj.transform.position += moveVelocity;
    }
}
