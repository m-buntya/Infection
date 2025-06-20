using UnityEngine;

[CreateAssetMenu(fileName = "UnitData", menuName = "Game/UnitData")]
public class UnitData : ScriptableObject
{
    public string unitName;  // ユニット名
    public int cost;         // 必要コスト
    public GameObject prefab; // ユニットのプレハブ
    public int maxDeployCount = 3; //出撃数の限度
    public float cooldownTime = 5f; //クールタイム
    public bool canGrowDeploycount = false;
    public int attackPower = 10; //攻撃力
    public float attackInterval = 1.5f; // ✅ ユニットの攻撃間隔


}