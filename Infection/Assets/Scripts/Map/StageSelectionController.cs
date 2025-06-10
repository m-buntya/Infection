//Chat GPT�Q��

using UnityEngine;
using UnityEngine.SceneManagement;


/// �X�e�[�W�I���̋����𐧌䂷��R���g���[���[
public class StageSelectionController : MonoBehaviour
{
    [Header("���C���J����")]
    [SerializeField] private Camera mainCamera;

    [Header("�X�e�[�W�I���\�ȃ��C���[")]
    [SerializeField] private LayerMask stageLayerMask;

    private string selectedAreaName;

    // Awake�ŃJ������������PlayerPrefs����G���A����ǂݍ���
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
            // �K�v�Ȃ炱���ŃG���A�I���V�[���ɖ߂������Ȃǂ�ǉ�
        }
        else
        {
            Debug.Log($"�G���A�u{selectedAreaName}�v���I������܂����B�X�e�[�W�I�����J�n���Ă��������B");
        }
    }

    // ���t���[���A���N���b�N�����m���X�e�[�W�N���b�N�𔻒�
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


    /// �X�e�[�W���N���b�N���������肵�A�I��������V�[�������[�h����
    private void DetectStageClick()
    {
        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(
            mousePos,
            Vector2.zero,
            Mathf.Infinity,
            stageLayerMask
        );

        if (hit.collider != null)
        {
            string stageName = hit.collider.gameObject.name;
            Debug.Log($"�X�e�[�W�u{stageName}�v���I������܂����B");

            LoadSelectedStageScene(stageName);
        }
    }

    
    /// �w�肵���X�e�[�W���̃V�[�������݂���΃��[�h����
    private void LoadSelectedStageScene(string stageName)
    {
        if (Application.CanStreamedLevelBeLoaded(stageName))
        {
            SceneManager.LoadScene(stageName);
        }
        else
        {
            Debug.LogError($"�V�[�� '{stageName}' �� Build Settings �ɓo�^����Ă��܂���B");
        }
    }
}
