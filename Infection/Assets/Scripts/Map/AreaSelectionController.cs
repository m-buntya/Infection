//Chat GPT�Q��

using UnityEngine;
using UnityEngine.SceneManagement;


/// �G���A�I���̋����𐧌䂷��R���g���[���[
public class AreaSelectionController : MonoBehaviour
{
    [Header("�N���b�N���o�Ɏg���J����")]
    [SerializeField] private Camera mainCamera;

    [Header("�G���A�̃��C���[�}�X�N")]
    [SerializeField] private LayerMask areaLayerMask;

    // Awake�ŃJ�����̏��������s��
    private void Awake()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    // ���t���[���A���N���b�N�����m���ăG���A�N���b�N�𔻒肷��
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DetectAreaClick();
        }
    }

    
    /// �G���A���N���b�N���������肵�A�I��������V�[�������[�h����
    private void DetectAreaClick()
    {
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(
            mousePosition,
            Vector2.zero,
            Mathf.Infinity,
            areaLayerMask
        );

        if (hit.collider != null)
        {
            string areaName = hit.collider.gameObject.name;
            Debug.Log($"�I�����ꂽ�G���A: {areaName}");

            // PlayerPrefs�ɕۑ�
            PlayerPrefs.SetString("SelectedArea", areaName);
            PlayerPrefs.Save();

            LoadSelectedAreaScene(areaName);
        }
    }

    
    /// �w�肵���G���A���̃V�[�������݂���΃��[�h����
    private void LoadSelectedAreaScene(string areaName)
    {
        if (Application.CanStreamedLevelBeLoaded(areaName))
        {
            SceneManager.LoadScene(areaName);
        }
        else
        {
            Debug.LogError($"�V�[�� '{areaName}' �� Build Settings �ɓo�^����Ă��܂���B");
        }
    }
}
