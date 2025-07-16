using UnityEngine;
using System.Collections.Generic;
using TMPro;
using StatePatteren.State;
public class DeployManager : MonoBehaviour
{
    [SerializeField] private CostManager costManager;
    //[SerializeField] private Transform deployParent;
    //[SerializeField] private Vector3 deployPosition = Vector3.zero;
    //[SerializeField] private Transform deployPoint;
    //[SerializeField] private TMP_Text deployCounterText;
    //[SerializeField] private UnitController unitController;

    //private const int MAX_DEPLOYABLE_UNITS = 6;
    //private int currentUnitCount = 0;
    //private bool isDeployable = true;

    //private Dictionary<UnitController, int> deployCounts = new Dictionary<UnitController, int>();
    private Dictionary<UnitController, float> unitCooldowns = new Dictionary<UnitController, float>();

    public static DeployManager Instance { get; private set; }

    [SerializeField] private UnitController targetUnit;

    //private void Awake()
    //{
    //    if (Instance == null)
    //    {
    //        Instance = this;
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    //private void Start()
    //{
    //    UpdateDeployText();
    //}

    void Update()
    {
        List<UnitController> keys = new List<UnitController>(unitCooldowns.Keys);
        foreach (var unit in keys)
        {
            unitCooldowns[unit] -= Time.deltaTime;
            if (unitCooldowns[unit] <= 0f)
            {
                unitCooldowns.Remove(unit);
            }
        }
    }



    //private Vector3 GetValidDeployPosition()
    //{
    //    Vector3 basePosition = deployPoint.position;
    //    Vector3 viewportPos = Camera.main.WorldToViewportPoint(basePosition);
    //    viewportPos.x = Mathf.Clamp(viewportPos.x, 0.1f, 0.9f);
    //    viewportPos.y = Mathf.Clamp(viewportPos.y, 0.1f, 0.9f);
    //    viewportPos.z = basePosition.z - Camera.main.transform.position.z;
    //    return Camera.main.ViewportToWorldPoint(viewportPos);
    //}

    public void TryDeployUnit(UnitController unitController)
    {
        //if (!isDeployable) return;

        //if (!deployCounts.ContainsKey(unitController))
        //    deployCounts[unitController] = 0;

        //if (deployCounts[unitController] >= unitController.UnitStats.maxDeployCount)
        //{
        //    Debug.Log($"{unitController.unitStats.unitName} の出撃上限に達しています！");
        //    return;
        //}

        // クールタイム中なら出撃できない
        if (unitCooldowns.ContainsKey(unitController))
        {
            Debug.Log($"{unitController.unitStats.unitName} はクールタイム中（残り {unitCooldowns[unitController]:F1} 秒）");
            return;
        }

        // コスト不足なら出撃できない
        if (!costManager.CanAfford(unitController.unitStats.cost))
        {
            costManager.DisplayInsufficientCostFeedBack();
            return;
        }

        costManager.SpendCost(unitController.unitStats.cost);
        //Vector3 validPosition = GetValidDeployPosition();
        //GameObject unitObj = Instantiate(unitController.unitStats.prefab, validPosition, Quaternion.identity, deployParent);

        // ⬇ 感染マネージャーへの登録
       
        //UnitMovement newUnit = unitObj.GetComponent<UnitMovement>();

        //deployCounts[unitController]++;
        unitCooldowns[unitController] = unitController.unitStats.sortieCoolTime;

        Debug.Log($"{unitController.unitStats.unitName} を出撃！ 次は {unitController.unitStats.sortieCoolTime} 秒後に再出撃できます。");
    }
    public void OnDeployButtonPressed()
    {
        TryDeployUnit(targetUnit);
    }

    //public void ResetDeployment()
    //{
    //    currentUnitCount = 0;
    //    isDeployable = true;
    //    deployCounts.Clear();
    //    unitCooldowns.Clear();
    //}

    //private void UpdateDeployText()
    //{
    //    if (deployCounterText != null && unitController != null)
    //    {
    //        deployCounterText.text = $"出撃可能: 無制限";
    //    }
    //}


    //public int GetDeployedCount(UnitController unit)
    //{
    //    return deployCounts.ContainsKey(unit) ? deployCounts[unit] : 0;
    //}
}