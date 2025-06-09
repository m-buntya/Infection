using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class UnitStatsData : ScriptableObject
{
	public List<UnitStats> UnitParameter; // Replace 'EntityType' to an actual type that is serializable.
}
