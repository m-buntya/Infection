using UnityEngine;
using System.Collections.Generic;
using TMPro;
public class DeployManager : MonoBehaviour
{
    [SerializeField] private CostManager costManager;
    [SerializeField] private Transform deployParent;
    [SerializeField] private Vector3 deployPosition = Vector3.zero;
    [SerializeField] private Transform deployPoint;
    [SerializeField] private TMP_Text deployCounterText;
    [SerializeField] private UnitData unitData;
    private const int MAX_DEPLOYABLE_UNITS = 6;
    private int currentUnitCount = 0;
    private bool isDeployable = true;

    private Dictionary<UnitData, int> deployCounts = new Dictionary<UnitData, int>();
    private Dictionary<UnitData, float> unitCooldowns = new Dictionary<UnitData, float>();

    public static DeployManager Instance { get; private set; }
    private void Start()
    {
        UpdateDeployText();        
    }
    void Update()
    {
        // 各ユニットのクールタイムをカウントダウン
        List<UnitData> keys = new List<UnitData>(unitCooldowns.Keys);
        foreach (var unit in keys)
        {
            unitCooldowns[unit] -= Time.deltaTime;
            if (unitCooldowns[unit] <= 0f)
            {
                unitCooldowns.Remove(unit);
            }
        }
    }
    private Vector3 GetValidDeployPosition()
    {
        Vector3 basePosition = deployPoint.position;
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(basePosition);
        viewportPos.x = Mathf.Clamp(viewportPos.x, 0.1f, 0.9f);
        viewportPos.y = Mathf.Clamp(viewportPos.y, 0.1f, 0.9f);
        viewportPos.z = basePosition.z - Camera.main.transform.position.z;
        return Camera.main.ViewportToWorldPoint(viewportPos);
    }

    public void TryDeployUnit(UnitData unit)
    {
        if (!isDeployable) return;

        if (!deployCounts.ContainsKey(unit))
            deployCounts[unit] = 0;

        if (deployCounts[unit] >= unit.maxDeployCount)
        {
            Debug.Log($"{unit.unitName} の出撃上限に達しています！");
            return;
        }

        if (unitCooldowns.ContainsKey(unit))
        {
            Debug.Log($"{unit.unitName} は現在クールタイム中です！（あと {unitCooldowns[unit]:F1} 秒）");
            return;
        }

        if (!costManager.CanAfford(unit.cost))
        {
            costManager.DisplayInsufficientCostFeedBack();
            return;
        }
        costManager.SpendCost(unit.cost);
        Vector3 validPosition = GetValidDeployPosition();
        GameObject unitObj = Instantiate(unit.prefab, validPosition, Quaternion.identity, deployParent);

        // 必要ならここで UnitMovement 取得やターゲット指定もできる
        UnitMovement newUnit = unitObj.GetComponent<UnitMovement>();

        deployCounts[unit]++;
        unitCooldowns[unit] = unit.cooldownTime;

        Debug.Log($"{unit.unitName} を出撃！ 次は {unit.cooldownTime} 秒後に再出撃できます。");
       
    }

    public void ResetDeployment()
    {
        currentUnitCount = 0;
        isDeployable = true;
        deployCounts.Clear();
        unitCooldowns.Clear();
    }
    private void UpdateDeployText()
    {
        if (deployCounterText != null && unitData != null)
        {
            int remainingDeploys = unitData.maxDeployCount - (deployCounts.ContainsKey(unitData) ? deployCounts[unitData] : 0);
            deployCounterText.text = $"出撃可能: {remainingDeploys}/{unitData.maxDeployCount}";
        }
    }
    public int GetDeployedCount(UnitData unit)
    {
        return deployCounts.ContainsKey(unit) ? deployCounts[unit] : 0;
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}