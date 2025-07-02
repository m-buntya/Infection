using UnityEngine;

public enum UNITSIDE
{
    Player,
    Enemy
}

[CreateAssetMenu(fileName = "UnitData", menuName = "Game/UnitData")]
public class UnitData : ScriptableObject
{
    public string unitName;
    public int cost;
    public GameObject prefab;
    public int maxDeployCount = 3;
    public float cooldownTime = 5f;
    public bool canGrowDeploycount = false;
    public int attackPower = 10;
    public float attackInterval = 1.5f;

    [Header("感染関連パラメーター")]
    public float initialvirusPoint = 0f;
    public float maxVirusPoint = 100f;
    public float virusResistance = 0f;

    [Header("所属（初期値）")]
    public UNITSIDE defaultSide;
}