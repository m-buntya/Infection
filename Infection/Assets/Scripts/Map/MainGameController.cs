using UnityEngine;

public class MainGameController : MonoBehaviour
{
    private void Start()
    {
        StageData data = GameManager.Instance.SelectedStageData;

        if (data != null)
        {
            Debug.Log($"Main�V�[���J�n�F�X�e�[�WID = {data.stageID}, �G�� = {data.enemyList.Count}");
            // �����œG�𐶐�������A�^�X�N���J�n������ł���
        }
        else
        {
            Debug.LogError("GameManager �ɃX�e�[�W�f�[�^���n����Ă��܂���B");
        }
    }
}
