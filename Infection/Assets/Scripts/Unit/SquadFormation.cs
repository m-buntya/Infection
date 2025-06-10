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
        Debug.Log("パラメータをセット");

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
}

// 部隊の編成管理
public class SquadFormation : MonoBehaviour
{
    [SerializeField] UnitStatsData unitStatsData;

    public SquadStats squadStats { get; private set; }

    [SerializeField] Slider soldierSlider;
    int squadMemberCnt;

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        squadStats = new SquadStats();
        Debug.Log(squadStats);

        squadStats.SetLeaderStats(Clone(unitStatsData.UnitParameter[0]));

        int squadMemberMinCnt = 0;      // デバッグ用
        int squadMemberMaxCnt = 100;   // デバッグ用

        // スライダーの最小、最大値設定
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
