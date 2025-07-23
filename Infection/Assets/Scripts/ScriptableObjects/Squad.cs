using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Squads
{
    public UnitStats[] units;
}

[CreateAssetMenu(fileName = "Squad", menuName = "Scriptable Objects/Squad")]
public class Squad : ScriptableObject
{
    public List<Squads> squadList;
}
