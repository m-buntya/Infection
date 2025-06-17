using StatePatteren.State;
using StatePatteren.StateEnemy;
using System.Collections;
using UnityEngine;

[System.Serializable]
public class UnitStats
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
    public int lv;              // ���x��
    public float hp;            // �̗�
    public float atk;           // �U����
    public float virusPow;      // ������
    public float atkSpd;        // �U�����x
    public float spd;           // �ړ����x
    public bool isFly;          // �ړ����@
    public float range;         // �˒�����
    public int cost;            // �R�X�g
    public int sortieCoolTime;  // �o���ɂ����鎞��
}