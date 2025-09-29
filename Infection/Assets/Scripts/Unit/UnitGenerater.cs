using StatePatteren.State;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;

public class UnitGenerater : MonoBehaviour
{
    [SerializeField] UnitStatsData unitStatsData;
    [SerializeField] Squad squadData;

    UnitManager unitManager;
    CostManager costManager;
    PrefabGridManager prefabGridManager;

    [SerializeField] List<GameObject> unitIcon;      // 部隊アイコン
    Dictionary<GameObject, UnitStats> unitStatsDic = new Dictionary<GameObject, UnitStats>();
    [SerializeField] GameObject unitObj;             // 部隊オブジェクト

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        unitManager = GameObject.Find("UnitManager").GetComponent<UnitManager>();
        costManager = GameObject.Find("GameManager").GetComponent<CostManager>();
        prefabGridManager = GameObject.Find("Enemy_TilePlacer").GetComponent<PrefabGridManager>();

        StartCoroutine(EnemyGenerate());

        for (int i = 0; i < squadData.squadList[0].units.Length; i++)
        {
            unitStatsDic[unitIcon[i]] = squadData.squadList[0].units[i];
            UnitComplete(i);
        }
    }

    void Update()
    {
        CostCheck();
    }

    // コストが足りているか
    public void CostCheck()
    {
        foreach(GameObject unitIcon in unitIcon)
        {
            UnitDragHandler ud = unitIcon.GetComponent<UnitDragHandler>();

            if (costManager.CanAfford(unitStatsDic[unitIcon].cost))
            {
                ud.SetIsDrag(true);
            }
            else
            {
                ud.SetIsDrag(false);
            }
        }
    }

    // 部隊生成
    public void UnitGenerate(GameObject create, Vector3 pos)
    {
        GameObject unit = Instantiate(unitObj, pos, Quaternion.identity);

        UnitController unitController = unit.GetComponent<UnitController>();
        unitController.SetUnitStats(Clone(unitStatsDic[create]));
        unitController.SetUnitGroup(UnitController.UNIT_GROUP.PLAYER);

        unitManager.AddUnitList(unit, "Player");
        costManager.SpendCost(unitController.unitStats.cost);
    }

    // 敵部隊生成
    IEnumerator EnemyGenerate()
    {
        while (true)
        {
            var unit_Idx = Random.Range(0, 4);
            var grid_Idx = Random.Range(0, prefabGridManager.prefabList.Count);

            GameObject unit = Instantiate(unitObj, prefabGridManager.prefabList[grid_Idx].transform.position, Quaternion.identity);

            UnitController unitController = unit.GetComponent<UnitController>();
            unitController.SetUnitStats(Clone(unitStatsData.UnitParameter[unit_Idx]));
            unitController.SetUnitGroup(UnitController.UNIT_GROUP.ENEMY);

            unitManager.AddUnitList(unit, "Enemy");

            yield break;
            //yield return new WaitForSeconds(10.0f);
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
            hp = original.maxHp,
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
