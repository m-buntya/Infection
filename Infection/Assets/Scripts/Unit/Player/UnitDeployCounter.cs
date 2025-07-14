//using TMPro;
//using UnityEngine;
//using StatePatteren.State;
//public class UnitDeployCounter : MonoBehaviour
//{
//    [SerializeField] private UnitController unitController; // ✅ ユニットのデータ
//    [SerializeField] private TMP_Text deployText; // ✅ 出撃可能数を表示するUI

//    private void Start()
//    {
//        UpdateDeployText();
//    }

//    public void UpdateDeployText()
//    {
//        if (deployText != null && DeployManager.Instance != null)
//        {
//            int remainingDeploys = unitController.maxDeployCount - DeployManager.Instance.GetDeployedCount(unitController);
//            deployText.text = $"出撃可能: {remainingDeploys}/{unitController.maxDeployCount}";
//        }
//    }
//}