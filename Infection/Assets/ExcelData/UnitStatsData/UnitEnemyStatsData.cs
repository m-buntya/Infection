using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class UnitEnemyStatsData : ScriptableObject
{
	public List<UnitEnemyStats> UnitEnemyParameter; // Replace 'EntityType' to an actual type that is serializable.
}