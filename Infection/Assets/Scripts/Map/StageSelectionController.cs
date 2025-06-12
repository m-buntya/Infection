using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


// �X�e�[�W�I���̋������Ǘ�����R���g���[���[
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


    // ����������
    private void Awake()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        selectedAreaName = PlayerPrefs.GetString("SelectedArea", "");

        if (string.IsNullOrEmpty(selectedAreaName))
        {
            Debug.LogWarning("�I�����ꂽ�G���A�����ۑ�����Ă��܂���B");
        }
        else
        {
            Debug.Log($"�G���A�u{selectedAreaName}�v���I������܂����B�X�e�[�W�I�����J�n���Ă��������B");
        }

        stageInfoPanel.SetActive(false);
        deployButton.onClick.AddListener(OnDeployButtonClicked);
    }


    // ���t���[���̍X�V����
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


    // �}�E�X�N���b�N�ŃX�e�[�W�����o����
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

            Debug.Log($"�X�e�[�W \"{selectedStageName}\" ���I������܂����B");

            PlayerPrefs.SetString("SelectedStage", selectedStageName);
            ShowStageInfoPanel(selectedStageName);
        }
    }


    // �X�e�[�W���p�l����\������
    private void ShowStageInfoPanel(string stageName)
    {
        stageInfoPanel.SetActive(true);
    }


    // �o���{�^���������ꂽ�Ƃ��̏���
    private void OnDeployButtonClicked()
    {
        if (Application.CanStreamedLevelBeLoaded(selectedStageName))
        {
            SceneManager.LoadScene(selectedStageName);
        }
        else
        {
            Debug.LogError($"�V�[�� '{selectedStageName}' �� Build Settings �ɓo�^����Ă��܂���B");
        }
    }
}

// �o�T�FChatGPT �ɂ��R�[�h���t�@�N�^�����O�i2025�N6���j
