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
        Debug.Log("�p�����[�^���Z�b�g");

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
}

// �����̕Ґ��Ǘ�
public class SquadFormation : MonoBehaviour
{
    [SerializeField] UnitStatsData unitStatsData;

    public SquadStats squadStats { get; private set; }

    [SerializeField] Slider soldierSlider;
    int squadMemberCnt;

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        squadStats = new SquadStats();
        Debug.Log(squadStats);

        squadStats.SetLeaderStats(Clone(unitStatsData.UnitParameter[0]));

        int squadMemberMinCnt = 0;      // �f�o�b�O�p
        int squadMemberMaxCnt = 100;   // �f�o�b�O�p

        // �X���C�_�[�̍ŏ��A�ő�l�ݒ�
        soldierSlider.minValue = squadMemberMinCnt;
        soldierSlider.maxValue = squadMemberMaxCnt;

        soldierSlider.onValueChanged.AddListener(OnSliderSoldier);
    }

    public void OnClickLeader(int num)
    {
        squadStats.SetLeaderStats(Clone(unitStatsData.UnitParameter[num]));
        squadStats.SetSquadStats();
    }

    void OnSliderSoldier(float value)
    {
        squadStats.SetSoldierCnt((int)value);
        squadStats.SetSquadStats();
    }
}
