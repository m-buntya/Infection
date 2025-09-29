using System.Collections.Generic;
using UnityEngine;

public class UnitPrefabAssigner : MonoBehaviour
{
    [Header("ドラッグハンドラー一覧")]
    public List<UnitDragHandler> dragHandlers;

    void Start()
    {
        var formation = UnitFormationManager.GetFormation();
        if (formation == null)
        {
            Debug.LogError("[UnitPrefabAssigner] formation が null");
            return;
        }

        for (int i = 0; i < dragHandlers.Count && i < formation.slotDataList.Count; i++)
        {
            var data = formation.slotDataList[i];
            string prefabPath = $"Units/{data.unitCode}"; // 例: Units/warrior

            GameObject prefab = Resources.Load<GameObject>(prefabPath);
            if (prefab == null)
            {
                Debug.LogWarning($"[UnitPrefabAssigner] プレハブ読み込み失敗: {prefabPath}");
                continue;
            }

            dragHandlers[i].unitPrefab = prefab;
            Debug.Log($"[UnitPrefabAssigner] unitPrefab セット: slot[{i}] = {prefab.name}");
        }
    }
}