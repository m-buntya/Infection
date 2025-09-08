using UnityEngine;

public class UnitCost : MonoBehaviour
{
    [SerializeField] private int deployCost = 2;

    public int DeployCost => deployCost;

    public bool TryConsumeCost()
    {
        if (CostManager.Instance == null)
        {
            Debug.LogWarning("CostManagerが見つかりません！");
            return false;
        }

        if (!CostManager.Instance.CanAfford(deployCost))
        {
            CostManager.Instance.DisplayInsufficientCostFeedBack();
            return false;
        }

        CostManager.Instance.SpendCost(deployCost);
        //Debug.Log($"{gameObject.name} の出撃にコスト {deployCost} を消費しました。");
        return true;
    }
}