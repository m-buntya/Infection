using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class UnitFormationData
{
    public List<UnitSlotData> slotDataList = new();
}

[System.Serializable]
public class UnitSlotData
{
    public string unitCode;
    public string iconName;
    public override string ToString()
    {
        return $"[UnitSlotData] unitCode = {unitCode}, iconName = {iconName}";
    }

}