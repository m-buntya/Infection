using StatePatteren.State;
using UnityEngine;

public static class UnitCreator
{
    public static UnitController CreateUnitByCode(string code)
    {
        string prefabName = GetPrefabNameByCode(code);
        if (string.IsNullOrEmpty(prefabName))
        {
            Debug.LogWarning($"[UnitCreator] unitCode '{code}' に対応するプレハブ名が見つかりません");
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

    public static string GetPrefabNameByCode(string unitCode)
    {
        switch (unitCode)
        {
            case "Unit": return "Unit";
            case "Zombie": return "Zombi";
            case "virus": return "virus";
            case "virus_Infection": return "virus_Infection";
            case "White_Line_0": return "White_Line_0";
            case "monster": return "monster";
            case "Healer":return "Healer";
            case "Tank":return "Tank";
            default: return null;
        }
    }
    public static GameObject GetPrefabByCode(string unitCode)
    {
        string prefabName = GetPrefabNameByCode(unitCode);
        if (string.IsNullOrEmpty(prefabName)) return null;

        return Resources.Load<GameObject>($"Units/{prefabName}");
    }
}
