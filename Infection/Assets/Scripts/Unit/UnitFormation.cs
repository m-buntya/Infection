using StatePatteren.State;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
// 部隊のステータス管理
public class UnitParametor
{
    public UnitStats leaderUnit { get; private set; }   // リーダーユニットのパラメータ

    float defaultHp = 0;
    float defaultAtk = 0;
    float defaultVirusPow = 0;
    float defaultSpd = 0;

    public int unitMemberCnt;
    int unitMemberMinCnt = 0;
    int unitMemberMaxCnt = 100;

    // 雑兵のメンバー数をセット
    public void SetSoldierCnt(int value)
    {
        unitMemberCnt = Mathf.Clamp(value, unitMemberMinCnt, unitMemberMaxCnt);
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
    public void SetunitPara()
    {
        //Debug.Log("パラメータをセット");

        float correction = unitMemberCnt * 0.01f;      // 部隊の人数 * 1%の補正値

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

// 部隊の編成管理
public class UnitFormation : MonoBehaviour
{
    [SerializeField] UnitStatsData unitStatsData;

    UnitUIManager unitUIManager;

    public UnitParametor unitPara { get; private set; }     // 部隊のステータス(設定中)

    [SerializeField] Slider soldierSlider;
    int unitMemberCnt;

    [SerializeField] UnitStats[] units;              // 各部隊のステータス(設定後)
    [SerializeField] List<GameObject> unitIcon;      // 部隊アイコン
    [SerializeField] GameObject unitObj;             // 部隊オブジェクト

    const int UNIT_MAX_CNT = 8;     // 作成できる部隊の上限
    int unitsIndex = 0;             // 作成した部隊数

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
        units = new UnitStats[UNIT_MAX_CNT];
        for(int i = 0; i < UNIT_MAX_CNT; i++)
        {
            UnitComplete(i);
        }
        unitsIndex = 0;
        SerInteractable();
    }

    // リーダー選択
    public void OnClickLeader(int num)
    {
        unitPara.SetLeaderStats(Clone(unitStatsData.UnitParameter[num]));
        unitPara.SetunitPara();
    }

    // 雑兵数選択
    void OnSliderSoldier(float value)
    {
        unitPara.SetSoldierCnt((int)value);
        unitPara.SetunitPara();
        unitUIManager.UnitParaTexts();
    }

    // 部隊作成
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

    // 部隊削除
    public void OnClickRemove()
    {
        UnitReset();
    }

    // 部隊生成
    public void OnClickCreate(int num)
    {
        GameObject unit = Instantiate(unitObj);
        UnitController unitController = unit.GetComponent<UnitController>();
        unitController.SetUnitStats(units[num]);
    }

    // 部隊編成完了表示
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
