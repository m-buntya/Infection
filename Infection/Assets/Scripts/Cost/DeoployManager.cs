using UnityEngine;
using System.Collections.Generic;
using StatePatteren.State;

public class DeployManager : MonoBehaviour
{
    public static DeployManager Instance { get; private set; }

    [Header("コスト管理")]
    [SerializeField] private CostManager costManager;

    [Header("出撃ポイント（空オブジェクト）")]
    [SerializeField] private Transform deployPoint;

    [Header("ユニットの親オブジェクト（任意）")]
    [SerializeField] private Transform deployParent;

    [Header("出撃対象ユニット（ボタン連携用）")]
    [SerializeField] private UnitController targetUnit;

    // クールタイム管理
    private Dictionary<UnitController, float> unitCooldowns = new Dictionary<UnitController, float>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Update()
    {
        // クールタイム更新
        List<UnitController> keys = new List<UnitController>(unitCooldowns.Keys);
        foreach (var unit in keys)
        {
            unitCooldowns[unit] -= Time.deltaTime;
            if (unitCooldowns[unit] <= 0f)
            {
                unitCooldowns.Remove(unit);
            }
        }
    }

    public void TryDeployUnit(UnitController unitController)
    {
        if (unitController == null || costManager == null || deployPoint == null)
        {
            Debug.LogWarning("必要な参照が不足しています！");
            return;
        }

        // クールタイム中なら出撃不可
        if (unitCooldowns.ContainsKey(unitController))
        {
            Debug.Log($"{unitController.unitStats.unitName} はクールタイム中（残り {unitCooldowns[unitController]:F1} 秒）");
            return;
        }

        // コスト不足なら出撃不可
        if (!costManager.CanAfford(unitController.unitStats.cost))
        {
            costManager.DisplayInsufficientCostFeedBack();
            return;
        }

        // コスト消費
        costManager.SpendCost(unitController.unitStats.cost);

        // ユニット生成
        //GameObject unitObj = Instantiate(unitController.unitStats.prefab, deployPoint.position, Quaternion.identity, deployParent);

        // クールタイム設定
        unitCooldowns[unitController] = unitController.unitStats.sortieCoolTime;

        Debug.Log($"{unitController.unitStats.unitName} を出撃！ 次は {unitController.unitStats.sortieCoolTime} 秒後に再出撃できます。");
    }

    // UIボタンから呼び出す用
    public void OnDeployButtonPressed()
    {
        TryDeployUnit(targetUnit);
    }
}