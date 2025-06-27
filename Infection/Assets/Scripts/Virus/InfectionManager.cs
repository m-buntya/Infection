using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StatePatteren.State;
using UnityEditor;

public class InfectionManager : MonoBehaviour
{
    [Tooltip("管理対象のユニット（自動追加も可能)")]
    public List<UnitController> unitList = new List<UnitController>();

    [Tooltip("感染加算間隔（秒）")]
    public float interval = 3f;

    [Tooltip("感染加算ポイント")]
    public float virusPointPerTick = 10f;
    [Tooltip("感染タイプ（Enemy　or　Self")]
    public string virusType = "Enemy";
    private Dictionary<UnitController, UnitData> unitDataMap = new Dictionary<UnitController, UnitData>();


    private void Start()
    {
        
        unitList.AddRange(FindObjectsOfType<UnitController>());
        StartCoroutine(AddVirusPointsRoutine());

    }
    IEnumerator AddVirusPointsRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);

            foreach (var unit in unitList)
            {
                if (unit == null || !unitDataMap.ContainsKey(unit)) continue;

                UnitData data = unitDataMap[unit];
                float resistance = Mathf.Max(data.virusResistance, 0.1f);
                float maxPoint = data.maxVirusPoint;

                float current = virusType == "Enemy"
                    ? unit.unitStats.enemyVirusPoint
                    : unit.unitStats.virusPoint;

                // 最大値に到達していなければ感染を加算
                if (current < maxPoint)
                {
                    float add = Mathf.Min(virusPointPerTick / resistance, maxPoint - current);
                    unit.TakeVirusDamage(add, virusType);

                    float after = virusType == "Enemy"
                        ? unit.unitStats.enemyVirusPoint
                        : unit.unitStats.virusPoint;

                    Debug.Log($"[{unit.name}] {virusType}ウイルス：{after} / {maxPoint} (+{add})");
                }
            }
        }
    }


    public void AddUnit(UnitController unit)
    {
        if (!unitList.Contains(unit))
        {
            unitList.Add(unit);
        }
    }

    //ユニット削除（仮）
    public void RemoveUnit(UnitController unit)
    {
        //unitList.Remove(unit);
    }

    public void RegisterUnit(UnitController unit, UnitData data)
    {
        if (!unitList.Contains(unit))
        {
            unitList.Add(unit);
            unitDataMap[unit] = data;
        }
    }

}