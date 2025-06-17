using StatePatteren.State;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
// �����̃X�e�[�^�X�Ǘ�
public class UnitParametor
{
    public UnitStats leaderUnit { get; private set; }   // ���[�_�[���j�b�g�̃p�����[�^

    float defaultHp = 0;
    float defaultAtk = 0;
    float defaultVirusPow = 0;
    float defaultSpd = 0;

    public int unitMemberCnt;
    int unitMemberMinCnt = 0;
    int unitMemberMaxCnt = 100;

    // �G���̃����o�[�����Z�b�g
    public void SetSoldierCnt(int value)
    {
        unitMemberCnt = Mathf.Clamp(value, unitMemberMinCnt, unitMemberMaxCnt);
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
    public void SetunitPara()
    {
        //Debug.Log("�p�����[�^���Z�b�g");

        float correction = unitMemberCnt * 0.01f;      // �����̐l�� * 1%�̕␳�l

        leaderUnit.hp       = defaultHp       + defaultHp       * correction;
        leaderUnit.atk      = defaultAtk      + defaultAtk      * correction;
        leaderUnit.virusPow = defaultVirusPow + defaultVirusPow * correction;
        float slowRate = (float)unitMemberCnt / unitMemberMaxCnt;
        leaderUnit.spd = defaultSpd * (1 - slowRate);

        //Debug.Log(leaderUnit.hp);
        //Debug.Log(leaderUnit.atk);
        //Debug.Log(leaderUnit.virusPow);
        //Debug.Log(leaderUnit.spd);
    }
}

// �����̕Ґ��Ǘ�
public class UnitFormation : MonoBehaviour
{
    [SerializeField] UnitStatsData unitStatsData;

    UnitUIManager unitUIManager;

    public UnitParametor unitPara { get; private set; }     // �����̃X�e�[�^�X(�ݒ蒆)

    [SerializeField] Slider soldierSlider;
    int unitMemberCnt;

    [SerializeField] UnitStats[] units;              // �e�����̃X�e�[�^�X(�ݒ��)
    [SerializeField] List<GameObject> unitIcon;      // �����A�C�R��
    [SerializeField] GameObject unitObj;             // �����I�u�W�F�N�g

    const int UNIT_MAX_CNT = 8;     // �쐬�ł��镔���̏��
    int unitsIndex = 0;             // �쐬����������

    void Awake()
    {
        UnitReset();

        unitUIManager = GameObject.Find("UnitUIManager").GetComponent<UnitUIManager>();

        unitPara = new UnitParametor();
        unitPara.SetLeaderStats(Clone(unitStatsData.UnitParameter[0]));

        int unitMemberMinCnt = 0;      // �f�o�b�O�p
        int unitMemberMaxCnt = 100;    // �f�o�b�O�p

        // �X���C�_�[�̍ŏ��A�ő�l�ݒ�
        soldierSlider.minValue = unitMemberMinCnt;
        soldierSlider.maxValue = unitMemberMaxCnt;

        soldierSlider.onValueChanged.AddListener(OnSliderSoldier);      // �X���C�_�[�̕ύX�����m�ł���悤�ɂ���
    }

    // ����������
    void UnitReset()
    {
        units = new UnitStats[UNIT_MAX_CNT];
        for(int i = 0; i < UNIT_MAX_CNT; i++)
        {
            UnitComplete(i);
        }
        unitsIndex = 0;
        SerInteractable();
    }

    // ���[�_�[�I��
    public void OnClickLeader(int num)
    {
        unitPara.SetLeaderStats(Clone(unitStatsData.UnitParameter[num]));
        unitPara.SetunitPara();
    }

    // �G�����I��
    void OnSliderSoldier(float value)
    {
        unitPara.SetSoldierCnt((int)value);
        unitPara.SetunitPara();
        unitUIManager.UnitParaTexts();
    }

    // �����쐬
    public void OnClickSet()
    {
        if(unitsIndex < UNIT_MAX_CNT)
        {
            units.SetValue(Clone(unitPara.leaderUnit), unitsIndex);
            UnitComplete(unitsIndex);
        }

        unitsIndex++;
        SerInteractable();
    }

    // �����폜
    public void OnClickRemove()
    {
        UnitReset();
    }

    // ��������
    public void OnClickCreate(int num)
    {
        GameObject unit = Instantiate(unitObj);
        UnitController unitController = unit.GetComponent<UnitController>();
        unitController.SetUnitStats(units[num]);
    }

    // �����Ґ������\��
    void UnitComplete(int unitsNum)
    {
        TextMeshProUGUI completeText = unitIcon[unitsNum].transform.Find("CompleteText").GetComponent<TextMeshProUGUI>();
        if (units[unitsNum] != null)
        {
            completeText.text = "!";
        }
        else
        {
            completeText.text = "X";
        }
    }

    // �����쐬�{�^���L���E�����؂�ւ�
    void SerInteractable()
    {
        Button button = GameObject.Find("SetUnit").GetComponent<Button>();

        if (unitsIndex >= UNIT_MAX_CNT)
        {            
            button.interactable = false;
        }
        else
        {
            button.interactable = true;
        }
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
