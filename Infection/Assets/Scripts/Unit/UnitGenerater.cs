using StatePatteren.State;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnitGenerater : MonoBehaviour
{
    [SerializeField] Squad squadData;

    UnitManager unitManager;
    CostManager costManager;

    [SerializeField] List<GameObject> unitIcon;      // 部隊アイコン
    Dictionary<GameObject, UnitStats> unitStatsDic = new Dictionary<GameObject, UnitStats>();
    [SerializeField] GameObject unitObj;             // 部隊オブジェクト

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        unitManager = GameObject.Find("UnitManager").GetComponent<UnitManager>();
        costManager = GameObject.Find("GameManager").GetComponent<CostManager>();

        for (int i = 0; i < squadData.squadList[0].units.Length; i++)
        {
            unitStatsDic[unitIcon[i]] = squadData.squadList[0].units[i];
            UnitComplete(i);
        }
    }

    // 部隊生成
    public void UnitGenerate(GameObject create, Vector3 pos)
    {
        if (!costManager.CanAfford(unitStatsDic[create].cost))
        {
            Debug.Log("コストが足りません");
            return;
        }
        else
        {
            GameObject unit = Instantiate(unitObj, pos, transform.rotation);
            UnitController unitController = unit.GetComponent<UnitController>();
            unitController.SetUnitStats(Clone(unitStatsDic[create]));
            unitController.SetUnitGroup(UnitController.UNIT_GROUP.PLAYER);
            unitManager.AddUnitList(unit, "Player");
            costManager.SpendCost(unitController.unitStats.cost);
        }        
    }

    // 部隊編成完了表示
    void UnitComplete(int unitsNum)
    {
        TextMeshProUGUI completeText = unitIcon[unitsNum].transform.Find("CompleteText").GetComponent<TextMeshProUGUI>();
        if (squadData.squadList[0].units[unitsNum].hp != 0)
        {
            completeText.text = "!";
        }
        else
        {
            completeText.text = "X";
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
