using UnityEngine;

[System.Serializable]
public class UnitEnemyStats
{
    public enum LEADER_SKILL    // ���[�_�[�X�L��
    {
        AtkBoost,               // �U���͏㏸
        SpdBoost,               // �ړ����x�㏸
        Regeneration,           // ���R��
    }

    public enum ROLE           // ���[��
    {
        Attacker,               // �A�^�b�J�[
        Tank,                   // �^���N
        Healer,                 // �q�[���[
        Baffer,                 // �o�b�t�@�[
        Debaffer,               // �f�o�b�t�@�[
    }

    public int unitCode;        // ���j�b�g�ԍ�
    public string unitName;     // ���j�b�g��
    public LEADER_SKILL leaderSkill;    // ���[�_�[�X�L��
    public ROLE role;                   // ���[��
    public float hp;            // �̗�
    public float virusHp;       // �����̗�
    public float atk;           // �U����
    public float atkSpd;        // �U�����x
    public float spd;           // �ړ����x
    public bool isFly;          // �ړ����@
    public float range;         // �˒�����
}
