using UnityEngine;
using System.Collections.Generic;

public class GetTargetSystem
{
    float maxDistance = 10f;        // ŒŸ’m‚·‚éÅ‘å‹——£
    UnitManager unitManager;

    // Å‚à‹ß‚¢‘ÎÛ‚ğ•Ô‚·
    public GameObject GetTarget(GameObject myObj, string targetGroup)
    {
        unitManager = GameObject.Find("UnitManager").GetComponent<UnitManager>();
        if (unitManager == null) Debug.Log("unitManager‚ªnull‚Å‚·");

        List<GameObject> targets = new List<GameObject>();

        if(targetGroup == "Player")
        {
            var targetList = unitManager.GetPlayerUnits();
            for(int i = 0; i < targetList.Count; i++)
            {
                if (targetList[i] == myObj) continue;
                targets.Add(targetList[i]);
            }
        }
        if(targetGroup == "Enemy")
        {
            targets = unitManager.GetEnemyUnits();
        }

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

        if(nearest != null)
        {
            return nearest;
        }
        else
        {
            return null;
        }
    }
}
