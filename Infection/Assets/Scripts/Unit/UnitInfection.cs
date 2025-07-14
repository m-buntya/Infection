using StatePatteren.State;
using UnityEngine;

public class UnitInfection : MonoBehaviour
{
    public float virusPoint = 0f;
   
    [Header("感染ゲージ")]
    [SerializeField] private Transform gaugeBar;
    [SerializeField] private float maxGaugeWidth = 1f;
    [SerializeField] private float gaugeOffset = -0.05f;

    [Header("ゲージ色設定")]
    [SerializeField] private Color playerColor = Color.green;
    [SerializeField] private Color enemyColor = Color.red;

    [Header("テスト用自動感染")]
    [SerializeField] private bool testAutoInfect = false;
    [SerializeField] private float testInfectSpeed = 10f;

    private SpriteRenderer gaugeRenderer;
    private Vector3 initialScale;
    private Vector3 initialLocalPosition;

    private UnitController unitController;
    private UnitController.UNIT_GROUP currentSide;

    void Start()
    {
        unitController = GetComponent<UnitController>();
        virusPoint = unitController.unitStats.virusPoint;
        currentSide = unitController.GetUnitGroup();
        InfectionManager.Instance.RegisterUnit(this);

        if (gaugeBar != null)
        {
            gaugeRenderer = gaugeBar.GetComponent<SpriteRenderer>();
            initialScale = gaugeBar.localScale;
            initialLocalPosition = gaugeBar.localPosition;
            UpdateGaugeColor();
        }

        // 所属に応じてユニットグループを設定（必要な場合）
        var controller = GetComponent<UnitController>();
        if (controller != null)
        {
            controller.SetUnitGroup(currentSide == UnitController.UNIT_GROUP.PLAYER ?
                UnitController.UNIT_GROUP.PLAYER :
                UnitController.UNIT_GROUP.ENEMY);
        }
    }

    void Update()
    {
        if (testAutoInfect && virusPoint < unitController.unitStats.virusMaxPoint)
        {
            virusPoint += testInfectSpeed * Time.deltaTime;
            virusPoint = Mathf.Min(virusPoint, unitController.unitStats.virusMaxPoint);
            UpdateGaugeBar();
        }
    }

    public void AddInfection(float amount)
    {
        virusPoint = Mathf.Min(virusPoint + amount, unitController.unitStats.virusMaxPoint);
        UpdateGaugeBar();

        if (virusPoint >= unitController.unitStats.virusMaxPoint)
        {
            Debug.Log($"{unitController.unitStats.unitName} は感染限界に達し、所属が切り替わります！");
            ChangeSide();
        }
    }

    void ChangeSide()
    {
        var before = currentSide;

        // 所属を切り替え（unitData は変更しない！）
        if (currentSide == UnitController.UNIT_GROUP.PLAYER)
        {
            currentSide = UnitController.UNIT_GROUP.ENEMY;
            GetComponent<UnitController>().SetUnitGroup(UnitController.UNIT_GROUP.ENEMY);
        }
        else
        {
            currentSide = UnitController.UNIT_GROUP.PLAYER;
            GetComponent<UnitController>().SetUnitGroup(UnitController.UNIT_GROUP.PLAYER);
        }

        // 感染値とゲージをリセット
        virusPoint = 0f;
        UpdateGaugeBar();
        UpdateGaugeColor();

        Debug.Log($"✅ 所属が切り替わりました{unitController.unitStats.unitName}：{before} → {currentSide}");
    }

    void UpdateGaugeBar()
    {
        if (gaugeBar == null) return;

        float ratio = Mathf.Clamp01(virusPoint / unitController.unitStats.virusMaxPoint);
        float newWidth = ratio * maxGaugeWidth;

        Vector3 newScale = gaugeBar.localScale;
        newScale.x = newWidth;
        gaugeBar.localScale = newScale;

        Vector3 newPos = gaugeBar.localPosition;
        newPos.x = (newWidth - maxGaugeWidth) / 2f + gaugeOffset;
        gaugeBar.localPosition = newPos;
    }

    void UpdateGaugeColor()
    {
        if (gaugeRenderer == null) return;

        gaugeRenderer.color = currentSide == UnitController.UNIT_GROUP.PLAYER ? playerColor : enemyColor;
    }

    void OnDestroy()
    {
        if (InfectionManager.Instance != null)
            InfectionManager.Instance.UnregisterUnit(this);
    }
}