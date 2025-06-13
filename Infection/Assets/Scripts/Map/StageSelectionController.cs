using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class StageSelectionController : MonoBehaviour
{
    [Header("Main Camera")]
    [SerializeField] private Camera mainCamera;

    [Header("Selectable Stage Layer")]
    [SerializeField] private LayerMask stageLayer;

    [Header("Stage Info UI")]
    [SerializeField] private GameObject stageInfoPanel;

    [SerializeField] private Button deployButton;

    [Header("Stage Data")]
    [SerializeField] private Stage stageDataAsset;  // ScriptableObjectをアタッチ

    private string selectedStageName;
    private StageData selectedStageData;

    private void Awake()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        stageInfoPanel.SetActive(false);
        deployButton.onClick.AddListener(OnDeployButtonClicked);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DetectStageClick();
        }
    }

    private void DetectStageClick()
    {
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, stageLayer);

        if (hit.collider != null)
        {
            selectedStageName = hit.collider.gameObject.name;

            // 対応する StageData を探す
            selectedStageData = stageDataAsset.stageDatas
                .FirstOrDefault(s => s.stageID.ToString() == selectedStageName || s.stageID == int.Parse(selectedStageName));

            if (selectedStageData != null)
            {
                Debug.Log($"ステージ '{selectedStageName}' が選択されました (ID: {selectedStageData.stageID})");

                // GameManager にセット
                GameManager.Instance.SetStageData(selectedStageData);

                ShowStageInfoPanel();
            }
            else
            {
                Debug.LogWarning($"'{selectedStageName}' に一致するStageDataが見つかりませんでした。");
            }
        }
    }

    private void ShowStageInfoPanel()
    {
        stageInfoPanel.SetActive(true);
    }

    private void OnDeployButtonClicked()
    {
        if (selectedStageData != null)
        {
            SceneManager.LoadScene("Main");  // 共通の Main シーンに遷移
        }
        else
        {
            Debug.LogError("出撃しようとしているステージデータが null です。");
        }
    }
}
