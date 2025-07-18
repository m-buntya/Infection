using Mono.Cecil;
using StatePatteren.State;
using UnityEngine;

public  static class UnitFactory
{
    public static UnitController CreateUnitByCode(string code)
    {
        GameObject unitPrefab = Resources.Load<GameObject>($"Units/{code}");
        if (unitPrefab != null)
        {
            GameObject obj = GameObject.Instantiate(unitPrefab);
            return obj.GetComponent<UnitController>();
        }

        Debug.LogWarning($"Unit prefab not found for code: {code}");
        return null;
    }


}
