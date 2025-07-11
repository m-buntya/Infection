using StatePatteren.State;
using UnityEngine;

public class UnitInfection : MonoBehaviour
{
    public float virusPoint = 0f;
    public UnitData unitData;

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

    // ✅ 実行時の所属を個別に管理！
    private UNITSIDE currentSide;

    void Start()
    {
        virusPoint = unitData.initialvirusPoint;
        currentSide = unitData.defaultSide; // ← 初期所属をコピー！

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
            controller.SetUnitGroup(currentSide == UNITSIDE.Player ?
                UnitController.UNIT_GROUP.PLAYER :
                UnitController.UNIT_GROUP.ENEMY);
        }
    }

    void Update()
    {
        if (testAutoInfect && virusPoint < unitData.maxVirusPoint)
        {
            virusPoint += testInfectSpeed * Time.deltaTime;
            virusPoint = Mathf.Min(virusPoint, unitData.maxVirusPoint);
            UpdateGaugeBar();
        }
    }

    public void AddInfection(float amount)
    {
        virusPoint = Mathf.Min(virusPoint + amount, unitData.maxVirusPoint);
        UpdateGaugeBar();

        if (virusPoint >= unitData.maxVirusPoint)
        {
            Debug.Log($"{unitData.unitName} は感染限界に達し、所属が切り替わります！");
            ChangeSide();
        }
    }

    void ChangeSide()
    {
        var before = currentSide;

        // 所属を切り替え（unitData は変更しない！）
        if (currentSide == UNITSIDE.Player)
        {
            currentSide = UNITSIDE.Enemy;
            GetComponent<UnitController>().SetUnitGroup(UnitController.UNIT_GROUP.ENEMY);
        }
        else
        {
            currentSide = UNITSIDE.Player;
            GetComponent<UnitController>().SetUnitGroup(UnitController.UNIT_GROUP.PLAYER);
        }

        // 感染値とゲージをリセット
        virusPoint = 0f;
        UpdateGaugeBar();
        UpdateGaugeColor();

        Debug.Log($"✅ 所属が切り替わりました{unitData.unitName}：{before} → {currentSide}");
    }

    void UpdateGaugeBar()
    {
        if (gaugeBar == null) return;

        float ratio = Mathf.Clamp01(virusPoint / unitData.maxVirusPoint);
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

        gaugeRenderer.color = currentSide == UNITSIDE.Player ? playerColor : enemyColor;
    }

    void OnDestroy()
    {
        if (InfectionManager.Instance != null)
            InfectionManager.Instance.UnregisterUnit(this);
    }
}