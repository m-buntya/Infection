using UnityEngine;

public class MainGameController : MonoBehaviour
{
    private void Start()
    {
        StageData data = GameManager.Instance.SelectedStageData;

        if (data != null)
        {
            Debug.Log($"Mainシーン開始：ステージID = {data.stageID}, 敵数 = {data.enemyList.Count}");
            // ここで敵を生成したり、タスクを開始したりできる
        }
        else
        {
            Debug.LogError("GameManager にステージデータが渡されていません。");
        }
    }
}
