using UnityEngine;

public class GetTargetSystem
{
    float maxDistance = 50f;        // 検知する最大距離

    // 最も近い対象を返す
    public GameObject GetTarget(string targetTag, GameObject myObj)
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);
        GameObject nearest = null;
        float minDistance = maxDistance;

        foreach (var target in targets)
        {
            float dist = Vector2.Distance(myObj.transform.position, target.transform.position);
            if (dist < minDistance)
            {
                minDistance = dist;
                nearest = target;
            }
        }

        return nearest;
    }
}
