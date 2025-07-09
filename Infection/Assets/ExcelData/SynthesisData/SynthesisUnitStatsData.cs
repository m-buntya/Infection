using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class SynthesisUnitStatsData : ScriptableObject
{
    public List<SynthesisUnitStats> SynthesisUnitParameter; // Replace 'EntityType' to an actual type that is serializable.
}