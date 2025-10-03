using System.Collections.Generic;
using UnityEngine;

public class UnitPrefabAssigner : MonoBehaviour
{
    [Header("ドラッグハンドラー一覧")]
    public List<UnitDragHandler> dragHandlers;

    void Start()
    {
        Debug.Log("[Assigner] Start 実行");

        var formation = UnitFormationManager.GetFormation();
        if (formation == null)
        {
            Debug.LogError("[Assigner] formation が null");
            return;
        }

        for (int i = 0; i < dragHandlers.Count && i < formation.slotDataList.Count; i++)
        {
            var data = formation.slotDataList[i];

            // ✅ ここに書く！
            string prefabName = UnitCreator.GetPrefabNameByCode(data.unitCode);
            string path = $"Units/{prefabName}";

            var prefab = Resources.Load<GameObject>(path);

            if (prefab == null)
            {
                Debug.LogWarning($"[Assigner] プレハブ読み込み失敗: unitCode = {data.unitCode}, path = {path}");
                continue;
            }

            dragHandlers[i].unitPrefab = prefab;
            dragHandlers[i].gameObject.SetActive(true); // 念のため表示状態を保証
            Debug.Log($"[Assigner] 割り当て成功: slot[{i}] = {prefab.name}");
        }
    }

}