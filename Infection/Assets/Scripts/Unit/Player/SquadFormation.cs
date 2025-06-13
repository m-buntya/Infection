using StatePatteren.State;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// �����̃X�e�[�^�X�Ǘ�
public class SquadStats
{
    public UnitStats leaderUnit { get; private set; }   // ���[�_�[���j�b�g�̃p�����[�^

    float defaultHp = 0;
    float defaultAtk = 0;
    float defaultVirusPow = 0;
    float defaultSpd = 0;

    public int squadMemberCnt;
    int squadMemberMinCnt = 0;
    int squadMemberMaxCnt = 100;
    bool isDead;            // ��������ł�����

    // �G���̃����o�[�����Z�b�g
    public void SetSoldierCnt(int value)
    {
        squadMemberCnt = value;
    }

    // ���[�_�[�̃p�����[�^���Z�b�g
    public void SetLeaderStats(UnitStats leader)
    {
        leaderUnit = leader;

        defaultHp = leader.hp;
        defaultAtk = leader.atk;
        defaultVirusPow = leader.virusPow;
        defaultSpd = leader.spd;
    }

    // �����̃p�����[�^���Z�b�g
    public void SetSquadStats()
    {
        //Debug.Log("�p�����[�^���Z�b�g");

        float correction = squadMemberCnt * 0.01f;      // �����̐l�� * 1%�̕␳�l

        leaderUnit.hp       = defaultHp       + defaultHp       * correction;
        leaderUnit.atk      = defaultAtk      + defaultAtk      * correction;
        leaderUnit.virusPow = defaultVirusPow + defaultVirusPow * correction;
        float slowRate = (float)squadMemberCnt / squadMemberMaxCnt;
        leaderUnit.spd = defaultSpd * (1 - slowRate);

        //Debug.Log(leaderUnit.hp);
        //Debug.Log(leaderUnit.atk);
        //Debug.Log(leaderUnit.virusPow);
        //Debug.Log(leaderUnit.spd);
    }

    // SquadStats �� new �ŕ�������֐������
    public SquadStats Clone()
    {
        SquadStats clone = new SquadStats();
        clone.SetLeaderStats(new UnitStats
        {
            unitCode = leaderUnit.unitCode,
            unitName = leaderUnit.unitName,
            leaderSkill = leaderUnit.leaderSkill,
            role = leaderUnit.role,
            lv = leaderUnit.lv,
            hp = leaderUnit.hp,
            atk = leaderUnit.atk,
            virusPow = leaderUnit.virusPow,
            atkSpd = leaderUnit.atkSpd,
            spd = leaderUnit.spd,
            isFly = leaderUnit.isFly,
            range = leaderUnit.range,
            cost = leaderUnit.cost,
            sortieCoolTime = leaderUnit.sortieCoolTime
        });
        clone.SetSoldierCnt(this.squadMemberCnt);
        clone.SetSquadStats();
        return clone;
    }
}

// �����̕Ґ��Ǘ�
public class SquadFormation : MonoBehaviour
{
    [SerializeField] UnitStatsData unitStatsData;

    public SquadStats squadStats { get; private set; }

    [SerializeField] Slider soldierSlider;
    int squadMemberCnt;

    [SerializeField] List<SquadController> squadList;
    [SerializeField] GameObject squadObj;       // �����I�u�W�F�N�g

    void Awake()
    {
        squadStats = new SquadStats();

        squadStats.SetLeaderStats(Clone(unitStatsData.UnitParameter[0]));

        int squadMemberMinCnt = 0;      // �f�o�b�O�p
        int squadMemberMaxCnt = 100;   // �f�o�b�O�p

        // �X���C�_�[�̍ŏ��A�ő�l�ݒ�
        soldierSlider.minValue = squadMemberMinCnt;
        soldierSlider.maxValue = squadMemberMaxCnt;

        soldierSlider.onValueChanged.AddListener(OnSliderSoldier);

        squadList.Clear();
    }

    // ���[�_�[�I��
    public void OnClickLeader(int num)
    {
        squadStats.SetLeaderStats(Clone(unitStatsData.UnitParameter[num]));
        squadStats.SetSquadStats();
    }

    // �G�����I��
    void OnSliderSoldier(float value)
    {
        squadStats.SetSoldierCnt((int)value);
        squadStats.SetSquadStats();
    }

    // �����쐬
    public void OnClickCreat()
    {
        GameObject squad = Instantiate(squadObj);
        SquadController squadController = squad.GetComponent<SquadController>();
        squadController.SetUnitStats(squadStats.Clone());
        squadList.Add(squadController);
    }

    // UnitStats �� new �ŕ�������֐������
    public UnitStats Clone(UnitStats original)
    {
        return new UnitStats
        {
            unitCode = original.unitCode,
            unitName = original.unitName,
            leaderSkill = original.leaderSkill,
            role = original.role,
            lv = original.lv,
            hp = original.hp,
            atk = original.atk,
            virusPow = original.virusPow,
            atkSpd = original.atkSpd,
            spd = original.spd,
            isFly = original.isFly,
            range = original.range,
            cost = original.cost,
            sortieCoolTime = original.sortieCoolTime
        };
    }
}
