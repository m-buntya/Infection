using StatePatteren.State;
using System.Collections.Generic;
using UnityEngine;

public static class UnitCreator
{
    public static UnitController CreateUnitByCode(string code)
    {
        string prefabName = GetPrefabNameByCode(code);
        if (string.IsNullOrEmpty(prefabName))
        {
            //Debug.LogWarning($"[UnitCreator] unitCode '{code}' に対応するプレハブ名が見つかりません");
            return null;
        }

        var prefab = Resources.Load<GameObject>($"Units/{prefabName}");
        if (prefab == null)
        {
            Debug.LogWarning($"[UnitCreator] プレハブが見つかりません: Units/{prefabName}");
            return null;
        }

        var instance = GameObject.Instantiate(prefab);
        return instance.GetComponent<UnitController>();
    }

    private static readonly Dictionary<string, string> prefabMap = new()
{
    { "3", "monster" },
    { "0", "Unit" },
    { "2", "virus" },
    { "6", "virus_Infection" },
    { "1", "Zombi" },
    { "Zombie", "Zombi" },
    { "virus", "virus" },
    // 他にも追加可能
};

    public static string GetPrefabNameByCode(string unitCode)
    {
        return prefabMap.TryGetValue(unitCode, out var name) ? name : null;
    }

}
