using UnityEngine;

public class GetTargetSystem
{
    float maxDistance = 50f;        // ŒŸ’m‚·‚éÅ‘å‹——£

    // Å‚à‹ß‚¢‘ÎÛ‚ğ•Ô‚·
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
