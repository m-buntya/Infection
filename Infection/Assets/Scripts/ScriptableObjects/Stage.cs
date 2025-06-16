using UnityEngine;
using System.Collections.Generic;

/// ステージデータ1つ分の情報
[System.Serializable]
public class StageData
{
    /// ステージの識別子（例： "001"、"Stage_A" など）
    public string stageID;

    /// タスクIDの一覧（出現順など）
    public List<int> taskList;

    /// 登場する敵オブジェクトのプレハブリスト
    public List<GameObject> enemyList;
}

/// ステージ全体のデータ管理用ScriptableObject
[CreateAssetMenu(fileName = "Stage", menuName = "Stage")]
public class Stage : ScriptableObject
{
    /// 登録されている全ステージのリスト
    public List<StageData> stageDatas;
}
