// UnitCreator.cs ‚É•Û‘¶
using StatePatteren.State;
using UnityEngine;

public static class UnitCreator
{
    public static UnitController CreateUnitByCode(string code)
    {
        var prefab = Resources.Load<GameObject>($"Units/{code}");
        if (prefab == null)
        {
            Debug.LogWarning($"ƒ†ƒjƒbƒgPrefab‚ªŒ©‚Â‚©‚è‚Ü‚¹‚ñ: {code}");
            return null;
        }

        var instance = GameObject.Instantiate(prefab);
        var controller = instance.GetComponent<UnitController>();
        return controller;
    }
}