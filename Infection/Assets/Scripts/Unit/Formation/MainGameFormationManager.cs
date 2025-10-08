using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using StatePatteren.State;

public class MainGameFormationManager : MonoBehaviour
{
    [Header("ユニットを配置するスロットUI")]
    public List<UnitSlotUI> slotUIs;

    [Header("ユニットを配置するゲーム空間の親")]
    public Transform unitParent;

    [Header("ドラッグ可能なユニットボタン")]
    public List<UnitDragHandler> dragHandlers;

    IEnumerator Start()
    {
        yield return null;

        var formation = UnitFormationManager.GetFormation();
        if (formation == null)
        {
            Debug.LogError("[MainGame] formation が null です");
            yield break;
        }

        for (int i = 0; i < formation.slotDataList.Count; i++)
        {
            var data = formation.slotDataList[i];

            // プレファブ名とパスを取得
            string prefabName = UnitCreator.GetPrefabNameByCode(data.unitCode);
            string path = $"Units/{prefabName}";
            GameObject prefab = Resources.Load<GameObject>(path);
            Sprite icon = Resources.Load<Sprite>($"Sprites/{data.iconName}");

            if (prefab == null)
            {
                Debug.LogWarning($"[MainGame] プレハブ読み込み失敗: unitCode = {data.unitCode}, path = {path}");
                continue;
            }

            // ドラッグボタンにプレファブを設定
            if (i < dragHandlers.Count)
            {
                var handler = dragHandlers[i];
                handler.unitPrefab = prefab;
                handler.gameObject.SetActive(true);
                Debug.Log($"[MainGame] ドラッグボタン割り当て: slot[{i}] = {prefab.name}");
            }

            // ユニット生成（Scene表示用）
            UnitController unit = UnitCreator.CreateUnitByCode(data.unitCode);

            // スロットUIに反映
            if (i < slotUIs.Count)
            {
                slotUIs[i].SetUnit(unit, icon);
            }

            // ゲーム空間に配置
            if (unit != null)
            {
                var instance = Instantiate(unit.gameObject, unitParent);
                instance.transform.position = GetSpawnPosition(i);
            }
        }
    }

    Vector3 GetSpawnPosition(int index)
    {
        // スロット番号に応じた配置位置を返す（例：橋の左右など）
        return new Vector3(index * 2f, 0, 0); // 仮の例
    }
}