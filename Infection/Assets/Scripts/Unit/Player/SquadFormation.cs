using StatePatteren.State;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// 部隊のステータス管理
public class SquadStats
{
    public UnitStats leaderUnit { get; private set; }   // リーダーユニットのパラメータ

    float defaultHp = 0;
    float defaultAtk = 0;
    float defaultVirusPow = 0;
    float defaultSpd = 0;

    public int squadMemberCnt;
    int squadMemberMinCnt = 0;
    int squadMemberMaxCnt = 100;
    bool isDead;            // 部隊が壊滅したか

    // 雑兵のメンバー数をセット
    public void SetSoldierCnt(int value)
    {
        squadMemberCnt = value;
    }

    // リーダーのパラメータをセット
    public void SetLeaderStats(UnitStats leader)
    {
        leaderUnit = leader;

        defaultHp = leader.hp;
        defaultAtk = leader.atk;
        defaultVirusPow = leader.virusPow;
        defaultSpd = leader.spd;
    }

    // 部隊のパラメータをセット
    public void SetSquadStats()
    {
        //Debug.Log("パラメータをセット");

        float correction = squadMemberCnt * 0.01f;      // 部隊の人数 * 1%の補正値

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

    // SquadStats を new で複製する関数を作る
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

// 部隊の編成管理
public class SquadFormation : MonoBehaviour
{
    [SerializeField] UnitStatsData unitStatsData;

    public SquadStats squadStats { get; private set; }

    [SerializeField] Slider soldierSlider;
    int squadMemberCnt;

    [SerializeField] List<SquadController> squadList;
    [SerializeField] GameObject squadObj;       // 部隊オブジェクト

    void Awake()
    {
        squadStats = new SquadStats();

        squadStats.SetLeaderStats(Clone(unitStatsData.UnitParameter[0]));

        int squadMemberMinCnt = 0;      // デバッグ用
        int squadMemberMaxCnt = 100;   // デバッグ用

        // スライダーの最小、最大値設定
        soldierSlider.minValue = squadMemberMinCnt;
        soldierSlider.maxValue = squadMemberMaxCnt;

        soldierSlider.onValueChanged.AddListener(OnSliderSoldier);

        squadList.Clear();
    }

    // リーダー選択
    public void OnClickLeader(int num)
    {
        squadStats.SetLeaderStats(Clone(unitStatsData.UnitParameter[num]));
        squadStats.SetSquadStats();
    }

    // 雑兵数選択
    void OnSliderSoldier(float value)
    {
        squadStats.SetSoldierCnt((int)value);
        squadStats.SetSquadStats();
    }

    // 部隊作成
    public void OnClickCreat()
    {
        GameObject squad = Instantiate(squadObj);
        SquadController squadController = squad.GetComponent<SquadController>();
        squadController.SetUnitStats(squadStats.Clone());
        squadList.Add(squadController);
    }

    // UnitStats を new で複製する関数を作る
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
