using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Linq;

/// ステージ選択画面でボタンをクリックしてステージデータを選択し、出撃処理を行うコントローラー
/// 出展：ChatGPT生成
public class StageSelectionController : MonoBehaviour
{
    [Header("ステージ選択ボタン")]
    [SerializeField] private Button[] stageButtons;

    [Header("ステージ情報UI")]
    [SerializeField] private GameObject stageInfoPanel;

    [SerializeField] private Button deployButton;

    [Header("ステージデータ")]
    [SerializeField] private Stage stageDataAsset;

    private StageData selectedStageData;


    private void Awake()
    {
        stageInfoPanel.SetActive(false);

        deployButton.onClick.AddListener(OnDeployButtonClicked);

        foreach (Button stageButton in stageButtons)
        {
            stageButton.onClick.AddListener(() => OnStageButtonClicked(stageButton));
        }
    }


    /// ステージボタンが押されたときに呼ばれる処理
    private void OnStageButtonClicked(Button button)
    {
        TMP_Text stageIdText = button.GetComponentInChildren<TMP_Text>();

        if (stageIdText == null)
        {
            Debug.LogError("ボタンに対応するTMP_Textが見つかりません。");
            return;
        }

        string stageId = stageIdText.text.Trim();

        selectedStageData = stageDataAsset.stageDatas
            .FirstOrDefault(stage => stage.stageID == stageId);

        if (selectedStageData != null)
        {
            Debug.Log($"ステージ「{stageId}」が選択されました。");

            GameManager.Instance.SetStageData(selectedStageData);

            ShowStageInfoPanel();
        }
        else
        {
            Debug.LogWarning($"ステージID「{stageId}」に一致するデータが見つかりませんでした。");
        }
    }


    /// ステージ情報パネルを表示
    private void ShowStageInfoPanel()
    {
        stageInfoPanel.SetActive(true);
    }


    /// 出撃ボタンが押されたときに呼ばれる処理
    private void OnDeployButtonClicked()
    {
        if (selectedStageData != null)
        {
            SceneManager.LoadScene("Main");
        }
        else
        {
            Debug.LogError("出撃しようとしているステージデータがnullです。");
        }
    }
}
