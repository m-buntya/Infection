using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class StageData
{
    public int stageID;
    public List<int> taskList;
    public List<GameObject> enemyList;
}

[CreateAssetMenu(fileName = "Stage", menuName = "Stage")]
public class Stage : ScriptableObject
{
    public List<StageData> stageDatas;
}
