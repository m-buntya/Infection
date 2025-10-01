using System.Collections.Generic;
using UnityEngine;

public class UnitPrefabAssigner : MonoBehaviour
{
    [Header("ドラッグハンドラー一覧")]
    public List<UnitDragHandler> dragHandlers;

    void Start()
    {
        //Debug.Log("[Assigner] Start 実行");

        var formation = UnitFormationManager.GetFormation();
        if (formation == null)
        {
            Debug.LogError("[Assigner] formation が null");
            return;
        }

        for (int i = 0; i < dragHandlers.Count && i < formation.slotDataList.Count; i++)
        {
            var data = formation.slotDataList[i];
            //Debug.Log($"[Assigner] slot[{i}] unitCode = '{data.unitCode}'");

            if (string.IsNullOrEmpty(data.unitCode))
            {
                Debug.LogWarning($"[Assigner] unitCode が未設定です: slot[{i}]");
                continue;
            }


            string path = $"Units/{data.unitCode}";
            var prefab = Resources.Load<GameObject>(path);
            //Debug.Log(prefab != null
            //    //? $"✅ 読み込み成功: {path}"
            //    //: $"⚠️ 読み込み失敗: {path}"
            //    );


            if (prefab == null)
            {
                Debug.LogWarning($"[Assigner] プレハブ読み込み失敗: {path}");
                continue;
            }

            dragHandlers[i].unitPrefab = prefab;
            Debug.Log($"[Assigner] 割り当て成功: slot[{i}] = {prefab.name}");
        }
    }

}