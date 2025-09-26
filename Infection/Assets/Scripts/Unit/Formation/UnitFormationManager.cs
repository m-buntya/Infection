using System.Collections.Generic;
using UnityEngine;

public static class UnitFormationManager
{
    private static UnitFormationData currentFormation = new();

    public static void SaveFormation(List<UnitSlotButton> buttons)
    {
        currentFormation.slotDataList.Clear();
        Debug.Log($"📦 SaveFormation 開始: {buttons.Count} スロット");

        foreach (var button in buttons)
        {
            Debug.Log($"🔍 スロット: unitCode = {button.unitCode}, icon = {button.iconImage?.sprite?.name}");

            var data = new UnitSlotData
            {
                unitCode = button.unitCode,
                iconName = button.iconImage?.sprite?.name ?? ""
            };
            currentFormation.slotDataList.Add(data);
        }

        Debug.Log($"📦 SaveFormation 完了: {currentFormation.slotDataList.Count} 件保存");
    }

    public static UnitFormationData GetFormation()
    {
        //Debug.Log("📤 GetFormation 呼び出し");

        if (currentFormation == null)
        {
            Debug.LogWarning("⚠️ currentFormation が null");
        }

        Debug.Log($"📤 slotDataList 件数: {currentFormation.slotDataList.Count}");

        for (int i = 0; i < currentFormation.slotDataList.Count; i++)
        {
            var data = currentFormation.slotDataList[i];
            Debug.Log($"🔁 復元スロット {i}: {data.unitCode}, {data.iconName}");
        }

        return currentFormation;
    }
}