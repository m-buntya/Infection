using UnityEngine;

public class DeployManager : MonoBehaviour
{
    [SerializeField] private CostManager costManager;
    [SerializeField] private Transform deployParent;
    [SerializeField] private Vector3 deployPosition = Vector3.zero;

    private const int MAX_DEPLOYABLE_UNITS = 6;
    private int currentUnitCount = 0;
    private const float DEPLOY_COOL_DOWNTIME = 5.0f;
    private float remainingCooldown = 0.0f;
    private bool isDeployable = true;
    private bool isCooldownActive = false;

    void Update()
    {
        if (isCooldownActive)
        {
            remainingCooldown -= Time.deltaTime;
            if (remainingCooldown <= 0f)
            {
                remainingCooldown = 0f;
                isCooldownActive = false;
            }
        }
    }

    public void TryDeployUnit(UnitData unit)
    {
        if (!isDeployable)
        {
            Debug.Log("拠点が崩壊しているため出撃できません！");
            return;
        }

        if (isCooldownActive)
        {
            Debug.Log("クールタイム中です！");
            return;
        }

        if (currentUnitCount >= MAX_DEPLOYABLE_UNITS)
        {
            Debug.Log("出撃可能上限に達しています！");
            return;
        }

        if (!costManager.CanAfford(unit.cost))
        {
            costManager.DisplayInsufficientCostFeedBack();
            return;
        }

        costManager.SpendCost(unit.cost); // 🔥 コスト消費

        GameObject newUnit = Instantiate(unit.prefab, deployPosition, Quaternion.identity, deployParent);
        currentUnitCount++;

        StartCooldown();
        Debug.Log($"{unit.unitName} を出撃しました！ 残りコスト: {costManager.GetCurrentCost()}");
    }

    public void ResetDeployment()
    {
        currentUnitCount = 0;
        isCooldownActive = false;
        remainingCooldown = 0f;
        isDeployable = true;
    }

    private void StartCooldown()
    {
        remainingCooldown = DEPLOY_COOL_DOWNTIME;
        isCooldownActive = true;
    }
}