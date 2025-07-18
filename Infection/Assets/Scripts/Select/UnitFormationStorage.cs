using System.Collections.Generic;
using UnityEngine;

public static class UnitFormationStorage
{
    public static void SaveFormation(List<UnitSlotButton> slots)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            var controller = slots[i].unitController;
            string code = controller != null ? controller.unitStats.unitCode.ToString() : "";
            Debug.Log($"保存するスロット {i} のコード: {code}");
            PlayerPrefs.SetString($"unit_slot_{i}", code);
        }
        PlayerPrefs.Save();
    }
    public static List<string> LoadFormation(int slotCount)
    {
        var codes = new List<string>();
        for (int i = 0; i < slotCount; i++)
        {
            string code = PlayerPrefs.GetString($"unit_slot_{i}", "");
            codes.Add(code);
        }
        return codes;
    }
}