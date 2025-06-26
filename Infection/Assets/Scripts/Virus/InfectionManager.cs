using System.Collections.Generic;
using UnityEngine;
using StatePatteren.State;

public enum InfectionMode
{
    Off,
    AutoTest,
    Manual
}

public class InfectionManager : MonoBehaviour
{
    [Header("感染モード")]
    [SerializeField] private InfectionMode infectionMode = InfectionMode.Off;

    [Header("感染設定")]
    [SerializeField] private float infectionInterval = 3f;
    [SerializeField] private float infectionPoint = 2f;
    [SerializeField] private string infectionType = "Enemy";
    [SerializeField] private float maxPoint = 100f;

    [Header("管理ユニット一覧")]
    [SerializeField] private List<UnitController> unitList = new List<UnitController>();

    private float nextInfectionTime = 0f;

    void Start()
    {
        nextInfectionTime = Time.time + infectionInterval;
    }

    void Update()
    {
        if (infectionMode != InfectionMode.AutoTest) return;

        if (Time.time >= nextInfectionTime)
        {
            ApplyInfectionToAll();
            nextInfectionTime = Time.time + infectionInterval;
        }
    }

    public void ApplyInfectionToAll()
    {
        foreach (var unit in unitList)
        {
            if (unit == null) continue;

            AddInfection(unit, infectionPoint, infectionType);
            Debug.Log($"[AutoTest] {unit.name} に {infectionPoint} 感染ポイントを加算");
        }
    }

    public void Infect(UnitController unit, float point, string type)
    {
        if (unit != null)
        {
            AddInfection(unit, point, type);
        }
    }

    public void HealInfection(UnitController unit, float point, string type)
    {
        if (unit == null) return;

        if (type == "Enemy")
            unit.unitStats.enemyVirusPoint = Mathf.Max(0f, unit.unitStats.enemyVirusPoint - point);
        else
            unit.unitStats.virusPoint = Mathf.Max(0f, unit.unitStats.virusPoint - point);
    }

    private void AddInfection(UnitController unit, float point, string type)
    {
        if (type == "Enemy")
            unit.unitStats.enemyVirusPoint = Mathf.Min(unit.unitStats.enemyVirusPoint + point, maxPoint);
        else
            unit.unitStats.virusPoint = Mathf.Min(unit.unitStats.virusPoint + point, maxPoint);
    }

    public void RegisterUnit(UnitController unit)
    {
        if (unit != null && !unitList.Contains(unit))
            unitList.Add(unit);
    }

    public void UnregisterUnit(UnitController unit)
    {
        if (unitList.Contains(unit))
            unitList.Remove(unit);
    }

    public void SetInfectionMode(InfectionMode mode)
    {
        infectionMode = mode;
    }

    public void ResetAllInfections()
    {
        foreach (var unit in unitList)
        {
            if (unit == null) continue;
            unit.unitStats.virusPoint = 0f;
            unit.unitStats.enemyVirusPoint = 0f;
        }
    }
}