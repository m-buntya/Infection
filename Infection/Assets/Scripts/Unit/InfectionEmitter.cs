using UnityEngine;
using System.Collections.Generic;

public class InfectionEmitter : MonoBehaviour
{
    [Header("感染進行設定")]
    public float infectionAmountPerTick = 0.1f;
    public float tickInterval = 1f;

    private float timer = 0f;
    private HashSet<UnitInfection> unitsInRange = new HashSet<UnitInfection>();

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= tickInterval)
        {
            timer = 0f;
            InfectUnits(); // 範囲内の全ユニットに感染を進める
        }
    }

    void InfectUnits()
    {
        foreach (var unit in unitsInRange)
        {
            if (unit != null && unit.enabled)
            {
                unit.StartProgress(); // 感染開始
                unit.AddInfection(infectionAmountPerTick); // ゲージ進行
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        UnitInfection unit = other.GetComponent<UnitInfection>();
        if (unit != null)
        {
            unitsInRange.Add(unit);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        UnitInfection unit = other.GetComponent<UnitInfection>();
        if (unit != null)
        {
            unitsInRange.Remove(unit);
            unit.StopProgress();
            Debug.Log($"感染範囲から出ました → {unit.gameObject.name}");

        }
    }
}