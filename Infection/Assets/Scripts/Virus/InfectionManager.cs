using UnityEngine;
using System.Collections.Generic;

public class InfectionManager : MonoBehaviour
{
    public static InfectionManager Instance { get; private set; }

    private List<UnitInfection> activeUnits = new List<UnitInfection>();
    private float infectionInterval = 3f;
    private float timer = 0f;
    private float infectionAmount = 10f;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= infectionInterval)
        {
            timer = 0f;
            ApplyInfection();
        }
    }

    private void ApplyInfection()
    {
        // リストのコピーを使用して安全にループする
        var snapshot = new List<UnitInfection>(activeUnits);
        foreach (var unit in snapshot)
        {
            unit.AddInfection(infectionAmount);
        }
    }

    public void RegisterUnit(UnitInfection unit)
    {
        if (!activeUnits.Contains(unit))
            activeUnits.Add(unit);
    }

    public void UnregisterUnit(UnitInfection unit)
    {
        if (activeUnits.Contains(unit))
            activeUnits.Remove(unit);
    }
}