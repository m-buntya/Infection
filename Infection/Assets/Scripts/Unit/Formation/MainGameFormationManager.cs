using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class MainGameFormationManager : MonoBehaviour
{
    [Header("ユニットを配置するスロットUI")]
    public List<UnitSlotUI> slotUIs;

    [Header("ドラッグ可能なユニットボタン")]
    public List<UnitDragHandler> dragHandlers;

    IEnumerator Start()
    {
        yield return null;
        LoadFormationFromManager();
    }

    void LoadFormationFromManager()
    {
        var formation = UnitFormationManager.GetFormation();
        if (formation == null)
        {
            Debug.LogError("[MainGame] formation が null");
            return;
        }

        for (int i = 0; i < formation.slotDataList.Count; i++)
        {
            var data = formation.slotDataList[i];

            // アイコン読み込み
            var icon = Resources.Load<Sprite>($"Sprites/{data.iconName}");

            if (i < slotUIs.Count)
            {
                slotUIs[i].SetUnit(data.unitCode, icon);
            }

            if (i < dragHandlers.Count)
            {
                var handler = dragHandlers[i];

                // unitCode が空ならドラッグ不可
                if (string.IsNullOrEmpty(data.unitCode))
                {
                    handler.gameObject.SetActive(false); // ✅ 完全に非表示にする
                    continue;
                }

                string prefabName = UnitCreator.GetPrefabNameByCode(data.unitCode);
                if (string.IsNullOrEmpty(prefabName))
                {
                    Debug.LogWarning($"[MainGame] unitCode '{data.unitCode}' に対応するプレハブ名が不明です");
                    handler.gameObject.SetActive(false); // ✅ プレハブ不明でも非表示
                    continue;
                }

                var prefab = Resources.Load<GameObject>($"Units/{prefabName}");
                if (prefab == null)
                {
                    Debug.LogWarning($"[MainGame] プレハブが見つかりません: Units/{prefabName}");
                    handler.gameObject.SetActive(false); // ✅ プレハブ未取得でも非表示
                    continue;
                }

                handler.unitPrefab = prefab;
                handler.gameObject.SetActive(true); // ✅ 有効なユニットなら表示
            }
        }
    }
}