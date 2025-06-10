using UnityEngine;

// �����̃X�e�[�^�X�Ǘ�
[SerializeField]
public class SquadStats
{
    UnitStats leaderUnit;   // ���[�_�[���j�b�g�̃p�����[�^

    int squadMemberCnt;     // �����̃����o�[��
    bool isDead;            // ��������ł�����

    // ���[�_�[�̃p�����[�^���Z�b�g
    public void SetLeaderStats(UnitStats leader)
    {
        leaderUnit = leader;
    }

    // �����̃p�����[�^���Z�b�g
    public void SetSquadStats(int member)
    {
        float correction = member * (1 / 100);      // �����̐l�� * 1%�̕␳�l

        leaderUnit.hp       += leaderUnit.hp       * correction;
        leaderUnit.atk      += leaderUnit.atk      * correction;
        leaderUnit.virusPow += leaderUnit.virusPow * correction;
        leaderUnit.spd      -= leaderUnit.spd      * correction;
    }
}

// �����̕Ґ��Ǘ�
public class SquadFormation : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
