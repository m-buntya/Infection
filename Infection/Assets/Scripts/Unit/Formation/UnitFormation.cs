using StatePatteren.State;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
// 部隊のステータス管理
//public class UnitParametor
//{
//    public UnitStats leaderUnit { get; private set; }   // リーダーユニットのパラメータ

//    float defaultMaxHp = 0;
//    float defaultAtk = 0;
//    float defaultVirusPow = 0;
//    float defaultSpd = 0;

//    public int unitMemberCnt;
//    int unitMemberMinCnt = 0;
//    int unitMemberMaxCnt = 100;

//    // 雑兵のメンバー数をセット
//    public void SetSoldierCnt(int value)
//    {
//        unitMemberCnt = Mathf.Clamp(value, unitMemberMinCnt, unitMemberMaxCnt);
//    }

//    // リーダーのパラメータをセット
//    public void SetLeaderStats(UnitStats leader)
//    {
//        leaderUnit = leader;

//        defaultMaxHp = leader.maxHp;
//        defaultAtk = leader.atk;
//        defaultVirusPow = leader.virusPow;
//        defaultSpd = leader.spd;
//    }

//    // 部隊のパラメータをセット
//    public void SetUnitPara()
//    {
//        float correction = unitMemberCnt * 0.01f;      // 部隊の人数 * 1%の補正値

//        leaderUnit.maxHp    = defaultMaxHp       + defaultMaxHp * correction;
//        leaderUnit.hp       = leaderUnit.maxHp;
//        leaderUnit.atk      = defaultAtk      + defaultAtk      * correction;
//        leaderUnit.virusPow = defaultVirusPow + defaultVirusPow * correction;
//        float slowRate = (float)unitMemberCnt / unitMemberMaxCnt;
//        leaderUnit.spd = defaultSpd * (1 - slowRate);
//    }
//}

// 部隊の編成管理
public class UnitFormation : MonoBehaviour
{
    [SerializeField] UnitStatsData unitStatsData;
    [SerializeField] Squad squadData;

    UnitUIManager unitUIManager;
    UnitManager unitManager;
    CostManager costManager;

    public UnitParametor unitPara { get; private set; }     // 部隊のステータス(設定中)

    [SerializeField] Slider soldierSlider;
    int unitMemberCnt;

    [SerializeField] List<GameObject> unitIcon;      // 部隊アイコン
    Dictionary<GameObject, UnitStats> unitStatsDic = new Dictionary<GameObject, UnitStats>();
    [SerializeField] GameObject unitObj;             // 部隊オブジェクト

    const int UNIT_MAX_CNT = 8;     // 作成できる部隊の上限
    int unitsIndex = 0;             // 作成した部隊数

    [SerializeField] Transform EnemySpawnPoint;

    void Awake()
    {
        UnitReset();

        unitUIManager = GameObject.Find("UnitUIManager").GetComponent<UnitUIManager>();
        unitPara = new UnitParametor();
        unitPara.SetLeaderStats(Clone(unitStatsData.UnitParameter[0]));

        int unitMemberMinCnt = 0;      // デバッグ用
        int unitMemberMaxCnt = 100;    // デバッグ用

        // スライダーの最小、最大値設定
        soldierSlider.minValue = unitMemberMinCnt;
        soldierSlider.maxValue = unitMemberMaxCnt;

        soldierSlider.onValueChanged.AddListener(OnSliderSoldier);      // スライダーの変更を検知できるようにする
    }

    // 部隊初期化
    void UnitReset()
    {
        squadData.squadList[0].units = new UnitStats[UNIT_MAX_CNT];
        unitsIndex = 0;
        SerInteractable();
    }

    // リーダー選択
    public void OnClickLeader(int num)
    {
        unitPara.SetLeaderStats(Clone(unitStatsData.UnitParameter[num]));
        unitPara.SetUnitPara();
    }

    // 雑兵数選択
    void OnSliderSoldier(float value)
    {
        unitPara.SetSoldierCnt((int)value);
        unitPara.SetUnitPara();
        unitUIManager.UnitParaTexts();
    }

    // 部隊作成
    public void OnClickSet()
    {
        if(unitsIndex < UNIT_MAX_CNT)
        {
            squadData.squadList[0].units.SetValue(Clone(unitPara.leaderUnit), unitsIndex);
        }

        unitsIndex++;
        SerInteractable();
    }

    // 部隊削除
    public void OnClickRemove()
    {
        UnitReset();
    }

    // 部隊作成ボタン有効・無効切り替え
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

    // UnitStats を new で複製する関数を作る
    public static UnitStats Clone(UnitStats original)
    {
        return new UnitStats
        {
            unitCode = original.unitCode,
            unitName = original.unitName,
            leaderSkill = original.leaderSkill,
            role = original.role,
            lv = original.lv,
            maxLv = original.maxLv,
            hp = original.hp,
            maxHp = original.maxHp,
            virusPoint = original.virusPoint,
            virusMaxPoint = original.virusMaxPoint,
            enemyVirusPoint = original.enemyVirusPoint,
            enemyVirusMaxPoint = original.enemyVirusMaxPoint,
            atk = original.atk,
            virusPow = original.virusPow,
            atkSpd = original.atkSpd,
            spd = original.spd,
            range = original.range,
            cost = original.cost,
            sortieCoolTime = original.sortieCoolTime
        };
    }
}
