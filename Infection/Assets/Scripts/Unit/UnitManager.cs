using UnityEngine;
using System.Collections.Generic;

public class UnitManager : MonoBehaviour
{
    [SerializeField] List<GameObject> playerUnits;
    [SerializeField] List<GameObject> enemyUnits;

    // プレイヤーユニットのリストを取得
    public List<GameObject> GetPlayerUnits()
    {
        return playerUnits;
    }

    // エネミーユニットのリストを取得
    public List<GameObject> GetEnemyUnits()
    {
        return enemyUnits;
    }

    // ユニットリストに追加
    public void AddUnitList(GameObject unit, string group)
    {
        if(group == "Player")
        {
            playerUnits.Add(unit);
        }
        else if (group == "Enemy")
        {
            enemyUnits.Add(unit);
        }
    }

    // ユニットリストから削除
    public void RemoveUnitList(GameObject unit, string group)
    {
        if (group == "Player")
        {
            playerUnits.Remove(unit);
        }
        else if (group == "Enemy")
        {
            enemyUnits.Remove(unit);
        }
    }
}
