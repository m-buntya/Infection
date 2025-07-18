using UnityEngine;
using UnityEngine.UI;
using TMPro;
using StatePatteren.State;
public class DeployButtonController:MonoBehaviour
{
    [SerializeField] private UnitController assignedUnit;
    [SerializeField] private TMP_Text costLabelText;
    [SerializeField] private int costToConsume = 2;

    void Start()
    {
        if(costLabelText!=null)
        {
    costLabelText.text=$"{costToConsume}";
        }
    }
    public void OnPressed()
    {
        int cost = assignedUnit.unitStats.cost;

        if (!CostManager.Instance.CanAfford(costToConsume))
        {
            CostManager.Instance.DisplayInsufficientCostFeedBack();
            return;
        }
        CostManager.Instance.SpendCost(costToConsume);
        Debug.Log($"コスト{costToConsume}を消費しました。");
        DeployManager.Instance.TryDeployUnit(assignedUnit);
    }
}
