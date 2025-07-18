using Mono.Cecil;
using StatePatteren.State;
using UnityEngine;

public static class UnitFactory
{
    public static UnitController CreateUnitByCode(string code)
    {
        GameObject unitPrefab = Resources.Load<GameObject>($"Units/{code}");
        if (unitPrefab == null)
        {
            Debug.LogWarning($"Unit prefab not found for code: {code}");
            return null;
        }

        GameObject obj = GameObject.Instantiate(unitPrefab);
        UnitController controller = obj.GetComponent<UnitController>();
        if (controller == null)
        {
            Debug.LogWarning($"UnitController not found on prefab: {code}");
        }

        return controller;
    }

}
