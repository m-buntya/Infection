using System.Collections.Generic;
using UnityEngine;

public static class UnitFormationManager
{
    private static UnitFormationData currentFormation = new();

    public static void SaveFormation(List<UnitSlotButton> buttons)
    {
        currentFormation.slotDataList.Clear();

        for (int i = 0; i < buttons.Count; i++)
        {
            var button = buttons[i];
            string iconName = button.iconImage?.sprite?.name ?? "";

            //Debug.Log($"🔍 スロット {i}: unitCode = {button.unitCode}, icon = {iconName}");

            var data = new UnitSlotData
            {
                unitCode = button.unitCode,
                iconName = iconName
            };
            currentFormation.slotDataList.Add(data);

            // 🔽 PlayerPrefs に保存（画像名）
            PlayerPrefs.SetString($"unit_icon_{i}", iconName);
            PlayerPrefs.SetString($"unit_code_{i}", button.unitCode);
        }

        PlayerPrefs.Save(); // 🔽 明示的に保存
        Debug.Log($"📦 SaveFormation 完了: {currentFormation.slotDataList.Count} 件保存");
    }

    public static void SetFormation(UnitFormationData formation)
    {
        currentFormation = formation;
    }

    public static UnitFormationData GetFormation()
    {
        return currentFormation;
    }


}