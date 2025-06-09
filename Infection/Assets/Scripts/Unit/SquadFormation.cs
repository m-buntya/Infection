using UnityEngine;

// 部隊のステータス管理
[SerializeField]
public class SquadStats
{
    UnitStats leaderUnit;   // リーダーユニットのパラメータ

    int squadMemberCnt;     // 部隊のメンバー数
    bool isDead;            // 部隊が壊滅したか

    // リーダーのパラメータをセット
    public void SetLeaderStats(UnitStats leader)
    {
        leaderUnit = leader;
    }

    // 部隊のパラメータをセット
    public void SetSquadStats(int member)
    {
        float correction = member * (1 / 100);      // 部隊の人数 * 1%の補正値

        leaderUnit.hp       += leaderUnit.hp       * correction;
        leaderUnit.atk      += leaderUnit.atk      * correction;
        leaderUnit.virusPow += leaderUnit.virusPow * correction;
        leaderUnit.spd      -= leaderUnit.spd      * correction;
    }
}

// 部隊の編成管理
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
