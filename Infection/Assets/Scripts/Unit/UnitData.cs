using UnityEngine;

[CreateAssetMenu(fileName = "UnitData", menuName = "Game/UnitData")]
public class UnitData : ScriptableObject
{
    public string unitName;  // ユニット名
    public int cost;         // 必要コスト
    public GameObject prefab; // ユニットのプレハブ
}