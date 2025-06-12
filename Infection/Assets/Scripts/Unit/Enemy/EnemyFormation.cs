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
    public EnemyStats enemyStats;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
