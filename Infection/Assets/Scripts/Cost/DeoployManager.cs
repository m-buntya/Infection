using UnityEngine;

public class DeployManager : MonoBehaviour
{
    [SerializeField] private CostManager costManager;
    [SerializeField] private Transform deployPosition;
    [SerializeField] private UnitData[] unitOptions; // 異なるユニットのリスト

    private UnitData selectedUnit;

    public void SelectUnit(int index)
    {
        if (index >= 0 && index < unitOptions.Length)
        {
            selectedUnit = unitOptions[index]; // ボタンを押したときに異なるユニットをセット
            Debug.Log("選択されたユニット: " + selectedUnit.unitName);
        }
    }

    public void TryDeployUnit()
    {
        Debug.Log("TryDeployUnit 実行: ユニット出撃チェック");

        if (selectedUnit == null)
        {
            Debug.LogWarning("ユニットが選択されていません！");
            return;
        }

        if (costManager.CanAfford(selectedUnit.cost))
        {
            Debug.Log("コストOK！出撃します！");
            costManager.SpendCost(selectedUnit.cost);
            DeployUnit();
        }
        else
        {
            Debug.LogWarning("コストが足りません！");
        }
    }

    void DeployUnit()
    {
        if (selectedUnit == null || selectedUnit.prefab == null)
        {
            Debug.LogWarning("ユニットのプレハブが設定されていません！");
            return;
        }

        GameObject newUnit = Instantiate(selectedUnit.prefab, deployPosition.position, Quaternion.identity);
        Debug.Log(selectedUnit.unitName + " を出撃！ => " + newUnit);
    }
}