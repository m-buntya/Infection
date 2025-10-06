using UnityEngine;
using UnityEngine.UI;
using TMPro;
using StatePatteren.State;

public class FormationUnitButton : MonoBehaviour
{
    public GameObject unitPrefab;
    public GameObject redFrame;
    public Button button;
    public FormationPanelManager panelManager;

    public UnitController unitController;
    public UnitAttackBace attackBace;

    private UnitStats currentlySelectedStats;

    [Header("表示用UI")]
    public TextMeshProUGUI roleText;
    public TextMeshProUGUI soldierCntText;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI atkText;
    public TextMeshProUGUI virusPowText;
    public TextMeshProUGUI atkSpdText;
    public TextMeshProUGUI spdText;
    public TextMeshProUGUI rangeText;
    public TextMeshProUGUI costText;
    public TextMeshProUGUI unitNameText;
    public TextMeshProUGUI unitCodeText;

    [Header("兵士数（外部から設定）")]
    public int soldierCnt = 0;

    private void Start()
    {
        // ✅ 明示的に初期化
        unitController = unitPrefab.GetComponent<UnitController>();
        attackBace = unitPrefab.GetComponent<UnitAttackBace>();

        if (button != null)
        {
            button.onClick.AddListener(() =>
            {
                if (panelManager != null && unitController != null)
                {
                    panelManager.HighlightUnit(unitController);
                }

                if (attackBace != null && attackBace.unitStats != null)
                {
                    var stats = attackBace.unitStats;

                    // UI表示
                    roleText.text = stats.role.ToString();
                    soldierCntText.text = soldierCnt.ToString();
                    hpText.text = stats.maxHp.ToString("F1");
                    atkText.text = stats.atk.ToString("F1");
                    virusPowText.text = stats.virusPow.ToString("F1");
                    atkSpdText.text = stats.atkSpd.ToString("F1");
                    spdText.text = stats.spd.ToString("F1");
                    rangeText.text = stats.range.ToString();
                    costText.text = stats.cost.ToString();
                    unitNameText.text = stats.unitName;
                    unitCodeText.text = stats.unitCode.ToString();

                    var icon = unitPrefab.GetComponentInChildren<SpriteRenderer>()?.sprite;
                    var slotButton = panelManager.GetCurrentSlotButton();
                    if (slotButton != null)
                    {
                        slotButton.SetUnit(unitController, icon, stats.unitCode.ToString(), stats.unitName);
                        Debug.Log($"📦 FormationUnitButton: unitCode = {stats.unitCode}, unitName = {stats.unitName}, iconName = {icon?.name}");
                        Debug.Log($"🔍 unitController = {unitController.name}, instanceID = {unitController.GetInstanceID()}");
                    }
                }
                else
                {
                    Debug.LogWarning("UnitAttackBace または unitStats が null です");
                }
            });
        }

        SetRedFrameVisible(false);
    }

    public void SetRedFrameVisible(bool visible)
    {
        if (redFrame != null)
            redFrame.SetActive(visible);
    }
}