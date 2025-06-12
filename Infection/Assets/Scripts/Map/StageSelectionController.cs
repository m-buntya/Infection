using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


// ステージ選択の挙動を管理するコントローラー
public class StageSelectionController : MonoBehaviour
{
    [Header("Main Camera")]
    [SerializeField] private Camera mainCamera;

    [Header("Selectable Stage Layer")]
    [SerializeField] private LayerMask stageLayer;

    [Header("Stage Info UI")]
    [SerializeField] private GameObject stageInfoPanel;

    [SerializeField] private Button deployButton;


    private string selectedAreaName;
    private string selectedStageName;


    // 初期化処理
    private void Awake()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        selectedAreaName = PlayerPrefs.GetString("SelectedArea", "");

        if (string.IsNullOrEmpty(selectedAreaName))
        {
            Debug.LogWarning("選択されたエリア名が保存されていません。");
        }
        else
        {
            Debug.Log($"エリア「{selectedAreaName}」が選択されました。ステージ選択を開始してください。");
        }

        stageInfoPanel.SetActive(false);
        deployButton.onClick.AddListener(OnDeployButtonClicked);
    }


    // 毎フレームの更新処理
    private void Update()
    {
        if (string.IsNullOrEmpty(selectedAreaName))
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            DetectStageClick();
        }
    }


    // マウスクリックでステージを検出する
    private void DetectStageClick()
    {
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(
            mousePosition,
            Vector2.zero,
            Mathf.Infinity,
            stageLayer
        );

        if (hit.collider != null)
        {
            selectedStageName = hit.collider.gameObject.name;

            Debug.Log($"ステージ \"{selectedStageName}\" が選択されました。");

            PlayerPrefs.SetString("SelectedStage", selectedStageName);
            ShowStageInfoPanel(selectedStageName);
        }
    }


    // ステージ情報パネルを表示する
    private void ShowStageInfoPanel(string stageName)
    {
        stageInfoPanel.SetActive(true);
    }


    // 出撃ボタンが押されたときの処理
    private void OnDeployButtonClicked()
    {
        if (Application.CanStreamedLevelBeLoaded(selectedStageName))
        {
            SceneManager.LoadScene(selectedStageName);
        }
        else
        {
            Debug.LogError($"シーン '{selectedStageName}' が Build Settings に登録されていません。");
        }
    }
}

// 出典：ChatGPT によるコードリファクタリング（2025年6月）
