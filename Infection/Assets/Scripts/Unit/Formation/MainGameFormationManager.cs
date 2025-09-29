using UnityEngine;
using System.Collections.Generic;
using System.Collections;

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

            
            var icon = Resources.Load<Sprite>($"Sprites/{data.iconName}");

            var unit = UnitCreator.CreateUnitByCode(data.unitCode);

            // UIに反映
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

            // 🔍 デバッグログで確認
            Debug.Log($"🧩 読み込み: slot[{i}] unitCode = {data.unitCode}, iconName = {data.iconName}, icon = {(icon != null ? icon.name : "null")}");
        }
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

            //Debug.Log($"🧪 メインゲーム復元: スロット {i}, ユニット = {data.unitCode}, アイコン = {data.iconName}");
            Debug.Log($"[確認] スロット {i} の unitCode = '{data.unitCode}'");
        }
    }

    Vector3 GetSpawnPosition(int index)
    {
        // スロット番号に応じた配置位置を返す（例：橋の左右など）
        return new Vector3(index * 2f, 0, 0); // 仮の例
    }
}