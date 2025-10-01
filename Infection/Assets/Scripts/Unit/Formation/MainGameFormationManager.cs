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
            //Debug.Log($"[MainGame] slot[{i}] unitCode = {data.unitCode}, iconName = {data.iconName}");

            // アイコン読み込み
            var icon = Resources.Load<Sprite>($"Sprites/{data.iconName}");

            // UIに反映（UnitControllerは渡さない）
            if (i < slotUIs.Count)
            {
                slotUIs[i].SetUnit(data.unitCode, icon);
            }

            // ドラッグハンドラーにプレハブを設定
            if (i < dragHandlers.Count)
            {
                string prefabName = UnitCreator.GetPrefabNameByCode(data.unitCode);
                if (string.IsNullOrEmpty(prefabName))
                {
                    Debug.LogWarning($"[MainGame] unitCode '{data.unitCode}' に対応するプレハブ名が不明です");
                    continue;
                }

                var prefab = Resources.Load<GameObject>($"Units/{prefabName}");
                if (prefab == null)
                {
                    Debug.LogWarning($"[MainGame] プレハブが見つかりません: Units/{prefabName}");
                    continue;
                }

                dragHandlers[i].unitPrefab = prefab;
                //Debug.Log($"[MainGame] dragHandler[{i}] に unitPrefab を設定: {prefab.name}");
            }
        }
    }
}