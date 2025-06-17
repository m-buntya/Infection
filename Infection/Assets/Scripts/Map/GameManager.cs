using UnityEngine;

/// �Q�[���S�̂̏�Ԃ��Ǘ�����}�l�[�W���[�i�V���O���g���j
/// �o�W�FChatGPT����
public class GameManager : MonoBehaviour
{
    /// �V���O���g���C���X�^���X
    public static GameManager Instance { get; private set; }

    /// �I�����ꂽ�X�e�[�W�̃f�[�^
    public StageData SelectedStageData { get; private set; }


    /// �V���O���g���̏������Ɖi����
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    /// �I�����ꂽ�X�e�[�W�f�[�^���Z�b�g����
    public void SetStageData(StageData data)
    {
        SelectedStageData = data;
    }
}
