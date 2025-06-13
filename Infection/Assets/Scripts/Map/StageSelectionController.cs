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
    [SerializeField] private Stage stageDataAsset;  // ScriptableObject���A�^�b�`

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

            // �Ή����� StageData ��T��
            selectedStageData = stageDataAsset.stageDatas
                .FirstOrDefault(s => s.stageID.ToString() == selectedStageName || s.stageID == int.Parse(selectedStageName));

            if (selectedStageData != null)
            {
                Debug.Log($"�X�e�[�W '{selectedStageName}' ���I������܂��� (ID: {selectedStageData.stageID})");

                // GameManager �ɃZ�b�g
                GameManager.Instance.SetStageData(selectedStageData);

                ShowStageInfoPanel();
            }
            else
            {
                Debug.LogWarning($"'{selectedStageName}' �Ɉ�v����StageData��������܂���ł����B");
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
            SceneManager.LoadScene("Main");  // ���ʂ� Main �V�[���ɑJ��
        }
        else
        {
            Debug.LogError("�o�����悤�Ƃ��Ă���X�e�[�W�f�[�^�� null �ł��B");
        }
    }
}
