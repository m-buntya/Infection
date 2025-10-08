using StatePatteren.State;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Android.Gradle;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using static UnityEngine.EventSystems.EventTrigger;

public class Synthesissystem : MonoBehaviour
{
    UnitController unitController;

    public UnitManager unitManager;
    //public UnitDragHandler unitDragHandler;

    public GameObject testSynthesisUnit;
    public SynthesisUnitStatsData synthesisUnitStatsData;

    // 合成可能なユニットの参照用
    GameObject targetUnit1;
    GameObject targetUnit2;
    // 合成可能なユニットの名前を入れる
    public string taergetUnitName1 = "アーチャー";
    public string targetUnitName2 = "アタッカー";

    //合成をする距離
    [Tooltip("ここでユニットがどれだけ近づいたら合成を発動するかを指定")]
    public float synthesisdistance = 3f;

    //合成が可能かのフラグ
    public bool isSynthes = false;

    public bool hasDisplay = false;

    [Tooltip("true の時は Player と Enemy の合成はしない")]
    //デバッグ用のフラグ
    public bool isDebug = false;

    //味方、敵のユニット数
    public int paleyerCount;
    public int enemyCount;

    // ロールの順不同比較用ペアを正規化
    private (UnitStats.ROLE, UnitStats.ROLE) NormalizePair(UnitStats.ROLE a, UnitStats.ROLE b)
    {
        return (a.CompareTo(b) <= 0) ? (a, b) : (b, a);
    }

    private void Start()
    {

    }

    private void Update()
    {
        //TODO 後で使えるようにストック部分を作る
        if (hasDisplay) return;

        Debug.Log("見つかった味方ユニットの数: " + unitManager.GetPlayerUnits().Count);
        Debug.Log("見つかった敵ユニットの数: " + unitManager.GetEnemyUnits().Count);

        isSynthes = false; // 毎フレーム初期化

        List<GameObject> allUnits = new List<GameObject>();
        allUnits.AddRange(unitManager.GetPlayerUnits());
        allUnits.AddRange(unitManager.GetEnemyUnits());

        // オブジェクト同士のペアをチェック
        for (int i = 0; i < allUnits.Count; i++)
        {
            for (int j = i + 1; j < allUnits.Count; j++)
            {
                GameObject objA = allUnits[i];
                GameObject objB = allUnits[j];

                //ユニット同士の距離を測る
                float distance = Vector3.Distance(objA.transform.position, objB.transform.position);

                if (distance <= synthesisdistance)//変数で距離を調整
                {
                    var ucA = objA.GetComponent<UnitController>();
                    var ucB = objB.GetComponent<UnitController>();

                    if (ucA != null && ucB != null &&
                        ucA.unitStats != null && ucB.unitStats != null)
                    {
                        //グループのチェック
                        var groupA = ucA.GetUnitGroup();
                        var groupB = ucB.GetUnitGroup();

                        if(isDebug && 
                            ((groupA == UnitController.UNIT_GROUP.PLAYER && groupB == UnitController.UNIT_GROUP.ENEMY)||
                            (groupA == UnitController.UNIT_GROUP.ENEMY && groupB == UnitController.UNIT_GROUP.ENEMY)))
                        {
                            Debug.Log("DebugモードでPlayerとEnemyの合成をスキップ");
                            continue;
                        }

                        //両方の生成が完了しているか
                        if (!ucA.isSynthesisReady || !ucB.isSynthesisReady)
                        {
                            Debug.Log("どちらかのユニットが合成準備完了していない");
                            continue;
                        }

                        var roleA = ucA.unitStats.role;
                        var roleB = ucB.unitStats.role;
                        var pair = NormalizePair(roleA, roleB);

                        Debug.Log($"Checking pair: {roleA} and {roleB}, Distance: {distance}");

                        if(synthesisUnitStatsData == null)
                        {
                            Debug.LogError("SynthesisUnitStatsDataが見つからないです");
                        }

                        bool exists = synthesisUnitStatsData.SynthesisUnitParameter.Any(entry =>
                            NormalizePair(entry.combo1, entry.combo2) == pair);

                        //組み合わせを判定
                        if (exists)
                        {
                            Debug.Log($"合成条件成立: {roleA} と {roleB} 距離: {distance}");
                            targetUnit1 = objA;
                            targetUnit2 = objB;
                            taergetUnitName1 = ucA.unitStats.unitName;
                            targetUnitName2 = ucB.unitStats.unitName;

                            isSynthes = true;
                            hasDisplay = true;
                            return;
                        }
                    }
                }
            }
        }

        Debug.Log("合成条件に一致するペアはありませんでした。");
    }



    public void GetSynthesisUnit()
    {
        if (targetUnit1 == null || targetUnit2 == null)
        {
            Debug.LogWarning("合成対象が設定されていません");
            return;
        }

        var roleA = targetUnit1.GetComponent<UnitController>().unitStats.role;
        var roleB = targetUnit2.GetComponent<UnitController>().unitStats.role;
        var normalizedPair = NormalizePair(roleA, roleB);

        var resultStats = synthesisUnitStatsData.SynthesisUnitParameter.FirstOrDefault(entry =>
            NormalizePair(entry.combo1, entry.combo2) == normalizedPair
        );

        if (resultStats == null)
        {
            Debug.LogWarning("この組み合わせに対応する合成ユニットが定義されていません");
            return;
        }

        // 合成ユニットの生成
        // 2つのユニットの中間地点に配置
        Vector2 spawnPosition = (targetUnit1.transform.position + targetUnit2.transform.position) / 2;
        GameObject newUnit = Instantiate(testSynthesisUnit, spawnPosition, Quaternion.identity);
        Debug.Log($"合成ユニットを生成しました: {newUnit.name}");

        var controller = newUnit.GetComponent<SynthesisUnitController>();
        if (controller != null)
        {
            controller.SetUnitStats(resultStats);
            controller.SetUnitGroup(SynthesisUnitController.UNIT_GROUP.PLAYER);
        }
        else
        {
            Debug.LogWarning("SynthesisUnitController が見つかりませんでした");
        }

        // 旧ユニットの削除
        Destroy(targetUnit1);
        Destroy(targetUnit2);

        // 状態リセット
        targetUnit1 = null;
        targetUnit2 = null;
        taergetUnitName1 = "";
        targetUnitName2 = "";
        isSynthes = false;
        hasDisplay = false;
    }

}
