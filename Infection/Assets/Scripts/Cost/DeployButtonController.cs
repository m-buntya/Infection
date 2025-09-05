using UnityEngine;
using TMPro;
using StatePatteren.State;

public class DeployButtonController : MonoBehaviour
{
    [SerializeField] private GameObject unitObject; // UnitControllerを含むユニット
    [SerializeField] private TMP_Text costLabelText;

    private UnitCost unitCost;

    void Start()
    {
        unitCost = unitObject.GetComponent<UnitCost>();

        if (costLabelText != null && unitCost != null)
        {
            costLabelText.text = $"{unitCost.DeployCost}";
        }
    }

    public void OnPressed()
    {
        if (unitCost == null)
        {
            Debug.LogWarning("UnitCostが見つかりません！");
            return;
        }

        if (unitCost.TryConsumeCost())
        {
            DeployManager.Instance.TryDeployUnit(unitObject.GetComponent<UnitController>());
        }
    }
}