using StatePatteren.State;
using StatePatteren.StateEnemy;
using UnityEngine;

// 部隊のステータス管理
public class EnemyStats
{
    public UnitEnemyStats enemyUnit { get; private set; }   // リーダーユニットのパラメータ

    float defaultHp = 0;
    float defaultVirusHp = 0;
    float defaultAtk = 0;
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
    public void SetLeaderStats(UnitEnemyStats leader)
    {
        enemyUnit = leader;

        defaultHp = leader.hp;
        defaultVirusHp = leader.virusHp;
        defaultAtk = leader.atk;
        defaultSpd = leader.spd;
    }

    // 部隊のパラメータをセット
    public void SetSquadStats()
    {
        //Debug.Log("パラメータをセット");

        float correction = squadMemberCnt * 0.01f;      // 部隊の人数 * 1%の補正値

        enemyUnit.hp = defaultHp + defaultHp * correction;
        enemyUnit.virusHp = defaultVirusHp + defaultVirusHp * correction;
        enemyUnit.atk = defaultAtk + defaultAtk * correction;
        float slowRate = (float)squadMemberCnt / squadMemberMaxCnt;
        enemyUnit.spd = defaultSpd * (1 - slowRate);

        //Debug.Log(leaderUnit.hp);
        //Debug.Log(leaderUnit.atk);
        //Debug.Log(leaderUnit.virusPow);
        //Debug.Log(leaderUnit.spd);
    }
}

public class EnemyFormation : MonoBehaviour
{
    [SerializeField] UnitEnemyStatsData unitEnemyStats;

    public EnemyStats enemyStats { get; private set; }

    // UnitStats を new で複製する関数を作る
    public UnitEnemyStats Clone(UnitEnemyStats original)
    {
        return new UnitEnemyStats
        {
            unitCode = original.unitCode,
            unitName = original.unitName,
            leaderSkill = original.leaderSkill,
            role = original.role,
            hp = original.hp,
            virusHp = original.virusHp,
            atk = original.atk,
            atkSpd = original.atkSpd,
            spd = original.spd,
            isFly = original.isFly,
            range = original.range,
        };
    }

    void Awake()
    {
        enemyStats = new EnemyStats();

        enemyStats.SetLeaderStats(Clone(unitEnemyStats.UnitEnemyParameter[0]));
        enemyStats.SetSoldierCnt(100);
        enemyStats.SetSquadStats();
    }
}
