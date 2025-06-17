using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Linq;

/// �X�e�[�W�I����ʂŃ{�^�����N���b�N���ăX�e�[�W�f�[�^��I�����A�o���������s���R���g���[���[
/// �o�W�FChatGPT����
public class StageSelectionController : MonoBehaviour
{
    [Header("�X�e�[�W�I���{�^��")]
    [SerializeField] private Button[] stageButtons;

    [Header("�X�e�[�W���UI")]
    [SerializeField] private GameObject stageInfoPanel;

    [SerializeField] private Button deployButton;

    [Header("�X�e�[�W�f�[�^")]
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


    /// �X�e�[�W�{�^���������ꂽ�Ƃ��ɌĂ΂�鏈��
    private void OnStageButtonClicked(Button button)
    {
        TMP_Text stageIdText = button.GetComponentInChildren<TMP_Text>();

        if (stageIdText == null)
        {
            Debug.LogError("�{�^���ɑΉ�����TMP_Text��������܂���B");
            return;
        }

        string stageId = stageIdText.text.Trim();

        selectedStageData = stageDataAsset.stageDatas
            .FirstOrDefault(stage => stage.stageID == stageId);

        if (selectedStageData != null)
        {
            Debug.Log($"�X�e�[�W�u{stageId}�v���I������܂����B");

            GameManager.Instance.SetStageData(selectedStageData);

            ShowStageInfoPanel();
        }
        else
        {
            Debug.LogWarning($"�X�e�[�WID�u{stageId}�v�Ɉ�v����f�[�^��������܂���ł����B");
        }
    }


    /// �X�e�[�W���p�l����\��
    private void ShowStageInfoPanel()
    {
        stageInfoPanel.SetActive(true);
    }


    /// �o���{�^���������ꂽ�Ƃ��ɌĂ΂�鏈��
    private void OnDeployButtonClicked()
    {
        if (selectedStageData != null)
        {
            SceneManager.LoadScene("Main");
        }
        else
        {
            Debug.LogError("�o�����悤�Ƃ��Ă���X�e�[�W�f�[�^��null�ł��B");
        }
    }
}
