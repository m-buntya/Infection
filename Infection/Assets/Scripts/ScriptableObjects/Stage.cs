using UnityEngine;
using System.Collections.Generic;

/// �X�e�[�W�f�[�^1���̏��
[System.Serializable]
public class StageData
{
    /// �X�e�[�W�̎��ʎq�i��F "001"�A"Stage_A" �Ȃǁj
    public string stageID;

    /// �^�X�NID�̈ꗗ�i�o�����Ȃǁj
    public List<int> taskList;

    /// �o�ꂷ��G�I�u�W�F�N�g�̃v���n�u���X�g
    public List<GameObject> enemyList;
}

/// �X�e�[�W�S�̂̃f�[�^�Ǘ��pScriptableObject
[CreateAssetMenu(fileName = "Stage", menuName = "Stage")]
public class Stage : ScriptableObject
{
    /// �o�^����Ă���S�X�e�[�W�̃��X�g
    public List<StageData> stageDatas;
}
