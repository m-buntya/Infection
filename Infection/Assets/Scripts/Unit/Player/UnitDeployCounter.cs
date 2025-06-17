using TMPro;
using UnityEngine;

public class UnitDeployCounter : MonoBehaviour
{
    [SerializeField] private UnitData unitData; // ✅ ユニットのデータ
    [SerializeField] private TMP_Text deployText; // ✅ 出撃可能数を表示するUI

    private void Start()
    {
        UpdateDeployText();
    }

    public void UpdateDeployText()
    {
        if (deployText != null && DeployManager.Instance != null)
        {
            int remainingDeploys = unitData.maxDeployCount - DeployManager.Instance.GetDeployedCount(unitData);
            deployText.text = $"出撃可能: {remainingDeploys}/{unitData.maxDeployCount}";
        }
    }
}