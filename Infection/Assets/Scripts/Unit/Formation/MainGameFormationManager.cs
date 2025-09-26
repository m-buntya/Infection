using UnityEngine;
using System.Collections.Generic;

public class MainGameFormationManager : MonoBehaviour
{
    [Header("ユニットを配置するスロットUI")]
    public List<UnitSlotUI> slotUIs;

    [Header("ユニットを配置するゲーム空間の親")]
    public Transform unitParent;

    [Header("ドラッグ可能なユニットボタン")]
    public List<UnitDragHandler> dragHandlers;

    void Start()
    {
        var formation = UnitFormationManager.GetFormation();
        for(int i = 0; i < formation.slotDataList.Count; i++)
        {
            var data = formation.slotDataList[i];
            var unit = UnitCreator.CreateUnitByCode(data.unitCode);
            if (unit != null && i < dragHandlers.Count)
            {
                dragHandlers[i].unitPrefab = unit.gameObject;
                Debug.Log($"ユニット{data.unitCode}をスロット{i}に設定");
            }
        }

        LoadFormationFromManager();
    }

    void LoadFormationFromManager()
    {
        var formation = UnitFormationManager.GetFormation();

        for (int i = 0; i < formation.slotDataList.Count; i++)
        {
            var data = formation.slotDataList[i];
            var unit = UnitCreator.CreateUnitByCode(data.unitCode);
            var icon = Resources.Load<Sprite>($"Icons/{data.iconName}");

            // UIに反映
            if (i < slotUIs.Count)
            {
                slotUIs[i].SetUnit(unit, icon);
            }

            // ゲーム空間に配置（例：Instantiate）
            if (unit != null)
            {
                var instance = Instantiate(unit.gameObject, unitParent);
                instance.transform.position = GetSpawnPosition(i); // 任意の配置ロジック
            }

            Debug.Log($"🧪 メインゲーム復元: スロット {i}, ユニット = {data.unitCode}, アイコン = {data.iconName}");
        }
    }

    Vector3 GetSpawnPosition(int index)
    {
        // スロット番号に応じた配置位置を返す（例：橋の左右など）
        return new Vector3(index * 2f, 0, 0); // 仮の例
    }
}